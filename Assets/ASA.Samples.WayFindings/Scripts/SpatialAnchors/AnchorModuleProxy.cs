// Copyright (c) 2020 Takahiro Miyaura
// Released under the MIT license
// http://opensource.org/licenses/mit-license.php

using UnityEngine;

namespace Com.Reseul.ASA.Samples.WayFindings.SpatialAnchors
{
    /// <summary>
    ///     Proxy class for accessing Azure Spatial Anchors services
    /// </summary>
    /// <remarks>
    ///     Azure Spatial Anchors does not work with errors on Unity Editor, so we provide this class to work with stubs on Unity Editor.
    /// </remarks>
    public class AnchorModuleProxy : MonoBehaviour
    {
        /// <summary>
        ///     èDelegate for log output to display the progress of processing. Separately.<see cref="AnchorFeedbackScript" />Call within
        /// </summary>
        /// <param name="description">Message content</param> 
        /// <param name="isOverWrite">Whether to overwrite the previous message. Overwrite if True</param> 
        /// <param name="isReset"> Whether to delete the previous message. If True, delete the previous output before displaying</param>

        public delegate void FeedbackDescription(string description, bool isOverWrite = false, bool isReset = false);

        public float DistanceInMeters => distanceInMeters;

        #region Static Methods

        /// <summary>
        ///     Get an instance of the class that performs the processing of Azure Spatial Anchors.
        /// </summary>
        public static IAnchorModuleScript Instance
        {
            get
            {
#if UNITY_EDITOR
                // Execute processing with stub when executing Unity Editor
                var module = FindObjectsOfType<AnchorModuleScriptForStub>();
#else
                var module = FindObjectsOfType<AnchorModuleScript>();
#endif
                if (module.Length == 1)
                {
                    var proxy = FindObjectOfType<AnchorModuleProxy>();
                    //Set the parameters used by Azure Spatial Anchors
                    module[0].SetDistanceInMeters(proxy.distanceInMeters);
                    module[0].SetMaxResultCount(proxy.maxResultCount);
                    module[0].SetExpiration(proxy.Expiration);
                    return module[0];
                }

                Debug.LogWarning(
                    "Not found an existing AnchorModuleScript in your scene. The Anchor Module Script requires only one.");
                return null;
            }
        }

    #endregion

    #region Unity Lifecycle

        private void Start()
        {
#if UNITY_EDITOR
            // When running Unity Editor, disable the objects in Azure Spatial Anchors itself.
            transform.GetChild(0).gameObject.SetActive(false);
#endif
        }

        #endregion

        #region Inspector Properites

        [Header("NearbySetting")]
        [SerializeField]
        [Tooltip("Maximum distance in meters from the source anchor (defaults to 5).")]
        private float distanceInMeters = 5f;

        [SerializeField]
        [Tooltip("Maximum desired result count (defaults to 20).")]
        private int maxResultCount = 20;

        [Header("CreateAnchorParams")]
        [SerializeField]
        [Tooltip("The number of days until the anchor is automatically deleted")]
        private int Expiration = 7;

    #endregion
    }
}