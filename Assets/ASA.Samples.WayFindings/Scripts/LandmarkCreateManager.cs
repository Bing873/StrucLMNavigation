// Copyright (c) 2020 Takahiro Miyaura
// Released under the MIT license
// http://opensource.org/licenses/mit-license.php

using System;
using System.Collections.Generic;
using Com.Reseul.ASA.Samples.WayFindings.Anchors;
using Com.Reseul.ASA.Samples.WayFindings.Factories;
using Com.Reseul.ASA.Samples.WayFindings.SpatialAnchors;
using Com.Reseul.ASA.Samples.WayFindings.UX.Dialogs;
using Com.Reseul.ASA.Samples.WayFindings.UX.Effects;
using Com.Reseul.ASA.Samples.WayFindings.UX.Menus;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace Com.Reseul.ASA.Samples.WayFindings
{
    /// <summary>
    ///     Class for create landmarks
    /// </summary>
    public class LandmarkCreateManager : MonoBehaviour
    {
        private string basePointAnchorId;
        private IDictionary<string, string> basePointAppProperties;

        [HideInInspector]
        public string CurrentAnchorId;

        private GameObject currentAnchorObject;
        
        private SettingPointAnchor settingPointAnchor;
        private int landmarkType;

        #region Static Methods

        /// <summary>
        ///     Get an instance of the class that performs the processing of Azure Spatial Anchors.
        /// </summary>
        public static LandmarkCreateManager Instance
        {
            get
            {
                var module = FindObjectsOfType<LandmarkCreateManager>();
                if (module.Length == 1)
                {
                    return module[0];
                }

                Debug.LogWarning(
                    "Not found an existing LandmarkCreateManager in your scene. The LandmarkCreateManager requires only one.");
                return null;
            }
        }

        #endregion

        #region Inspector Properites

        //[Tooltip("Set a GameObject of Landmark Collection.")]
        //public GameObject LandmarkCollection;

        [Tooltip("Set a GameObject of Instruction.")]
        public GameObject Instruction;

        [Tooltip("Set a GameObject of Menu.")]
        public LandmarkCreateMenu Menu;



        #endregion

        #region Public Methods

        /// <summary>
        ///    Enables or disables the object.
        /// </summary>
        /// <param name="enabled"></param>
        public void Initialize(bool enabled, string anchorId = null,
            SettingPointAnchor anchorObj = null, IDictionary<string, string> appProperties = null)
        {
            Debug.Log("LCManager is called");
            try
            {
                if (anchorId == null || anchorObj == null || appProperties == null)
                {
                    Debug.LogWarning("null reference the base point anchor information.");
                    return;
                }

                basePointAnchorId = anchorId;
                CurrentAnchorId = basePointAnchorId;
                Debug.Log($"LCManager is called and AnchorID is {anchorId} not null");
                settingPointAnchor = anchorObj;
                basePointAppProperties = appProperties;

                Dialog.OpenDialog("Create Landmarks",
                    "Please create the landmarks\n * Selecting 'Create Next Landmark', to create landmark and type.\n * Selecting 'Create Next Point', add transit point.",
                    new[] { "Ok" }, new[]
                    {
                        new UnityAction(() =>
                        {
                            Instruction?.SetActive(enabled);
                           // LandmarkCollection?.SetActive(enabled);
                            Menu.ChangeStatus(BaseMenu.MODE_INITIALIZE);
                        })
                    });
            }
            catch (Exception e)
            {
                Debug.Log(e);
                throw;
            }
        }

        /// <summary>
        ///     Generate a Gameobject to be used to visualize the anchor.
        /// </summary>
        /// <param name="landmarkType">Landmark Type determination</param> //isDestination
        public void CreateAnchorObject(int landmarkType)
        {
            try
            {
                Menu.ChangeStatus(LandmarkCreateMenu.MODE_CREATE_ANCHOR);

                this.landmarkType = landmarkType;

                if (currentAnchorObject == null)
                {
                    currentAnchorObject = settingPointAnchor.gameObject;
                }

                LandmarkPointAnchor landm = null;
                switch (landmarkType)
                {
                    case 0:
                        //Debug.Log("LMCreate Mangager calls to create VSE_LM");
                        landm = AnchorGenerateFactory.GenerateVSELandmarkPointAnchor(); //(SettingPointAnchor.AnchorMode.Landmark,LandmarkPointAnchor.LandmarkType.VSLandmark);
                       // Debug.Log("LMCreate Mangager calls to create VSE_LM");
                        break;
                    case 1:
                        landm = AnchorGenerateFactory.GenerateSFLandmarkPointAnchor();// (SettingPointAnchor.AnchorMode.Landmark, LandmarkPointAnchor.LandmarkType.SFLandmark);
                       // Debug.Log("LMCreate Mangager calls to create SF_LM");
                        break;
                    case 2:
                        landm = AnchorGenerateFactory.GenerateDFLandmarkPointAnchor();// (SettingPointAnchor.AnchorMode.Landmark, LandmarkPointAnchor.LandmarkType.DFLandmark);
                       // Debug.Log("LMCreate Mangager calls to create DF_LM"); 
                        break;                    
                }

                currentAnchorObject = landm.gameObject;
                //currentAnchorObject.transform.parent = LandmarkCollection.transform;
                currentAnchorObject.transform.position =
                    Camera.main.transform.position + Camera.main.transform.forward * 1f;

               
            }
            catch (Exception e)
            {
                Debug.Log(e);
                throw;
            }
        }

        /// <summary>
        ///     可視化しているすべてのアンカーとリンクオブジェクトを削除します。
        /// </summary>
        public void DeleteAnchorObject()
        {

            DestroyImmediate(currentAnchorObject);
           // Menu.ChangeStatus(BaseMenu.MODE_COMPLETE);
            Menu.ChangeStatus(BaseMenu.MODE_INITIALIZE);
        }
        public void NextStepRouteCreate()
        {
            CloseContents();
            Menu.ChangeStatus(BaseMenu.MODE_CLOSE);
            SetRouteGuideManager.Instance.Initialize(true, basePointAnchorId, settingPointAnchor, basePointAppProperties);
        }

        /// <summary>
        ///    Executes the process to proceed to the next process step (route search).
        /// </summary>
        public void NextStepWayFinding()
        {
            Initialize(false);
            Menu.ChangeStatus(BaseMenu.MODE_CLOSE);
            WayFindingManager.Instance.Initialize(true, basePointAnchorId, settingPointAnchor,
                basePointAppProperties);
        }

        

        /// <summary>
        ///     アプリケーションを終了します。
        /// </summary>
        public void Exit()
        {
#if UNITY_EDITOR
            Menu.ChangeStatus(BaseMenu.MODE_COMPLETE);
            Debug.Log("exit called");
            EditorApplication.isPlaying = false;
#elif WINDOWS_UWP
            Menu.ChangeStatus(BaseMenu.MODE_COMPLETE);
            Debug.Log("exit called");
            Windows.ApplicationModel.Core.CoreApplication.Exit();
#endif
            Menu.ChangeStatus(BaseMenu.MODE_COMPLETE);
        }

        /// <summary>
        ///     Azure Spatial Anchorsサービスにアンカーを追加します。
        /// </summary>
        public async void CreateAzureAnchor(int landmarkType)
        {
            try
            {
                // Then we create a new local cloud anchor
                var appProperties = new Dictionary<string, string>();
  

                switch (landmarkType)
                {
                    case 0://LandmarkType.VSLandmark:
                        appProperties.Add(LandmarkCreateInformation.LM_TYPE, LandmarkCreateInformation.LM_TYPE_VSE);
                        Debug.Log($"VSElandmark Information created and key is {CurrentAnchorId} with Typevalue is {LandmarkCreateInformation.LM_TYPE_VSE} 1");
                        appProperties.Add(LandmarkCreateInformation.ANCHOR_ID, CurrentAnchorId);
                        Debug.Log($"VSElandmark Information created and key is {CurrentAnchorId} with Typevalue is {LandmarkCreateInformation.LM_TYPE_VSE} 2");
                        break;
                    case 1:// LandmarkType.SFLandmark:
                        appProperties.Add(LandmarkCreateInformation.LM_TYPE, LandmarkCreateInformation.LM_TYPE_SF);
                        appProperties.Add(LandmarkCreateInformation.ANCHOR_ID, CurrentAnchorId);
                        Debug.Log($"SFlandmark Information created and key is {CurrentAnchorId} with Typevalue is {LandmarkCreateInformation.LM_TYPE_SF}");
                        Debug.Log($"Landmark include {appProperties.Count} anchors");
                        //Debug.Log("SFlandmark Information created");
                        break;
                    case 2:// LandmarkType.DFLandmark:
                        appProperties.Add(LandmarkCreateInformation.LM_TYPE, LandmarkCreateInformation.LM_TYPE_DF);
                        appProperties.Add(LandmarkCreateInformation.ANCHOR_ID, CurrentAnchorId);
                        Debug.Log($"SFlandmark Information created and key is {CurrentAnchorId} with Typevalue is {LandmarkCreateInformation.LM_TYPE_DF}");
                        Debug.Log($"Landmark include {appProperties.Count} anchors");

                        Debug.Log("DFlandmark Information created");
                        break;
                }

                var identifier = await AnchorModuleProxy.Instance.CreateAzureAnchor(currentAnchorObject, appProperties);

                Debug.Log($"In total, Landmark include {appProperties.Keys} anchors");
                foreach (var anchorkey in appProperties.Keys){ Debug.Log($"anchor {anchorkey} is {appProperties[anchorkey]}"); }
                if (identifier == null)
                {
                    return;
                }

                CurrentAnchorId = identifier;
                var point = currentAnchorObject.GetComponent<LandmarkPointAnchor>();
                point.DisabledEffects();

                Menu.ChangeStatus(BaseMenu.MODE_INITIALIZE);
               


            }
            catch (Exception e)
            {
                Debug.Log(e);
                throw;
            }
        }

        #endregion
        private void CloseContents()
        {
            //isLocateAnchors = false;
            Menu.ChangeStatus(BaseMenu.MODE_CLOSE);
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
