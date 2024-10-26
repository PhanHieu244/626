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

namespace UnityEditor.PostProcessing
{
    [CustomPropertyDrawer(typeof(GetSetAttribute))]
    sealed class GetSetDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var attribute = (GetSetAttribute)base.attribute;

            EditorGUI.BeginChangeCheck();
            EditorGUI.PropertyField(position, property, label);

            if (EditorGUI.EndChangeCheck())
            {
                attribute.dirty = true;
            }
            else if (attribute.dirty)
            {
                var parent = ReflectionUtils.GetParentObject(property.propertyPath, property.serializedObject.targetObject);

                var type = parent.GetType();
                var info = type.GetProperty(attribute.name);

                if (info == null)
                    Debug.LogError("Invalid property name \"" + attribute.name + "\"");
                else
                    info.SetValue(parent, fieldInfo.GetValue(parent), null);

                attribute.dirty = false;
            }
        }
    }
}
