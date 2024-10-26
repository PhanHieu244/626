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
using System;

namespace EasyMobile
{
    internal class GifExportTask
    {
        internal int taskId;
        internal AnimatedClip clip;
        internal Color32[][] imageData;
        internal string filepath;
        internal int loop;
        /// <summary>
        /// The sample factor used in the color quantization algorithm,
        /// determining the color quality of the output GIF.
        /// </summary>
        internal int sampleFac;
        internal bool isExporting;
        internal bool isDone;
        internal float progress;
        internal Action<AnimatedClip, float> exportProgressCallback;
        internal Action<AnimatedClip, string> exportCompletedCallback;
        internal System.Threading.ThreadPriority workerPriority;
    }
}

