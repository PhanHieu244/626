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
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace EasyMobile.Demo
{
    public class DemoHomeController : MonoBehaviour
    {
        public Text installationTime;

        void OnEnable()
        {
            NotificationManager.NotificationOpened += NotificationManager_NotificationOpened;
        }

        void OnDisable()
        {
            NotificationManager.NotificationOpened -= NotificationManager_NotificationOpened;
        }

        public void AdvertisingDemo()
        {
            SceneManager.LoadScene("AdvertisingDemo");
        }

        public void GameServiceDemo()
        {
            SceneManager.LoadScene("GameServiceDemo");
        }

        public void GifDemo()
        {
            SceneManager.LoadScene("GifDemo");
        }

        public void InAppPurchaseDemo()
        {
            SceneManager.LoadScene("InAppPurchasingDemo");
        }

        public void MobileNativeShareDemo()
        {
            SceneManager.LoadScene("MobileNativeShareDemo");
        }

        public void MobileNativeUIDemo()
        {
            SceneManager.LoadScene("MobileNativeUIDemo");
        }

        public void UtilityDemo()
        {
            SceneManager.LoadScene("UtilitiesDemo");
        }

        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        void Start()
        {
            var installTime = Helper.GetAppInstallationTime();
            installationTime.text = "Install Date: " + installTime.ToShortDateString() + " " + installTime.ToShortTimeString();
        }

        void Update()
        {
            #if UNITY_ANDROID
            if (Input.GetKeyUp(KeyCode.Escape))
            {   
                // Ask if user wants to exit
                MobileNativeAlert alert = MobileNativeUI.ShowTwoButtonAlert("Exit App",
                                              "Do you want to exit?",
                                              "Yes", 
                                              "No");

                if (alert != null)
                    alert.OnComplete += delegate (int button)
                    { 
                        if (button == 0)
                            Application.Quit();
                    };
            }

            #endif
        }

        // Push notification opened handler
        void NotificationManager_NotificationOpened(string message, string actionID, Dictionary<string, object> additionalData, bool isAppInFocus)
        {
            Debug.Log("Push notification received!");
            Debug.Log("Message: " + message);
            Debug.Log("isAppInFocus: " + isAppInFocus.ToString());

            if (additionalData != null)
            {
                Debug.Log("AdditionalData:");
                foreach (KeyValuePair<string, object> item in additionalData)
                {
                    Debug.Log("Key: " + item.Key + " - Value: " + item.Value.ToString());
                }

                if (additionalData.ContainsKey("newUpdate"))
                {
                    if (!isAppInFocus)
                    {
                        Debug.Log("New update available! Should open the update page now.");
                    }
                }
            }
        }
    }
}

