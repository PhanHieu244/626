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
    internal static class AndroidUtil
    {
        internal static void CallJavaStaticMethod(string className, string method, params object[] args)
        {
            AndroidJavaObject activity;
            using (AndroidJavaClass unityCls = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            { 
                activity = unityCls.GetStatic<AndroidJavaObject>("currentActivity");
            }

            // This calling scheme makes sure it works on Unity version as old as 5.3.5.
            AndroidJavaClass targetClass = new AndroidJavaClass(className);
            activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
                    {
                        targetClass.CallStatic(method, args);
                        targetClass.Dispose();
                    }));
        }
    }
}
#endif
