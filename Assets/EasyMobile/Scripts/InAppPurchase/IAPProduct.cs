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
    public enum IAPProductType
    {
        Consumable,
        NonConsumable,
        Subscription
    }

    [System.Serializable]
    public class IAPProduct
    {
        /// <summary>
        /// Product name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get { return _name; } }

        /// <summary>
        /// The unified Id of the product.
        /// </summary>
        /// <value>The identifier.</value>
        public string Id { get { return _id; } }

        /// <summary>
        /// Product type.
        /// </summary>
        /// <value>The type.</value>
        public IAPProductType Type { get { return _type; } }

        /// <summary>
        /// Product price.
        /// </summary>
        /// <value>The price.</value>
        public string Price { get { return _price; } }

        /// <summary>
        /// Product description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get { return _description; } }

        /// <summary>
        /// Store-specific product Ids, these Ids if given will override the unified Id for the corresponding stores.
        /// </summary>
        /// <value>The store specific identifiers.</value>
        public StoreSpecificId[] StoreSpecificIds { get { return _storeSpecificIds; } }

        // Required
        [SerializeField]
        private string _name;
        [SerializeField]
        private IAPProductType _type = IAPProductType.Consumable;
        [SerializeField]
        private string _id;

        // Optional
        [SerializeField]
        private string _price;
        [SerializeField]
        private string _description;
        [SerializeField]
        private StoreSpecificId[] _storeSpecificIds;

        #if UNITY_EDITOR
        // Editor-use via reflection only, hence the warning suppression.
        #pragma warning disable 0414
        [SerializeField]
        private bool _isEditingAdvanced = false;
        #pragma warning restore 0414
        #endif

        [System.Serializable]
        public class StoreSpecificId
        {
            public IAPStore store;
            public string id;
        }
    }
}

