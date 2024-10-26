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
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using EasyMobile;

namespace EasyMobile.Demo
{
    public class MobileNativeUIDemo : MonoBehaviour
    {

        public GameObject isFirstButtonBool;
        public GameObject isSecondButtonBool;
        public GameObject isThirdButtonBool;

        public DemoUtils demoUtils;

        public void ShowThreeButtonsAlert()
        {
            MobileNativeAlert alert = MobileNativeUI.ShowThreeButtonAlert("Sample Alert", "This is a 3-button alert.", "Button 1", "Button 2", "Button 3");
            if (alert != null)
                alert.OnComplete += OnAlertComplete;
        }

        public void ShowTwoButtonsAlert()
        {
            MobileNativeAlert alert = MobileNativeUI.ShowTwoButtonAlert("Sample Alert", "This is a 2-button alert.", "Button 1", "Button 2");

            if (alert != null)
                alert.OnComplete += OnAlertComplete;
        }

        public void ShowOneButtonAlert()
        {
            MobileNativeAlert alert = MobileNativeUI.Alert("Sample Alert", "This is a simple (1-button) alert.");
            if (alert != null)
                alert.OnComplete += OnAlertComplete;
        }

        public void ShowToast()
        {
            #if UNITY_ANDROID
            MobileNativeUI.ShowToast("This is a sample Android toast");
            #else
            MobileNativeUI.Alert("Alert", "Toasts are available on Android only.");
            #endif
        }

        void OnAlertComplete(int buttonIndex)
        {
            bool isFistButtonClicked = buttonIndex == 0;
            bool isSecondButtonClicked = buttonIndex == 1;
            bool isThirdButtonClicked = buttonIndex == 2;

            if (isFistButtonClicked)
                demoUtils.DisplayBool(isFirstButtonBool, true, "isFirstButtonClicked: TRUE");
            else
                demoUtils.DisplayBool(isFirstButtonBool, false, "isFirstButtonClicked: FALSE");

            if (isSecondButtonClicked)
                demoUtils.DisplayBool(isSecondButtonBool, true, "isSecondButtonClicked: TRUE");
            else
                demoUtils.DisplayBool(isSecondButtonBool, false, "isSecondButtonClicked: FALSE");

            if (isThirdButtonClicked)
                demoUtils.DisplayBool(isThirdButtonBool, true, "isThirdButtonClicked: TRUE");
            else
                demoUtils.DisplayBool(isThirdButtonBool, false, "isThirdButtonClicked: FALSE");
        }
    }
}
