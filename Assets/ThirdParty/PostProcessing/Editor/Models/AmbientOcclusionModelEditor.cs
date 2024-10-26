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
    using Settings = AmbientOcclusionModel.Settings;

    [PostProcessingModelEditor(typeof(AmbientOcclusionModel))]
    public class AmbientOcclusionModelEditor : PostProcessingModelEditor
    {
        SerializedProperty m_Intensity;
        SerializedProperty m_Radius;
        SerializedProperty m_SampleCount;
        SerializedProperty m_Downsampling;
        SerializedProperty m_ForceForwardCompatibility;
        SerializedProperty m_AmbientOnly;
        SerializedProperty m_HighPrecision;

        public override void OnEnable()
        {
            m_Intensity = FindSetting((Settings x) => x.intensity);
            m_Radius = FindSetting((Settings x) => x.radius);
            m_SampleCount = FindSetting((Settings x) => x.sampleCount);
            m_Downsampling = FindSetting((Settings x) => x.downsampling);
            m_ForceForwardCompatibility = FindSetting((Settings x) => x.forceForwardCompatibility);
            m_AmbientOnly = FindSetting((Settings x) => x.ambientOnly);
            m_HighPrecision = FindSetting((Settings x) => x.highPrecision);
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(m_Intensity);
            EditorGUILayout.PropertyField(m_Radius);
            EditorGUILayout.PropertyField(m_SampleCount);
            EditorGUILayout.PropertyField(m_Downsampling);
            EditorGUILayout.PropertyField(m_ForceForwardCompatibility);
            EditorGUILayout.PropertyField(m_HighPrecision, EditorGUIHelper.GetContent("High Precision (Forward)"));

            using (new EditorGUI.DisabledGroupScope(m_ForceForwardCompatibility.boolValue))
                EditorGUILayout.PropertyField(m_AmbientOnly, EditorGUIHelper.GetContent("Ambient Only (Deferred + HDR)"));
        }
    }
}
