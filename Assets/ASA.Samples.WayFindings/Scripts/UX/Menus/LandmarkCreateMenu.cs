// Copyright (c) 2021 Bing Liu
// Released under the MIT license
// http://opensource.org/licenses/mit-license.php

using System;
using UnityEngine;

namespace Com.Reseul.ASA.Samples.WayFindings.UX.Menus
{
    /// <summary>
    ///     <see cref="LandmarkCreateManager" />need to create
    /// </summary>
    public class LandmarkCreateMenu : BaseMenu
    {
        /// <summary>
        ///     アンカー作成作成モードを表す定数
        /// </summary>
        public static string MODE_CREATE_ANCHOR = "CreateAnchor";

        private GameObject backButton;
        private GameObject createAzureAnchorButton;
        private GameObject createNextPointButton;

        private GameObject nextStepButtons;
        //private GameObject setDestinationButton;

        /// <summary>
        ///     Change menu status
        /// </summary>
        /// <param name="status">Change menu status</param>
        public override void ChangeStatus(string status)
        {
            if (!gameObject.activeSelf)
            {
                gameObject.SetActive(true);
            }

            if (status.Equals(MODE_INITIALIZE))
            {
                nextStepButtons.SetActive(false);
                createNextPointButton.SetActive(true);               
                createAzureAnchorButton.SetActive(false);
                backButton.SetActive(true);
            }
            else if (status.Equals(MODE_CREATE_ANCHOR))
            {
                nextStepButtons.SetActive(false);
                createNextPointButton.SetActive(false);                
                createAzureAnchorButton.SetActive(true);
                backButton.SetActive(true);
            }
            else if (status.Equals(MODE_COMPLETE))
            {
                nextStepButtons.SetActive(true);
                createNextPointButton.SetActive(false);                
                createAzureAnchorButton.SetActive(false);
                backButton.SetActive(false);
            }
            else if (status.Equals(MODE_CLOSE))
            {
                gameObject.SetActive(false);
            }
            else
            {
                throw new InvalidOperationException($"Not exits status.Status Code:{status}");
            }
        }

        /// <summary>
        ///     メニューに必要な初期化処理を実行します。
        /// </summary>
        protected override void Initialize()
        {
            nextStepButtons = transform.GetChild(1).gameObject;
            createNextPointButton = transform.GetChild(0).GetChild(0).gameObject;
            createAzureAnchorButton = transform.GetChild(0).GetChild(1).gameObject;
            backButton = transform.GetChild(0).GetChild(2).gameObject;
            Debug.Log("LCMenu is called");
        }
    }
}