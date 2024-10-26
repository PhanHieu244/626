/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_ANDROID
namespace EasyMobile
{
    internal static class AndroidNativeUtility
    {
        private static readonly string ANDROID_JAVA_UTILITY_CLASS = "com.sglib.easymobile.androidnative.EMUtility";

        internal static void RequestRating(RatingDialogContent content, RatingRequestSettings settings)
        {
            AndroidUtil.CallJavaStaticMethod(ANDROID_JAVA_UTILITY_CLASS, "RequestReview",
                content.Title,
                content.Message,
                content.LowRatingMessage,
                content.HighRatingMessage,
                content.PostponeButtonText,
                content.RefuseButtonText,
                content.CancelButtonText,
                content.FeedbackButtonText,
                content.RateButtonText,
                settings.MinimumAcceptedStars.ToString()
            );
        }
    }
}
#endif
