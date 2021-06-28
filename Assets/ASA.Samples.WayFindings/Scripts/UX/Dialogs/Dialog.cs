﻿// Copyright (c) 2020 Takahiro Miyaura
// Released under the MIT license
// http://opensource.org/licenses/mit-license.php

using UnityEngine;
using UnityEngine.Events;

namespace Com.Reseul.ASA.Samples.WayFindings.UX.Dialogs
{
    /// <summary>
    ///     ダイアログ出力のためのユーティリティクラス
    ///     Utility class for dialog output
    /// </summary>
    public class Dialog : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Set the dialog prefab to be used to display.")]
        private SimpleDialog dialog = null;

        /// <summary>
        ///     このクラスのインスタンスを取得します。
        ///     Gets an instance of this class.
        /// </summary>
        private static Dialog Instance
        {
            get
            {
                var module = FindObjectsOfType<Dialog>();
                if (module.Length == 1)
                {
                    return module[0];
                }

                Debug.LogWarning(
                    "Not found an existing Dialog in your scene. The Dialog requires only one.");
                return null;
            }
        }

        /// <summary>
        ///     ダイアログを表示します
        ///     Display a dialog
        /// </summary>
        /// <param name="title">title</param>
        /// <param name="message">message</param>
        /// <param name="buttonLabels">Button label. Set up to 2 elements.</param>
        /// <param name="events">Event when the button is pressed</param>
        public static void OpenDialog(string title, string message, string[] buttonLabels, UnityAction[] events = null)
        {
            var dialogObj = Instantiate(Instance.dialog);
            dialogObj.SetDialog(title, message, buttonLabels, events);
            dialogObj.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 1.5f;
        }
    }
}