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
    public class IAPSettings
    {
        public IAPAndroidStore TargetAndroidStore { get { return _targetAndroidStore; } }

        public bool IsValidateAppleReceipt { get { return _validateAppleReceipt; } }

        public bool IsValidateGooglePlayReceipt { get { return _validateGooglePlayReceipt; } }

        public IAPProduct[] Products { get { return _products; } }

        [SerializeField]
        private IAPAndroidStore _targetAndroidStore = IAPAndroidStore.GooglePlay;
        [SerializeField]
        private bool _validateAppleReceipt = true;
        [SerializeField]
        private bool _validateGooglePlayReceipt = true;
        [SerializeField]
        private IAPProduct[] _products;
    }
}
