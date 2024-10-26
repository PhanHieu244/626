/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using UnityEngine;
using System.Collections;

namespace EasyMobile
{
    public static class MobileNativeUI
    {
        #region Alerts

        /// <summary>
        /// Shows an alert with 3 buttons.
        /// </summary>
        /// <returns>The three buttons alert.</returns>
        /// <param name="title">Title.</param>
        /// <param name="message">Message.</param>
        /// <param name="button1">Button1.</param>
        /// <param name="button2">Button2.</param>
        /// <param name="button3">Button3.</param>
        public static MobileNativeAlert ShowThreeButtonAlert(string title, string message, string button1, string button2, string button3)
        {
            return MobileNativeAlert.ShowThreeButtonAlert(title, message, button1, button2, button3);
        }

        /// <summary>
        /// Shows an alert with 2 buttons.
        /// </summary>
        /// <returns>The two buttons alert.</returns>
        /// <param name="title">Title.</param>
        /// <param name="message">Message.</param>
        /// <param name="button1">Button1.</param>
        /// <param name="button2">Button2.</param>
        public static MobileNativeAlert ShowTwoButtonAlert(string title, string message, string button1, string button2)
        {
            return MobileNativeAlert.ShowTwoButtonAlert(title, message, button1, button2);
        }

        /// <summary>
        /// Shows a one-button alert with a custom button label.
        /// </summary>
        /// <returns>The one button alert.</returns>
        /// <param name="title">Title.</param>
        /// <param name="message">Message.</param>
        /// <param name="button">Button.</param>
        public static MobileNativeAlert Alert(string title, string message, string button)
        {
            return MobileNativeAlert.ShowOneButtonAlert(title, message, button);
        }

        /// <summary>
        /// Shows a one-button alert with the default "OK" button.
        /// </summary>
        /// <returns>The alert.</returns>
        /// <param name="title">Title.</param>
        /// <param name="message">Message.</param>
        public static MobileNativeAlert Alert(string title, string message)
        {
            return MobileNativeAlert.Alert(title, message);
        }

        #endregion


        #region Android Toasts

        #if UNITY_ANDROID
        /// <summary>
        /// Shows a toast message (Android only).
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="isLongToast">If set to <c>true</c> use long-length toast, otherwise use short-length toast.</param>
        public static void ShowToast(string message, bool isLongToast = false)
        {   
            MobileNativeAlert.ShowToast(message, isLongToast);
        }
        #endif

        #endregion
    }
}
