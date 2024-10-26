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
    using Settings = DepthOfFieldModel.Settings;

    [PostProcessingModelEditor(typeof(DepthOfFieldModel))]
    public class DepthOfFieldModelEditor : PostProcessingModelEditor
    {
        SerializedProperty m_FocusDistance;
        SerializedProperty m_Aperture;
        SerializedProperty m_FocalLength;
        SerializedProperty m_UseCameraFov;
        SerializedProperty m_KernelSize;

        public override void OnEnable()
        {
            m_FocusDistance = FindSetting((Settings x) => x.focusDistance);
            m_Aperture = FindSetting((Settings x) => x.aperture);
            m_FocalLength = FindSetting((Settings x) => x.focalLength);
            m_UseCameraFov = FindSetting((Settings x) => x.useCameraFov);
            m_KernelSize = FindSetting((Settings x) => x.kernelSize);
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(m_FocusDistance);
            EditorGUILayout.PropertyField(m_Aperture, EditorGUIHelper.GetContent("Aperture (f-stop)"));

            EditorGUILayout.PropertyField(m_UseCameraFov, EditorGUIHelper.GetContent("Use Camera FOV"));
            if (!m_UseCameraFov.boolValue)
                EditorGUILayout.PropertyField(m_FocalLength, EditorGUIHelper.GetContent("Focal Length (mm)"));

            EditorGUILayout.PropertyField(m_KernelSize);
        }
    }
}
