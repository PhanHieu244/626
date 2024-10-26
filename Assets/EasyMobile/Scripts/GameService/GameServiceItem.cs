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
    public class GameServiceItem
    {
        public string Name { get { return _name; } }

        public string IOSId { get { return _iosId; } }

        public string AndroidId { get { return _androidId; } }

        public string Id
        {
            get
            {
                #if UNITY_IOS
                return _iosId;
                #elif UNITY_ANDROID
                return _androidId;
                #else
                return null;
                #endif
            }
        }

        [SerializeField]
        string _name;
        [SerializeField]
        string _iosId;
        [SerializeField]
        string _androidId;

        public GameServiceItem(string name, string iosId, string androidId)
        {
            this._name = name;
            this._iosId = iosId;
            this._androidId = androidId;
        }
    }

    [System.Serializable]
    public class Leaderboard : GameServiceItem
    {
        public Leaderboard(string name, string iosId, string androidId)
            : base(name, iosId, androidId)
        {
        }
    }

    [System.Serializable]
    public class Achievement : GameServiceItem
    {
        public Achievement(string name, string iosId, string androidId)
            : base(name, iosId, androidId)
        {
        }
    }
}
