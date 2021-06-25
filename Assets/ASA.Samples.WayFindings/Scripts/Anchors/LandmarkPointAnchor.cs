// Copyright (c) 2021 Bing Liu
// Released under the MIT license
// http://opensource.org/licenses/mit-license.php

using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Com.Reseul.ASA.Samples.WayFindings.UX.Effects;
using Com.Reseul.ASA.Samples.WayFindings.UX.KeyboardHelpers;
using Microsoft.MixedReality.Toolkit.Experimental.UI;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;


namespace Com.Reseul.ASA.Samples.WayFindings.Anchors
{
    /// <summary>
    ///     Spatial Anchor可視化に利用する経路案内時のアンカー用のコンポーネントです。
    /// </summary>
    public class LandmarkPointAnchor : MonoBehaviour
    {
       // private HandInteractionHint handInteractionHint;
        private NearInteractionGrabbable interactionGrabbable;
        private bool isInitialized;
        private ManipulationHandler manipulationHandler;
        private Vector3 pos1;
 

        public enum LandmarkType
        {
            /// <summary>
            ///     Virtual Semantic Landmark
            /// </summary>
            VSLandmark,

            /// <summary>
            ///     structural landmark leading to the same floor
            /// </summary>
            SFLandmark,

            /// <summary>
            ///     structural landmark leading to another floor
            /// </summary>
            DFLandmark,

        }


        #region Public Properites

        /// <summary>
        ///     目的地名を設定取得します。
        /// </summary>
        // [HideInInspector]
        //public string Landma;

        /// <summary>
        ///     Anchor IDを設定取得します。
        /// </summary>
       /* [HideInInspector]
        public string Identifier
        {
            get => identifier;
            set
            {
                identifier = value;
                name = "Anchor:" + value;
            }
        }*/


        #endregion

        #region Unity Lifecycle

        /// <summary>
        ///     初期化処理を実施します
        /// </summary>
        private void OnEnable()
        {
            if (isInitialized)
            {
                return;
            }

            isInitialized = true;
            
            Debug.Log("LPAnchor onEnable called");
            manipulationHandler = GetComponent<ManipulationHandler>();
            interactionGrabbable = GetComponent<NearInteractionGrabbable>();

        }

        /// <summary>
        ///     フレーム毎に実行する処理を実施します。
        /// </summary>
        private void Update()
        {
            ///回転系を何とかしたい
            transform.rotation =
                Quaternion.LookRotation(transform.position - Camera.main.transform.position, Vector3.up);

        }

        #endregion


        #region Public Methods
        ///// <summary>
        /////     オブジェクトの有効無効を設定します。
        ///// </summary>
        ///// <param name="state"></param>
        //public void SetActiveState(bool state)
        //{
        //    gameObject.SetActive(state);
        //}

        public void DisabledEffects()
        {
            manipulationHandler.enabled = false;
            interactionGrabbable.enabled = false;            
        }

        // set the landmark type
        public void SetLandmarkType(LandmarkType type)
        {
            OnEnable();
            //var getMaterial = this.GetComponent<Image>();

            switch (type)
            {
                case LandmarkType.VSLandmark:
                    //this.GetComponent<Image>().sprite = vsLandmark;
                   /* if (currentAnchorObject == null)
                    {
                        currentAnchorObject = settingPointAnchor.gameObject;
                    }*/
                    //Instantiate(Resources.Load("Landmarks"));
                    Debug.Log("VSlandmark Created");
                    //GameObject go1 = new GameObject();
                    //go1.name = "go1";
                   // currentAnchorObject.AddComponent<Rigidbody>();
                   // currentAnchorObject.AddComponent<ManipulationHandler>();

                    //systemKeyboardInputHelper.gameObject.SetActive(false);
                    break;
                case LandmarkType.SFLandmark:
                    //   this.GetComponent<Image>().sprite = sfLandmark; 
                    //systemKeyboardInputHelper.gameObject.SetActive(true);
                    Debug.Log("SFlandmark Created");
                    break;
                case LandmarkType.DFLandmark:
                    // this.GetComponent<Image>().sprite = dfLandmark;
                    //systemKeyboardInputHelper.gameObject.SetActive(true);
                    Debug.Log("DFlandmark Created");
                    break;
            }

        }

        #endregion
    }
}