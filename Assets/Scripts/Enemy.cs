/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    protected Rigidbody m_rigidbody;
    protected Transform m_targetTransform;
    protected BoxCollider m_collider;
    protected Animation m_anim;

    [SerializeField]
    protected Material[] m_materials;

    [SerializeField]
    protected SkinnedMeshRenderer m_skinnedMesh;

    [SerializeField]
    protected float speed;

    protected bool isDead;

    protected virtual void Awake() {
        m_anim = GetComponent<Animation>();
        m_rigidbody = GetComponent<Rigidbody>();
        m_collider = GetComponent<BoxCollider>();
        m_targetTransform = Camera.main.transform;
        m_skinnedMesh.material = m_materials[UnityEngine.Random.Range(0, m_materials.Length)];
    }

    protected virtual void Update () {
        if (isDead) {
            return;
        }
        if (Vector3.Distance(transform.position, m_targetTransform.position) < 5) {
            Shooter.Instance.OnWasHit();
            Destroy(this.gameObject);
            return;
        }
        Vector3 relative_dir = (m_targetTransform.position - transform.position);
        Vector3 direction = relative_dir.normalized;
        transform.position += direction * speed * Time.deltaTime;
        transform.LookAt(m_targetTransform.position);
	}

    public virtual void OnHit() {
        isDead = true;
        m_anim.CrossFade("Death");
        StartCoroutine(WaitForFall());
    }

    protected virtual IEnumerator WaitForFall() {
        do {
            yield return null;
        } while (m_rigidbody.velocity.y < 0);
        Destroy(m_rigidbody);
        m_collider.enabled = false;

        float count = 0;
        do {
            yield return new WaitForEndOfFrame();
            count += Time.deltaTime;

            m_skinnedMesh.material.SetColor("_Color", new Color(1, 1, 1, 1 - count));
        } while (count < 1);
        m_skinnedMesh.material.SetColor("_Color", new Color(1, 1, 1, 0));
        Destroy(this.gameObject);
    }
}
