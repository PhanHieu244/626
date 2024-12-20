/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR || (!UNITY_IOS && !UNITY_ANDROID)
using Moments;
using Moments.Encoder;
#endif

namespace EasyMobile
{
    [AddComponentMenu(""), DisallowMultipleComponent]
    public class Gif : MonoBehaviour
    {
        public static Gif Instance
        { 
            get
            {
                if (_instance == null)
                {
                    GameObject ob = new GameObject("Gif");
                    _instance = ob.AddComponent<Gif>();
                    DontDestroyOnLoad(ob);
                }

                return _instance;
            }
        }

        private static Gif _instance;
        private static Dictionary<int, GifExportTask> gifExportTasks = new Dictionary<int, GifExportTask>();
        private static int curExportId = 0;

        #region Public API

        /// <summary>
        /// Starts recording on the specified recorder.
        /// </summary>
        /// <param name="recorder">Recorder.</param>
        public static void StartRecording(Recorder recorder)
        {
            if (recorder == null)
            { 
                Debug.LogError("StartRecording FAILED: recorder is null.");
                return;
            }
            else if (recorder.IsRecording())
            {
                Debug.LogWarning("Attempted to start recording while it is already in progress.");
                return;
            }

            // Start recording!
            recorder.Record();
        }

        /// <summary>
        /// Stops recording on the specified recorder.
        /// </summary>
        /// <returns>The recorded clip.</returns>
        /// <param name="recorder">Recorder.</param>
        public static AnimatedClip StopRecording(Recorder recorder)
        {
            AnimatedClip clip = null;

            if (recorder == null)
                Debug.LogError("StopRecording FAILED: recorder is null.");
            else
                clip = recorder.Stop();

            return clip;
        }

        /// <summary>
        /// Determines whether the recorder is recording.
        /// </summary>
        /// <returns><c>true</c> if is recording; otherwise, <c>false</c>.</returns>
        /// <param name="recorder">Recorder.</param>
        public static bool IsRecording(Recorder recorder)
        {
            return (recorder != null && recorder.IsRecording());
        }

        /// <summary>
        /// Plays the clip on the specified clip player.
        /// </summary>
        /// <param name="player">Player.</param>
        /// <param name="clip">Clip.</param>
        /// <param name="startDelay">Optional delay before the playing starts.</param>
        /// <param name="loop">If set to <c>true</c> loop indefinitely.</param>
        public static void PlayClip(IClipPlayer player, AnimatedClip clip, float startDelay = 0, bool loop = true)
        {
            if (player == null)
            {
                Debug.LogError("Player is null.");
                return;
            }
            else
            {
                player.Play(clip, startDelay, loop);
            }
        }

        /// <summary>
        /// Pauses the clip player.
        /// </summary>
        /// <param name="player">Player.</param>
        public static void PausePlayer(IClipPlayer player)
        {
            if (player == null)
            {
                Debug.LogError("Player is null.");
                return;
            }
            else
            {
                player.Pause();
            }
        }

        /// <summary>
        /// Resumes playing on the clip player.
        /// </summary>
        /// <param name="player">Player.</param>
        public static void ResumePlayer(IClipPlayer player)
        {
            if (player == null)
            {
                Debug.LogError("Player is null.");
                return;
            }
            else
            {
                player.Resume();
            }
        }

        /// <summary>
        /// Stops the clip player.
        /// </summary>
        /// <param name="player">Player.</param>
        public static void StopPlayer(IClipPlayer player)
        {
            if (player == null)
            {
                Debug.LogError("Player is null.");
                return;
            }
            else
            {
                player.Stop();
            }
        }

        /// <summary>
        /// Exports a GIF image from the provided clip.
        /// </summary>
        /// <param name="clip">The clip to export to GIF format.</param>
        /// <param name="filename">Filename to save the output GIF.</param>
        /// <param name="quality">Quality setting for the exported image. Inputs will be clamped between 1 and 100. Bigger values mean better quality but slightly longer processing time. 80 is generally a good value in terms of time-quality balance.</param>
        /// <param name="threadPriority">Thread priority to use when exporting GIF file.</param>
        /// <param name="exportProgressCallback">Export progress callback: 1st parameter is the provided clip, 2nd parameter is the export progress from 0 to 1.</param>
        /// <param name="exportCompletedCallback">Export completed callback: 1st parameter is the provided clip, 2nd parameter is the filepath of the exported GIF.</param>
        public static void ExportGif(AnimatedClip clip, string filename, int quality, System.Threading.ThreadPriority threadPriority, Action<AnimatedClip, float> exportProgressCallback, Action<AnimatedClip, string> exportCompletedCallback)
        {
            ExportGif(clip, filename, 0, quality, threadPriority, exportProgressCallback, exportCompletedCallback);
        }

