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
using System;

namespace EasyMobile
{
    [AddComponentMenu("")]
    public class Giphy : MonoBehaviour
    {
        public static Giphy Instance
        { 
            get
            {
                if (_instance == null)
                {
                    GameObject ob = new GameObject("Giphy");
                    _instance = ob.AddComponent<Giphy>();
                    DontDestroyOnLoad(ob);
                }

                return _instance;
            }
        }

        private static Giphy _instance;

        public static bool IsUsingAPI { get { return _apiUseCount > 0; } }

        public const string GIPHY_PUBLIC_BETA_KEY = "dc6zaTOxFJmzC";
        public const string GIPHY_UPLOAD_PATH = "https://upload.giphy.com/v1/gifs";
        public const string GIPHY_BASE_URL = "http://giphy.com/gifs/";

        static int _apiUseCount = 0;

        #region Child classes

        [System.Serializable]
        private class UploadSuccessResponse
        {
            public UploadSuccessData data = new UploadSuccessData();

            [System.Serializable]
            public class UploadSuccessData
            {
                public string id = "";
            }
        }

        #endregion

        #region Public API

        /// <summary>
        /// Uploads a GIF image to Giphy using the public beta key. The GIF file can be stored on
        /// the local storage or at a provided URL.
        /// </summary>
        /// <param name="content">Content to upload.</param>
        /// <param name="uploadProgressCallback">Upload progress callback: the parameter indicates upload progress from 0 to 1.</param>
        /// <param name="uploadCompletedCallback">Upload completed callback: the parameter is the URL of the uploaded image.</param>
        /// <param name="uploadFailedCallback">Upload failed callback: the parameter is the error message.</param>
        public static void Upload(GiphyUploadParams content, Action<float> uploadProgressCallback, Action<string> uploadCompletedCallback, Action<string> uploadFailedCallback)
        {
            Upload("", GIPHY_PUBLIC_BETA_KEY, content, uploadProgressCallback, uploadCompletedCallback, uploadFailedCallback);
        }

        /// <summary>
        /// Uploads a GIF image to your Giphy channel. Requires a username and a production API key.
        /// The GIF file can be stored on the local storage or at a provided URL.
        /// </summary>
        /// <param name="username">Your Giphy username.</param>
        /// <param name="apiKey">Production API key.</param>
        /// <param name="content">Content to upload.</param>
        /// <param name="uploadProgressCallback">Upload progress callback: the parameter indicates upload progress from 0 to 1.</param>
        /// <param name="uploadCompletedCallback">Upload completed callback: the parameter is the URL of the uploaded image.</param>
        /// <param name="uploadFailedCallback">Upload failed callback: the parameter is the error message.</param>
        public static void Upload(string username, string apiKey, GiphyUploadParams content, Action<float> uploadProgressCallback, Action<string> uploadCompletedCallback, Action<string> uploadFailedCallback)
        {
            if (string.IsNullOrEmpty(content.localImagePath) && string.IsNullOrEmpty(content.sourceImageUrl))
            {
                Debug.LogError("UploadToGiphy FAILED: no image was specified for uploading.");
                return;
            }
            else if (!string.IsNullOrEmpty(content.localImagePath) && !System.IO.File.Exists(content.localImagePath))
            {
                Debug.LogError("UploadToGiphy FAILED: (local) file not found.");
                return;
            }
        
            WWWForm form = new WWWForm();
            form.AddField("api_key", apiKey);
            form.AddField("username", username);

            if (!string.IsNullOrEmpty(content.localImagePath) && System.IO.File.Exists(content.localImagePath))
                form.AddBinaryData("file", System.IO.File.ReadAllBytes(content.localImagePath));

            if (!string.IsNullOrEmpty(content.sourceImageUrl))
                form.AddField("source_image_url", content.sourceImageUrl);

            if (!string.IsNullOrEmpty(content.tags))
                form.AddField("tags", content.tags);

            if (!string.IsNullOrEmpty(content.sourcePostUrl))
                form.AddField("source_post_url", content.sourcePostUrl);

            if (content.isHidden)
                form.AddField("is_hidden", "true");
            
            Instance.StartCoroutine(CRUpload(form, uploadProgressCallback, uploadCompletedCallback, uploadFailedCallback));
        }

        #endregion

        #region Methods

        static IEnumerator CRUpload(WWWForm form, Action<float> uploadProgressCB, Action<string> uploadCompletedCB, Action<string> uploadFailedCB)
        {
            WWW www = new WWW(GIPHY_UPLOAD_PATH, form);
            _apiUseCount++;

            while (!www.isDone)
            {
                if (uploadProgressCB != null)
                    uploadProgressCB(www.uploadProgress);

                yield return null;
            }

            if (string.IsNullOrEmpty(www.error))
            {
                if (uploadCompletedCB != null)
                {
                    // Extract and return the GIF URL from the return response.
                    UploadSuccessResponse json = JsonUtility.FromJson<UploadSuccessResponse>(www.text);
                    uploadCompletedCB(GIPHY_BASE_URL + json.data.id);
                }
            }
            else
            {
                if (uploadFailedCB != null)
                    uploadFailedCB(www.error);
            }

            _apiUseCount--;
        }

        #endregion

        #region Unity events

        void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        void OnDestroy()
        {
            if (this == _instance)
                _instance = null;
        }

        #endregion
    }
}
