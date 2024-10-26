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
using UnityEngine.SceneManagement;

namespace EasyMobile.Demo
{
    public class DemoUtils : MonoBehaviour
    {
        public Sprite checkedSprite;
        public Sprite uncheckedSprite;

        public void GoHome()
        {
            SceneManager.LoadScene("DemoHome");
        }

        public void DisplayBool(GameObject infoObj, bool state, string msg)
        {
            Image img = infoObj.GetComponentInChildren<Image>();
            Text txt = infoObj.GetComponentInChildren<Text>();

            if (img == null || txt == null)
            {
                Debug.LogError("Could not found Image or Text component beneath object: " + infoObj.name);
            }

            if (state)
            {
                img.sprite = checkedSprite;
                img.color = Color.green;
            }
            else
            {
                img.sprite = uncheckedSprite;
                img.color = Color.red;
            }

            txt.text = msg;
        }

        public void PlayButtonSound()
        {
            SoundManager.Instance.PlaySound(SoundManager.Instance.button);
        }
    }
}
