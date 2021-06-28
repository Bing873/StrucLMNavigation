// Copyright (c) 2020 Takahiro Miyaura
// Released under the MIT license
// http://opensource.org/licenses/mit-license.php

using System.Collections.Generic;
using UnityEngine;

namespace Com.Reseul.ASA.Samples.WayFindings
{
    /// <summary>
    ///     Spatial Anchorの設置に関連して発生するイベントを実装するインターフェースです。
    ///     An interface that implements the events that occur in connection with the installation of the Spatial Anchor.
    /// </summary>
    public interface IASACallBackManager
    {
        /// <summary>
        ///     Spatial Anchorの可視化に必要なオブジェクトの生成を行います。
        ///     Spatial Anchorの設置が完了した場合に発生するイベント内で実行されます。
        ///     Creates the objects required for visualization of Spatial Anchor.
        ///     Executed within the event that occurs when the Spatial Anchor installation is complete.
        /// </summary>
        /// <param name="identifier">AnchorId</param>
        /// <param name="appProperties">Spatial Anchorに含まれるAppProperties; App Properties included in Spatial Anchor</param>
        /// <param name="gameObject">可視化に利用するオブジェクト; Objects used for visualization</param>
        /// <returns>アンカーの設置対象か。trueの場合設置対象; Is it an anchor installation target? If true, installation target</returns>
        bool OnLocatedAnchorObject(string identifier,
            IDictionary<string, string> appProperties, out GameObject gameObject);

        /// <summary>
        ///     Spatial Anchorの設置完了時に実行する処理
        ///     Spatial Anchorの設置がすべて完了した場合に実行されます。
        ///     Process to be executed when the installation of Spatial Anchor is completed
        ///     Executed when all Spatial Anchor installations are complete.
        /// </summary>
        void OnLocatedAnchorComplete();
    }
}