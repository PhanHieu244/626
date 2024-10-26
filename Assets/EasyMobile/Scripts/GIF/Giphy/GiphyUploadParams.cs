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
    public struct GiphyUploadParams
    {
        /// <summary>
        /// The local image filepath, required if no sourceImageUrl supplied.
        /// If both localImagePath and sourceImageUrl are supplied, the local file
        /// will be used over the sourceImageUrl.
        /// </summary>
        public string localImagePath;
        /// <summary>
        /// The URL for the image to be uploaded, required if no localImagePath specified.
        /// If both localImagePath and sourceImageUrl are supplied, the local file
        /// will be used over the sourceImageUrl.
        /// </summary>
        public string sourceImageUrl;
        /// <summary>
        /// [Optional] Comma-delimited list of tags.
        /// </summary>
        public string tags;
        /// <summary>
        /// [Optional] The source of the asset.
        /// </summary>
        public string sourcePostUrl;
        /// <summary>
        /// [Optional] If set to true, the uploaded image will be marked as private (only visible to the uploader).
        /// </summary>
        public bool isHidden;
    }
}
