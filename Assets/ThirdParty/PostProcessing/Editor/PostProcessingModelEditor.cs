/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using UnityEngine;
using UnityEngine.PostProcessing;
using System;
using System.Linq.Expressions;

namespace UnityEditor.PostProcessing
{
    public class PostProcessingModelEditor
    {
        public PostProcessingModel target { get; internal set; }
        public SerializedProperty serializedProperty { get; internal set; }

        protected SerializedProperty m_SettingsProperty;
        protected SerializedProperty m_EnabledProperty;

        internal bool alwaysEnabled = false;
        internal PostProcessingProfile profile;
        internal PostProcessingInspector inspector;

        internal void OnPreEnable()
        {
            m_SettingsProperty = serializedProperty.FindPropertyRelative("m_Settings");
            m_EnabledProperty = serializedProperty.FindPropertyRelative("m_Enabled");

            OnEnable();
        }

        public virtual void OnEnable()
        {}

        public virtual void OnDisable()
        {}

        internal void OnGUI()
        {
            GUILayout.Space(5);

            var display = alwaysEnabled
                ? EditorGUIHelper.Header(serializedProperty.displayName, m_SettingsProperty, Reset)
                : EditorGUIHelper.Header(serializedProperty.displayName, m_SettingsProperty, m_EnabledProperty, Reset);

            if (display)
            {
                EditorGUI.indentLevel++;
                using (new EditorGUI.DisabledGroupScope(!m_EnabledProperty.boolValue))
                {
                    OnInspectorGUI();
                }
                EditorGUI.indentLevel--;
            }
        }

        void Reset()
        {
            var obj = serializedProperty.serializedObject;
            Undo.RecordObject(obj.targetObject, "Reset");
            target.Reset();
            EditorUtility.SetDirty(obj.targetObject);
        }

        public virtual void OnInspectorGUI()
        {}

        public void Repaint()
        {
            inspector.Repaint();
        }

        protected SerializedProperty FindSetting<T, TValue>(Expression<Func<T, TValue>> expr)
        {
            return m_SettingsProperty.FindPropertyRelative(ReflectionUtils.GetFieldPath(expr));
        }

        protected SerializedProperty FindSetting<T, TValue>(SerializedProperty prop, Expression<Func<T, TValue>> expr)
        {
            return prop.FindPropertyRelative(ReflectionUtils.GetFieldPath(expr));
        }
    }
}
