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
using UnityEngine.UI;

namespace EasyMobile.Demo
{
    [AddComponentMenu(""), DisallowMultipleComponent]
    public class ColorHandler : MonoBehaviour
    {
        public float lerpTime = 1;
        public Color[] colors;

        Image imgComp;
        Material material;
        Color currentColor;

        void Start()
        {
            imgComp = GetComponent<Image>();

            var mr = GetComponent<MeshRenderer>();
            if (mr != null)
                material = mr.material;

            StartCoroutine(CRChangeColor(lerpTime));
        }

        IEnumerator CRChangeColor(float time)
        {
            while (true)
            {
                if (material != null)
                    currentColor = material.color;
                else if (imgComp != null)
                    currentColor = imgComp.color;

                Color newColor;
                do
                {
                    newColor = colors[Random.Range(0, colors.Length)];
                }
                while (newColor == currentColor);

                float elapsed = 0;
                while (elapsed < time)
                {
                    elapsed += Time.deltaTime;
                    Color c = Color.Lerp(currentColor, newColor, elapsed / time);

                    if (material != null)
                        material.color = c;
                    else if (imgComp != null)
                        imgComp.color = c;
                    
                    yield return null;
                }
            }
        }
    }
}
