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
    [System.Serializable]
    public class NotificationSettings
    {
        public bool IsAutoInit { get { return _autoInit; } }

        public float AutoInitDelay { get { return _autoInitDelay; } }

        public string OneSignalAppId { get { return _oneSignalAppId; } }

        // Auto-init config
        [SerializeField]
        private bool _autoInit = true;
        [SerializeField]
        private float _autoInitDelay = 0f;

        // App credentials
        [SerializeField]
        private string _oneSignalAppId;
    }
}

