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
    internal static class iOSNativeUtility
    {
        [DllImport("__Internal")]
        [return:MarshalAs(UnmanagedType.Bool)]
        private static extern bool _IsBuiltinRequestReviewAvail();

        [DllImport("__Internal")]
        private static extern void _RequestReview();

        internal static bool CanUseBuiltinRequestReview()
        {
            return _IsBuiltinRequestReviewAvail();
        }

        internal static void RequestReview()
        {
            _RequestReview();
        }
    }
}
#endif
