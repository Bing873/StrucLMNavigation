// Copyright (c) 2020 Takahiro Miyaura
// Released under the MIT license
// http://opensource.org/licenses/mit-license.php

using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Com.Reseul.ASA.Samples.WayFindings.SpatialAnchors
{
    /// <summary>
    ///     Interface implemented in management class to execute Azure Spatial Anchors processing
    /// </summary>
    public interface IAnchorModuleScript
    {
        /// <summary>
        ///    Controller class with individual processing to be executed after anchor acquisition
        /// </summary>
        IASACallBackManager CallBackManager { set; get; }

        /// <summary>
        ///     Perform initialization processing
        /// </summary>
        void Start();

        /// <summary>
        ///     Performs the process to be executed for each frame.
        /// </summary>
        void Update();

        /// <summary>
        ///     Performs post-processing (discard) of the object.
        /// </summary>
        void OnDestroy();

        /// <summary>
        ///     Connect to the Azure Spatial Anchors service and start the session.
        /// </summary>
        /// <returns></returns>
        Task StartAzureSession();

        /// <summary>
        ///     Stops the connection with the zure Spatial Anchors service.
        /// </summary>
        /// <returns></returns>
        Task StopAzureSession();

        /// <summary>
        ///     Change the App Properties of Spatial Anchor obtained from Azure Spatial Anchors at once.
        ///     If the key already exists, it will be replaced according to
        ///     the value of the replace parameter, and the appending will be switched to perform the process.
        /// </summary>
        /// <param name="key">AppProperties keyÅ[</param>
        /// <param name="val">The value corresponding to the key</param>
        /// <param name="replace">true: overwrite, false: comma separated</param>
        void UpdatePropertiesAll(string key, string val, bool replace = true);

        /// <summary>
        ///     Sets the search range for Spatial Anchor.
        /// </summary>
        /// <param name="distanceInMeters">Search range (unit: m)</param>
        void SetDistanceInMeters(float distanceInMeters);

        /// <summary>
        ///     Sets the number of simultaneous searches for Spatial Anchor.
        /// </summary>
        /// <param name="distanceInMeters">åüçıêî</param>
        void SetMaxResultCount(int maxResultCount);

        /// <summary>
        ///     Sets the life of the Spatial Anchor
        /// </summary>
        /// <param name="expiration">Anchor registration period (unit: days)</param>
        void SetExpiration(int expiration);

        /// <summary>
        ///    Add an anchor to the Azure Spatial Anchors service.
        /// </summary>
        /// <param name="theObject">Objects installed in real space to be registered as Spatial Anchor information</param>
        /// <param name="appProperties">Information to include in Spatial Anchor</param>
        /// <returns>AnchorId at the time of registration</returns>
        Task<string> CreateAzureAnchor(GameObject theObject, IDictionary<string, string> appProperties);

        /// <summary>
        ///     Searches for the existence of other anchors centered on the Anchor registered with the specified AnchorId.
        /// </summary>
        /// <param name="anchorId">Reference AnchorId</param>
        void FindNearByAnchor(string anchorId);

        /// <summary>
        ///     Searches for the existence of other anchors centered on the Anchor registered with the specified AnchorId.
        /// </summary>
        /// <param name="anchorId">Reference AnchorId</param>
        void FindAzureAnchorById(params string[] azureAnchorIds);

        /// <summary>
        ///     Remove all acquired anchors from the Azure Spatial Anchors service.
        /// </summary>
        void DeleteAllAzureAnchor();

        /// <summary>
        ///     èEvent to output the processing status
        /// </summary>
        event AnchorModuleProxy.FeedbackDescription OnFeedbackDescription;

        /// <summary>
        ///     Set the controller to execute the Anchor generation process.
        /// </summary>
        /// <param name="iasaCallBackManager"></param>
        void SetASACallBackManager(IASACallBackManager iasaCallBackManager);
    }
}