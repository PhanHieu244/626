/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using UnityEngine;

namespace UnityEditor.PostProcessing
{
    using UnityObject = Object;

    static class EditorResources
    {
        static string m_EditorResourcesPath = string.Empty;

        internal static string editorResourcesPath
        {
            get
            {
                if (string.IsNullOrEmpty(m_EditorResourcesPath))
                {
                    string path;

                    if (SearchForEditorResourcesPath(out path))
                        m_EditorResourcesPath = path;
                    else
                        Debug.LogError("Unable to locate editor resources. Make sure the PostProcessing package has been installed correctly.");
                }

                return m_EditorResourcesPath;
            }
        }

        internal static T Load<T>(string name)
            where T : UnityObject
        {
            return AssetDatabase.LoadAssetAtPath<T>(editorResourcesPath + name);
        }

        static bool SearchForEditorResourcesPath(out string path)
        {
            path = string.Empty;

            string searchStr = "/PostProcessing/Editor Resources/";
            string str = null;

            foreach (var assetPath in AssetDatabase.GetAllAssetPaths())
            {
                if (assetPath.Contains(searchStr))
                {
                    str = assetPath;
                    break;
                }
            }

            if (str == null)
                return false;

            path = str.Substring(0, str.LastIndexOf(searchStr) + searchStr.Length);
            return true;
        }
    }
}
