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
    public class BuiltinDebugViewsModel : PostProcessingModel
    {
        [Serializable]
        public struct DepthSettings
        {
            [Range(0f, 1f), Tooltip("Scales the camera far plane before displaying the depth map.")]
            public float scale;

            public static DepthSettings defaultSettings
            {
                get
                {
                    return new DepthSettings
                    {
                        scale = 1f
                    };
                }
            }
        }

        [Serializable]
        public struct MotionVectorsSettings
        {
            [Range(0f, 1f), Tooltip("Opacity of the source render.")]
            public float sourceOpacity;

            [Range(0f, 1f), Tooltip("Opacity of the per-pixel motion vector colors.")]
            public float motionImageOpacity;

            [Min(0f), Tooltip("Because motion vectors are mainly very small vectors, you can use this setting to make them more visible.")]
            public float motionImageAmplitude;

            [Range(0f, 1f), Tooltip("Opacity for the motion vector arrows.")]
            public float motionVectorsOpacity;

            [Range(8, 64), Tooltip("The arrow density on screen.")]
            public int motionVectorsResolution;

            [Min(0f), Tooltip("Tweaks the arrows length.")]
            public float motionVectorsAmplitude;

            public static MotionVectorsSettings defaultSettings
            {
                get
                {
                    return new MotionVectorsSettings
                    {
                        sourceOpacity = 1f,

                        motionImageOpacity = 0f,
                        motionImageAmplitude = 16f,

                        motionVectorsOpacity = 1f,
                        motionVectorsResolution = 24,
                        motionVectorsAmplitude = 64f
                    };
                }
            }
        }

        public enum Mode
        {
            None,

            Depth,
            Normals,
            MotionVectors,

            AmbientOcclusion,
            EyeAdaptation,
            FocusPlane,
            PreGradingLog,
            LogLut,
            UserLut
        }

        [Serializable]
        public struct Settings
        {
            public Mode mode;
            public DepthSettings depth;
            public MotionVectorsSettings motionVectors;

            public static Settings defaultSettings
            {
                get
                {
                    return new Settings
                    {
                        mode = Mode.None,
                        depth = DepthSettings.defaultSettings,
                        motionVectors = MotionVectorsSettings.defaultSettings
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

        public bool willInterrupt
        {
            get
            {
                return !IsModeActive(Mode.None)
                       && !IsModeActive(Mode.EyeAdaptation)
                       && !IsModeActive(Mode.PreGradingLog)
                       && !IsModeActive(Mode.LogLut)
                       && !IsModeActive(Mode.UserLut);
            }
        }

        public override void Reset()
        {
            settings = Settings.defaultSettings;
        }

        public bool IsModeActive(Mode mode)
        {
            return m_Settings.mode == mode;
        }
    }
}
