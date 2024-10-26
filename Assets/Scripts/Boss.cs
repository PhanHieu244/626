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

public class Boss : Enemy {
    [SerializeField]
    private int Life;

    [SerializeField]
    private GameObject m_explosionParticle;

    [SerializeField]
    private GameObject m_starship;


    private AudioSource m_source;

    protected override void Awake() {
        base.Awake();
        m_source = GetComponent<AudioSource>();
        StartCoroutine(ChangeColorRoutine());
        
    }

    private IEnumerator ChangeColorRoutine() {
        int i = 0;
        do {
            m_skinnedMesh.material = m_materials[i];
            yield return new WaitForSeconds(1f);
            i++;
            if (i >= m_materials.Length) {
                i = 0;
            }
        } while (!isDead);
    }

    public override void OnHit() {
        if (Life <=0) {
            return;
        }
        Life--;

        if (Life <= 0) {
            StartCoroutine(DeathRoutine());
        }
    }

    private IEnumerator DeathRoutine() {
        isDead = true;
        m_anim.CrossFade("Death");
        yield return new WaitForSeconds(1f);
        GameObject ex = GameObject.Instantiate(m_explosionParticle);
        GameObject ex2 = GameObject.Instantiate(m_explosionParticle);
        GameObject ex3 = GameObject.Instantiate(m_explosionParticle);
        GameObject ex4 = GameObject.Instantiate(m_explosionParticle);
        GameObject ex5 = GameObject.Instantiate(m_explosionParticle);
        ex.transform.position = m_starship.transform.position + Vector3.up;
        ex2.transform.position = m_starship.transform.position + Vector3.forward;
        ex3.transform.position = m_starship.transform.position + Vector3.back;
        ex4.transform.position = m_starship.transform.position + Vector3.left;
        ex5.transform.position = m_starship.transform.position + Vector3.right;

        yield return new WaitForSeconds(0.2f);
        Shooter.Instance.Shake(3);
        m_source.Play();
        float count = 0;
        do {
            yield return new WaitForEndOfFrame();
            count += Time.deltaTime;

            m_skinnedMesh.material.SetColor("_Color", new Color(1, 1, 1, 1 - count));
        } while (count < 1);
        m_skinnedMesh.material.SetColor("_Color", new Color(1, 1, 1, 0));
        yield return new WaitForSeconds(1.4f);
        Destroy(m_starship);
        yield return new WaitForSeconds(5);
        GameController.Instance.RollCredits();
        Destroy(this.gameObject);
        //explode boss, explode ship after a few seconds, play both sounds.
    }
}
