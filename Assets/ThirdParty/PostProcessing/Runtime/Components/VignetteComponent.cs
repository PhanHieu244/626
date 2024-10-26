/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

namespace UnityEngine.PostProcessing
{
    public sealed class VignetteComponent : PostProcessingComponentRenderTexture<VignetteModel>
    {
        static class Uniforms
        {
            internal static readonly int _Vignette_Color    = Shader.PropertyToID("_Vignette_Color");
            internal static readonly int _Vignette_Center   = Shader.PropertyToID("_Vignette_Center");
            internal static readonly int _Vignette_Settings = Shader.PropertyToID("_Vignette_Settings");
            internal static readonly int _Vignette_Mask     = Shader.PropertyToID("_Vignette_Mask");
            internal static readonly int _Vignette_Opacity  = Shader.PropertyToID("_Vignette_Opacity");
        }

        public override bool active
        {
            get
            {
                return model.enabled
                       && !context.interrupted;
            }
        }

        public override void Prepare(Material uberMaterial)
        {
            var settings = model.settings;
            uberMaterial.SetColor(Uniforms._Vignette_Color, settings.color);

            if (settings.mode == VignetteModel.Mode.Classic)
            {
                uberMaterial.SetVector(Uniforms._Vignette_Center, settings.center);
                uberMaterial.EnableKeyword("VIGNETTE_CLASSIC");
                float roundness = (1f - settings.roundness) * 6f + settings.roundness;
                uberMaterial.SetVector(Uniforms._Vignette_Settings, new Vector4(settings.intensity * 3f, settings.smoothness * 5f, roundness, settings.rounded ? 1f : 0f));
            }
            else if (settings.mode == VignetteModel.Mode.Masked)
            {
                if (settings.mask != null && settings.opacity > 0f)
                {
                    uberMaterial.EnableKeyword("VIGNETTE_MASKED");
                    uberMaterial.SetTexture(Uniforms._Vignette_Mask, settings.mask);
                    uberMaterial.SetFloat(Uniforms._Vignette_Opacity, settings.opacity);
                }
            }
        }
    }
}
