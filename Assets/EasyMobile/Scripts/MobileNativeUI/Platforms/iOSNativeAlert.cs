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

#if UNITY_IOS
using System.Runtime.InteropServices;

namespace EasyMobile
{
    internal static class iOSNativeAlert
    {
        [DllImport("__Internal")]
        private static extern void _AlertWithThreeButtons(string title, string message, string button1, string button2, string button3);

        [DllImport("__Internal")]
        private static extern void _AlertWithTwoButtons(string title, string message, string button1, string button2);

        [DllImport("__Internal")]
        private static extern void _Alert(string title, string message, string button);

        internal static void ShowThreeButtonsAlert(string title, string message, string button1, string button2, string button3)
        {
            _AlertWithThreeButtons(title, message, button1, button2, button3);
        }

        internal static void ShowTwoButtonsAlert(string title, string message, string button1, string button2)
        {
            _AlertWithTwoButtons(title, message, button1, button2);
        }

        internal static void ShowOneButtonAlert(string title, string message, string button)
        {
            _Alert(title, message, button);
        }
    }
}
#endif
