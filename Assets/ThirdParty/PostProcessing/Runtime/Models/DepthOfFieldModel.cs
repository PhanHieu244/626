/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using System;

namespace UnityEngine.PostProcessing
{
    [Serializable]
    public class DepthOfFieldModel : PostProcessingModel
    {
        public enum KernelSize
        {
            Small,
            Medium,
            Large,
            VeryLarge
        }

        [Serializable]
        public struct Settings
        {
            [Min(0.1f), Tooltip("Distance to the point of focus.")]
            public float focusDistance;

            [Range(0.05f, 32f), Tooltip("Ratio of aperture (known as f-stop or f-number). The smaller the value is, the shallower the depth of field is.")]
            public float aperture;

            [Range(1f, 300f), Tooltip("Distance between the lens and the film. The larger the value is, the shallower the depth of field is.")]
            public float focalLength;

            [Tooltip("Calculate the focal length automatically from the field-of-view value set on the camera. Using this setting isn't recommended.")]
            public bool useCameraFov;

            [Tooltip("Convolution kernel size of the bokeh filter, which determines the maximum radius of bokeh. It also affects the performance (the larger the kernel is, the longer the GPU time is required).")]
            public KernelSize kernelSize;

            public static Settings defaultSettings
            {
                get
                {
                    return new Settings
                    {
                        focusDistance = 10f,
                        aperture = 5.6f,
                        focalLength = 50f,
                        useCameraFov = false,
                        kernelSize = KernelSize.Medium
                    };
                }
            }
        }

        [SerializeField]
        Settings m_Settings = Settings.defaultSettings;
        public Settings settings
        {
            get { return m_Settings; }
            set { m_Settings = value; }
        }

        public override void Reset()
        {
            m_Settings = Settings.defaultSettings;
        }
    }
}
