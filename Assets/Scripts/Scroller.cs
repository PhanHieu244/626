/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour {

    [SerializeField]
    private float speed;

    private RectTransform m_rect;

    private void Start() {
        m_rect = GetComponent<RectTransform>();
        m_rect.transform.localPosition = new Vector3(0, -700, 0);
    }

    private void Update () {
        if (m_rect.transform.localPosition.y < 2371.58f) {
            transform.position += Vector3.up * speed * Time.deltaTime;
        }
    }
}
