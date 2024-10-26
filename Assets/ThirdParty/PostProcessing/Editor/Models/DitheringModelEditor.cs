/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using UnityEngine.PostProcessing;

namespace UnityEditor.PostProcessing
{
    [PostProcessingModelEditor(typeof(DitheringModel))]
    public class DitheringModelEditor : PostProcessingModelEditor
    {
        public override void OnInspectorGUI()
        {
            if (profile.grain.enabled && target.enabled)
                EditorGUILayout.HelpBox("Grain is enabled, you probably don't need dithering !", MessageType.Warning);
            else
                EditorGUILayout.HelpBox("Nothing to configure !", MessageType.Info);
        }
    }
}
