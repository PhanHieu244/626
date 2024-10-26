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
using UnityEngine.UI;

public class Shooter : MonoBehaviour {

    private const float BULLET_SPEED = 270;

    public static Shooter Instance;

    [SerializeField]
    private Image[] m_imgBullets;
    [SerializeField]
    private Image[] m_imgHealth;
    [SerializeField]
    private AudioClip[] m_shotClips;
    [SerializeField]
    private AudioClip[] m_ricochetClips;
    [SerializeField]
    private AudioClip[] m_reloadingBulletClips;
    [SerializeField]
    private AudioClip m_reloadingSpinClip;
    [SerializeField]
    private AudioClip m_healthClip;
    [SerializeField]
    private GameObject[] m_particlesRef;

    private CameraShake m_shake;
    
    private int _magSize {
        get {
            return m_imgBullets.Length;
        }
    }

    private int _maxHealth {
        get {
            return m_imgHealth.Length;
        }
    }
    private int m_health;

    private int m_shotsFired;
    private bool m_isReloading;

    private AudioSource[] m_sources;

    private bool m_isDead;

    private int _sourceIndex;
    private int m_sourceIndex {
        set {
            if (value >= m_sources.Length) {
                _sourceIndex = 0;
            } else {
                _sourceIndex = value;
            }
        }
        get {
            return _sourceIndex;
        }
    }

    private void Awake() {
        Instance = this;
        m_isReloading = false;
        m_shotsFired = 0;
        m_sources = GetComponents<AudioSource>();
        m_sourceIndex = 0;
        m_health = _maxHealth / 2;
        UpdateUIHealth();
        m_shake = GetComponent<CameraShake>();
        m_shake.enabled = false;
    }

    private void Update() {
        if (m_isDead) {
            return;
        }
        if (m_isReloading) {
            return;
        }
        if (Input.GetMouseButtonDown(0)) {
            Shoot();
        } else if (Input.GetMouseButtonDown(1)) {
            StartCoroutine(Reload());
        }
    }

    public void ForceReload() {
        StartCoroutine(Reload());
    }

    private IEnumerator Reload() {
        if (m_shotsFired > 0) {
            m_isReloading = true;
            for (int i = _magSize - m_shotsFired; i < _magSize; i++) {
                yield return new WaitForSeconds(0.42f);
                PlayReloadSound();
                m_imgBullets[i].enabled = true;
            }
            yield return new WaitForSeconds(0.42f);
            PlayReloadSound(true);
            m_shotsFired = 0;
            m_isReloading = false;
        }
    }

    private void Shoot() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {
            if (hit.collider.tag == "Badge") {
                Destroy(hit.collider.gameObject);
                ChangeHealth(1);
                PlayHealthSound();
            } else if (hit.collider.tag == "Enemy") {
                Enemy en = hit.collider.GetComponent<Enemy>();
                GameObject go = GameObject.Instantiate(m_particlesRef[UnityEngine.Random.Range(0, m_particlesRef.Length)]);
                go.transform.position = hit.point;
                en.OnHit();
            } else {
                float time = hit.distance / BULLET_SPEED;
                StartCoroutine(PlayRicochetSound(hit.point, time));
            }
        }

        m_shotsFired++;
        PlayShotSound();
        DisableUIBullets();
        if (m_shotsFired >= _magSize) {
            StartCoroutine(Reload());
        }
    }

    private void PlayHealthSound() {
        m_sources[m_sourceIndex].clip = m_healthClip;
        if (m_sources[m_sourceIndex].isPlaying) {
            Debug.Log("atropelando som!");
        }
        m_sources[m_sourceIndex].Play();
        m_sourceIndex++;
    }

    private IEnumerator PlayRicochetSound(Vector3 pos, float t) {
        GameObject go = new GameObject();
        go.transform.position = pos;
        AudioSource asr = go.AddComponent<AudioSource>();
        asr.playOnAwake = false;
        asr.clip = m_ricochetClips[UnityEngine.Random.Range(0, m_ricochetClips.Length)];
        asr.PlayDelayed(t);
        yield return new WaitForSeconds(t);
        do {
            yield return null;
        } while (asr.isPlaying);
        Destroy(go);
    }

    private void PlayShotSound() {
        m_sources[m_sourceIndex].clip = m_shotClips[UnityEngine.Random.Range(0, m_shotClips.Length)];
        if (m_sources[m_sourceIndex].isPlaying) {
            Debug.Log("atropelando som!");
        }
        m_sources[m_sourceIndex].Play();
        m_sourceIndex++;
    }

    private void PlayReloadSound(bool isFinished = false) {
        if (isFinished) {
            m_sources[m_sourceIndex].clip = m_reloadingSpinClip;
        } else {
            m_sources[m_sourceIndex].clip = m_reloadingBulletClips[UnityEngine.Random.Range(0, m_reloadingBulletClips.Length)];
        }
        if (m_sources[m_sourceIndex].isPlaying) {
            Debug.Log("atropelando som!");
        }
        m_sources[m_sourceIndex].Play();
        m_sourceIndex++;
    }

    private void DisableUIBullets() {
        for (int i = _magSize - 1; i >= _magSize - m_shotsFired; i--) {
            m_imgBullets[i].enabled = false;
        }
    }

    private void ChangeHealth(int value) {
        m_health += value;
        if (m_health > _maxHealth) {
            m_health = _maxHealth;
        }

        if (m_health <= 0) {
            GameController.Instance.GameOver();
            m_isDead = true;
        }
        UpdateUIHealth();
    }

    private void UpdateUIHealth() {
        if (m_health < 0) {
            return;
        }

        for (int i = 0; i < m_health; i++) {
            m_imgHealth[i].enabled = true;
        }
        for (int i = _maxHealth - 1; i >= m_health; i--) {
            m_imgHealth[i].enabled = false;
        }
    }

    public void OnWasHit() {
        ChangeHealth(-1);
        Shake(0.1f);
    }

    public void Shake(float t) {
        StartCoroutine(ShakeRoutine(t));
    }

    private IEnumerator ShakeRoutine(float t) {
        m_shake.enabled = true;
        m_shake.shakeDuration = t;
        yield return new WaitForSeconds(t);
        yield return new WaitForEndOfFrame();
        m_shake.enabled = false;
    }

    public void HideAllUI() {
        for (int i = 0; i < m_imgBullets.Length; i++) {
            m_imgBullets[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < m_imgHealth.Length; i++) {
            m_imgHealth[i].gameObject.SetActive(false);
        }
    }
}