        /// <summary>
        /// Exports a GIF image from the provided clip.
        /// Allows setting looping mode of the output image.
        /// </summary>
        /// <param name="clip">The clip to export to GIF format.</param>
        /// <param name="filename">Filename to save the output GIF.</param>
        /// <param name="loop">-1 to disable, 0 to loop indefinitely, >0 to loop a set number of times.</param>
        /// <param name="quality">Quality setting for the exported image. Inputs will be clamped between 1 and 100. Bigger values mean better quality but slightly longer processing time. 80 is generally a good value in terms of time-quality balance.</param>
        /// <param name="threadPriority">Priority of the GIF encoding thread.</param>
        /// <param name="exportProgressCallback">Export progress callback: 1st parameter is the provided clip, 2nd parameter is the export progress from 0 to 1.</param>
        /// <param name="exportCompletedCallback">Export completed callback: 1st parameter is the provided clip, 2nd parameter is the filepath of the exported GIF.</param>
        public static void ExportGif(AnimatedClip clip, string filename, int loop, int quality, System.Threading.ThreadPriority threadPriority, Action<AnimatedClip, float> exportProgressCallback, Action<AnimatedClip, string> exportCompletedCallback)
        {
            if (clip == null || clip.Frames.Length == 0 || clip.IsDisposed())
            {
                Debug.LogError("Attempted to export GIF from an empty or disposed clip.");
                return;
            }

            if (String.IsNullOrEmpty(filename))
            {
                Debug.LogError("Exporting GIF failed: filename is null or empty.");
                return;
            }

            // Start the actual export process
            Instance.StartCoroutine(CRExportGif(clip, filename, loop, quality, threadPriority, exportProgressCallback, exportCompletedCallback));
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

        void OnEnable()
        {
            #if UNITY_IOS
            iOSNativeGif.GifExportProgress += OnGifExportProgress;
            iOSNativeGif.GifExportCompleted += OnGifExportCompleted;
            #elif UNITY_ANDROID
            AndroidNativeGif.GifExportProgress += OnGifExportProgress;
            AndroidNativeGif.GifExportCompleted += OnGifExportCompleted;
            #endif
        }

        void OnDisable()
        {
            #if UNITY_IOS
            iOSNativeGif.GifExportProgress -= OnGifExportProgress;
            iOSNativeGif.GifExportCompleted -= OnGifExportCompleted;
            #elif UNITY_ANDROID
            AndroidNativeGif.GifExportProgress -= OnGifExportProgress;
            AndroidNativeGif.GifExportCompleted -= OnGifExportCompleted;
            #endif
        }

        void OnDestroy()
        {
            if (this == _instance)
                _instance = null;
        }

        void Update()
        {
            // Iterate through all the GIF exporting tasks, check their state and
            // invoke their appropriate callbacks, so that they're on main thread
            // instead of a worker thread (so able to use Unity API).
            // Note on thread-safety: the 'isDone' and 'progress' variables
            // are updated from another thread. Since the independent read & write operations
            // of bool and float are atomic, these values are always read in whole here, so no
            // worry about data corruption.
            var ids = new List<int>(gifExportTasks.Keys);
            foreach (var id in ids)
            {
                var task = gifExportTasks[id];

                if (task.isExporting && task.exportProgressCallback != null)
                    task.exportProgressCallback(task.clip, task.progress);

                if (task.isDone)
                {
                    if (task.exportCompletedCallback != null)
                        task.exportCompletedCallback(task.clip, task.filepath);

                    // Release the clip and temporary buffer
                    task.clip = null;
                    task.imageData = null;

                    // Task is done, remove it from the list
                    gifExportTasks[id] = null;
                    gifExportTasks.Remove(id);
                }
            }
        }

        #endregion

        #region GIF export event handlers

        // The export is a 2-step process: pre-processing frames (main thread) and the actual GIF constructing (worker thread).
        // The progress value is divided as 50% on the 1st step and 50% on the 2nd step.
        static void OnGifPreProcessing(int taskId, float progress)
        {
            if (gifExportTasks.ContainsKey(taskId))
            {
                gifExportTasks[taskId].progress = progress * 0.5f; // consider the pre-processing as taking up first 50% of the whole procedure  
            }   
        }

        static void OnGifExportProgress(int taskId, float progress)
        {
            if (gifExportTasks.ContainsKey(taskId))
            {
                gifExportTasks[taskId].progress = 0.5f + progress * 0.5f;   // consider the actual GIF constructing as taking up last 50% of the whole procedure
            }
        }

        static void OnGifExportCompleted(int taskId, string filepath)
        {
            if (gifExportTasks.ContainsKey(taskId))
            {
                gifExportTasks[taskId].isDone = true;
            }
        }

        #endregion

        #region Methods

        // GIF exporting coroutine: preprocess the image data then send it to native code (mobile) or a worker thread (other platforms) to export GIF file.
        static IEnumerator CRExportGif(AnimatedClip clip, string filename, int loop, int quality, System.Threading.ThreadPriority threadPriority, Action<AnimatedClip, float> exportProgressCallback, Action<AnimatedClip, string> exportCompletedCallback)
        {
            // The encoder don't want loop to be < -1
            if (loop < -1)
                loop = -1;

            // Compute the NeuQuant sample factor from the inverse of the quality value.
            // Note that NeuQuant prefers values in range [1,30] so we'll also scale the factor to that range.
            int sampleFac = Mathf.RoundToInt(Mathf.Lerp(30, 1, (float)(Mathf.Clamp(quality, 1, 100)) / 100));

            // Construct filepath
            string folder;

            #if UNITY_EDITOR
            folder = Application.dataPath; // Assets folder
            #else
            folder = Application.persistentDataPath;
            #endif

            string filepath = System.IO.Path.Combine(folder, filename + ".gif");

            // Construct a new export task
            var exportTask = new GifExportTask();
            exportTask.taskId = curExportId++;  // assign this task a unique id
            exportTask.clip = clip;
            exportTask.imageData = null;
            exportTask.filepath = filepath;  
            exportTask.loop = loop;
            exportTask.sampleFac = sampleFac;
            exportTask.exportProgressCallback = exportProgressCallback;
            exportTask.exportCompletedCallback = exportCompletedCallback;
            exportTask.workerPriority = threadPriority;
            exportTask.isExporting = true;
            exportTask.isDone = false;
            exportTask.progress = 0;

            // Add task to the list with its unique id key
            gifExportTasks.Add(exportTask.taskId, exportTask);

            yield return null;

            // Create a temporary texture to read RenderTexture data
            Texture2D temp = new Texture2D(clip.Width, clip.Height, TextureFormat.RGB24, false);
            temp.hideFlags = HideFlags.HideAndDontSave;
            temp.wrapMode = TextureWrapMode.Clamp;
            temp.filterMode = FilterMode.Bilinear;
            temp.anisoLevel = 0;

            // On iOS and Android, the GIF encoding is done in native code.
            // In Unity editor (and other platforms), we use Moments encoder for testing purpose.
            #if UNITY_EDITOR || (!UNITY_IOS && !UNITY_ANDROID)
            // Converts to GIF frames
            List<GifFrame> frames = new List<GifFrame>(clip.Frames.Length);
            for (int i = 0; i < clip.Frames.Length; i++)
            {
                RenderTexture source = clip.Frames[i];
                RenderTexture.active = source;
                temp.ReadPixels(new Rect(0, 0, source.width, source.height), 0, 0);
                temp.Apply();
                RenderTexture.active = null;

                GifFrame frame = new GifFrame() { Width = temp.width, Height = temp.height, Data = temp.GetPixels32() };
                frames.Add(frame);

                OnGifPreProcessing(exportTask.taskId, (float)i / clip.Frames.Length);
                yield return null;
            }

            // Setup a worker thread and let it do its magic
            GifEncoder encoder = new GifEncoder(loop, sampleFac);
            encoder.SetDelay(Mathf.RoundToInt(1000f / clip.FramePerSecond));
            Worker worker = new Worker(
                                exportTask.taskId, 
                                threadPriority, 
                                frames, 
                                encoder, 
                                filepath, 
                                OnGifExportProgress, 
                                OnGifExportCompleted);
            
            worker.Start();
            #else

            // Allocate an array to hold the serialized image data
            exportTask.imageData = new Color32[clip.Frames.Length][];

            // Construct the serialized image data, note that texture data is layered down-top, so flip it
            for (int i = 0; i < clip.Frames.Length; i++)
            {
                var source = clip.Frames[i];
                RenderTexture.active = source;
                temp.ReadPixels(new Rect(0, 0, source.width, source.height), 0, 0);
                temp.Apply();
                RenderTexture.active = null;

                // Get the frame's pixel data
                exportTask.imageData[i] = temp.GetPixels32();

                // Call the preprocessing handler directly
                float progress = (float)i / clip.Frames.Length;
                OnGifPreProcessing(exportTask.taskId, progress);

                yield return null;
            }
              
            #if UNITY_IOS
            iOSNativeGif.ExportGif(exportTask);
            #elif UNITY_ANDROID
            AndroidNativeGif.ExportGif(exportTask);
            #endif

            #endif  // UNITY_EDITOR || (!UNITY_IOS && !UNITY_ANDROID)

            // Dispose the temporary texture
            Destroy(temp);
        }

        #endregion
    }
}
