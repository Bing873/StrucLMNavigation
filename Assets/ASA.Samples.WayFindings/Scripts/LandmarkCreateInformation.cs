// Copyright (c) 2021 Bing Liu
// Released under the MIT license
// http://opensource.org/licenses/mit-license.php

using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Com.Reseul.ASA.Samples.WayFindings
{
    /// <summary>
    ///     経路探索、道案内先に必要なアンカー情報を管理するクラスです。
    /// </summary>
    public class LandmarkCreateInformation : MonoBehaviour
    {
        /// <summary>
        ///     Key used in AppProperties: Constant representing the key name that stores the anchor type (VSE/SF/DF)
        /// </summary>
        public const string LM_TYPE = "Type";

        /// <summary>
        ///     A constant that represents the type of anchor (VS)
        /// </summary>
        public const string LM_TYPE_VSE = "VSE";

        /// <summary>
        ///      A constant that represents the type of anchor (SF)
        /// </summary>
        public const string LM_TYPE_SF = "SF";

        public const string ANCHOR_ID = "AnchorID";

        /// <summary>
        ///      A constant that represents the type of anchor (DF)
        /// </summary>
        public const string LM_TYPE_DF = "DF";

        /// <summary>
        ///     リンクする直前のAnchorIdが未設定であることを表す定数
        /// </summary>
        public const string ANCHOR_ID_NOT_INITIALIZED = "NotInitialized";



        /// <summary>
        ///     Gets / sets the current anchor Id.
        /// </summary>
        [HideInInspector]
        public string CurrentAnchorId = ANCHOR_ID_NOT_INITIALIZED;

        /// <summary>
        ///     This property manages the acquired Spatial Anchor information.
        /// </summary>
        public Dictionary<string, AnchorInfo> LocatedAnchors = new Dictionary<string, AnchorInfo>();


        /// <summary>
        ///    Gets the root element that contains the GameObject that visualizes the anchor.
        /// </summary>
        public Transform RootPointObjects { get; private set; }

        #region Static Methods

        // Start is called before the first frame update
        public static LandmarkCreateInformation Instance
        {
            get
            {
                var module = FindObjectsOfType<LandmarkCreateInformation>();
                if (module.Length == 1)
                {
                    return module[0];
                }

                Debug.LogWarning(
                    "Not found an existing AnchorModuleScript in your scene. The Anchor Module Script requires only one.");
                return null;
            }
        }

        #endregion

        #region Unity Lifecycle

        /// <summary>
        ///     Perform initialization processing
        /// </summary>
        private void Start()
        {
            RootPointObjects = transform.GetChild(0);
        }

        #endregion

        #region Inner Class

        /// <summary>
        ///     Carrier class that holds anchor information managed internally
        /// </summary>
        public class AnchorInfo
        {
            /// <summary>
            ///     It is a constructor.
            /// </summary>
            /// <param name="appProperties">App Properties set as anchor</param>
            /// <param name="anchorObject">Anchor GemeObject</param>
            // /// <param name="isLinkLineCreated">アンカー同士のリンクの可視化が完了しているか</param>
            public AnchorInfo([NotNull] IDictionary<string, string> appProperties, [NotNull] GameObject anchorObject
               )
            {
                if (appProperties == null) throw new ArgumentNullException(nameof(appProperties));
                if (anchorObject == null) throw new ArgumentNullException(nameof(anchorObject));
                AppProperties = appProperties;
                AnchorObject = anchorObject;
            }

            /// <summary>
            ///     Gets the AppProperties set on the anchor.
            /// </summary>
            public IDictionary<string, string> AppProperties { get; private set; }

            /// <summary>
            ///    Gets the GemeObject of the anchor.
            /// </summary>
            public GameObject AnchorObject { get; }

            /// <summary>
            ///     Discard the object.
            /// </summary>
            public void Destroy()
            {
                DestroyImmediate(AnchorObject);
                AppProperties = null;
            }
        }

        #endregion
    }
}
