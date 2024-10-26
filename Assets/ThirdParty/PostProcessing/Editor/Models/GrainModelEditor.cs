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
    using Settings = GrainModel.Settings;

    [PostProcessingModelEditor(typeof(GrainModel))]
    public class GrainModelEditor : PostProcessingModelEditor
    {
        SerializedProperty m_Colored;
        SerializedProperty m_Intensity;
        SerializedProperty m_Size;
        SerializedProperty m_LuminanceContribution;

        public override void OnEnable()
        {
            m_Colored = FindSetting((Settings x) => x.colored);
            m_Intensity = FindSetting((Settings x) => x.intensity);
            m_Size = FindSetting((Settings x) => x.size);
            m_LuminanceContribution = FindSetting((Settings x) => x.luminanceContribution);
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(m_Intensity);
            EditorGUILayout.PropertyField(m_LuminanceContribution);
            EditorGUILayout.PropertyField(m_Size);
            EditorGUILayout.PropertyField(m_Colored);
        }
    }
}
