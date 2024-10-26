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
using AcidTrip;
using TMPro;

public class GameController : MonoBehaviour {

    public static GameController Instance;

    public bool HasGameStarted;

    [SerializeField]
    private Transform[] m_cameraPositions;
    private int m_cameraIndex;

    [SerializeField]
    private GameObject m_player;

    [SerializeField]
    private Transform[] m_spawn1positions;
    [SerializeField]
    private Transform[] m_spawn2positions;

    [SerializeField]
    private Enemy m_enemyRef;

    [SerializeField]
    private Boss m_boss;

    private AcidTrip.AcidTrip m_acid;

    [SerializeField]
    private Scroller m_scroller;

    [SerializeField]
    private GameObject m_gameOverText;

    [SerializeField]
    private GameObject m_crosshair;

    private void Awake() {
        Instance = this;
        HasGameStarted = false;
        m_cameraIndex = 0;
    }

    private void Start() {
        m_acid = Shooter.Instance.GetComponent<AcidTrip.AcidTrip>();
        m_acid.enabled = false;
    }

    public void OnClickPlay() {
        HasGameStarted = true;
        StartCoroutine(Scene1());

        //manda a câmera pra posição 1
        //quando chegar, destrói o player
        //começa a spawnar inimigos na posição 1
        //espera todos os inimigos morrerem pra passar pra posição 2
    }

    private IEnumerator Scene1() {
        Shooter.Instance.ForceReload();
        StartCoroutine(MoveCameraToPosition(3f));
        yield return new WaitForSeconds(2.1f);
        m_player.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.9f);

        //camera has arrived to position
        int i = 0;
        do {
            Enemy en = Enemy.Instantiate(m_enemyRef);
            en.transform.position = m_spawn1positions[UnityEngine.Random.Range(0, m_spawn1positions.Length)].position;

            yield return new WaitForSeconds(1f);
            i++;
        } while (i < 3);
        //3 seconds have passed
        yield return new WaitForSeconds(3f);
        //6 seconds have passed
        i = 0;
        do {
            Enemy en = Enemy.Instantiate(m_enemyRef);
            en.transform.position = m_spawn1positions[UnityEngine.Random.Range(0, m_spawn1positions.Length)].position;

            yield return new WaitForSeconds(1f);
            i++;
        } while (i < 5);
        //11 seconds have passed
        yield return new WaitForSeconds(3f);
        //14 seconds have passed
        Enemy en2 = Enemy.Instantiate(m_enemyRef);
        en2.transform.position = m_spawn1positions[UnityEngine.Random.Range(0, m_spawn1positions.Length)].position;

        yield return new WaitForSeconds(1f);
        //15 seconds have passed

        StartCoroutine(Scene2());
    }

    private IEnumerator Scene2() {
        m_cameraIndex++;
        m_acid.enabled = true;
        StartCoroutine(MoveCameraToPosition(3));
        yield return new WaitForSeconds(3);

        //camera has arrived.
        int i = 0;
        do {
            Enemy en = Enemy.Instantiate(m_enemyRef);
            en.transform.position = m_spawn2positions[UnityEngine.Random.Range(0, m_spawn2positions.Length)].position;
            i++;
            yield return new WaitForSeconds(1f);
        } while (i < 6);
        //6 seconds have passed
        yield return new WaitForSeconds(4);
        //10 seconds have passed
        i = 0;
        do {
            Enemy en = Enemy.Instantiate(m_enemyRef);
            en.transform.position = m_spawn2positions[UnityEngine.Random.Range(0, m_spawn2positions.Length)].position;
            i++;
            yield return new WaitForSeconds(1f);
        } while (i < 4);
        //14 seconds have passed
        yield return new WaitForSeconds(1);
        //15 seconds have passed
        StartCoroutine(Scene3());
    }

    private IEnumerator Scene3() {
        m_cameraIndex++;
        StartCoroutine(MoveCameraToPosition(3));
        yield return new WaitForSeconds(3);
        //instantiate big boss
        //Lost
        StartCoroutine(LostRoutine());

        m_boss.gameObject.SetActive(true);
    }

    private IEnumerator LostRoutine() {
        yield return null;
        m_acid.Wavelength = 1f;
        m_acid.DistortionStrength = 0.6f;
        m_acid.Sparkling = false;
        m_acid.SaturationBase = 0;
        m_acid.SaturationSpeed = 1.4f;
        m_acid.SaturationAmplitude = 3.0f;
    }

    private IEnumerator MoveCameraToPosition(float totalTime) {
        Transform cam_transform = Camera.main.transform;
        Vector3 init_pos = cam_transform.position;
        Quaternion init_rot = cam_transform.rotation;
        Vector3 end_pos = m_cameraPositions[m_cameraIndex].transform.position;
        Quaternion end_rot = m_cameraPositions[m_cameraIndex].transform.rotation;

        float count = 0;
        do {
            yield return new WaitForEndOfFrame();
            count += Time.deltaTime;
            if (count > totalTime) {
                count = totalTime;
            }
            float t = count / totalTime;
            t = t * t * t * (t * (6f * t - 15f) + 10f);
            cam_transform.position = Vector3.Lerp(init_pos, end_pos, t);
            cam_transform.rotation = Quaternion.Lerp(init_rot, end_rot, t);
        } while (count < totalTime);
    }

    public void GameOver() {
        StartCoroutine(GameOverRoutine());
    }

    private IEnumerator GameOverRoutine() {
        m_gameOverText.gameObject.SetActive(true);
        m_crosshair.SetActive(false);
        yield return new WaitForSeconds(3f);
        PlayAgain();
    }

    public void PlayAgain() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void RollCredits() {
        m_scroller.enabled = true;
        m_acid.enabled = false;
        Shooter.Instance.HideAllUI();
    }
}
