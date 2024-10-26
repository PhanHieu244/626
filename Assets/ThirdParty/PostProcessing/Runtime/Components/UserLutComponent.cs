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
    public sealed class UserLutComponent : PostProcessingComponentRenderTexture<UserLutModel>
    {
        static class Uniforms
        {
            internal static readonly int _UserLut        = Shader.PropertyToID("_UserLut");
            internal static readonly int _UserLut_Params = Shader.PropertyToID("_UserLut_Params");
        }

        public override bool active
        {
            get
            {
                var settings = model.settings;
                return model.enabled
                       && settings.lut != null
                       && settings.contribution > 0f
                       && settings.lut.height == (int)Mathf.Sqrt(settings.lut.width)
                       && !context.interrupted;
            }
        }

        public override void Prepare(Material uberMaterial)
        {
            var settings = model.settings;
            uberMaterial.EnableKeyword("USER_LUT");
            uberMaterial.SetTexture(Uniforms._UserLut, settings.lut);
            uberMaterial.SetVector(Uniforms._UserLut_Params, new Vector4(1f / settings.lut.width, 1f / settings.lut.height, settings.lut.height - 1f, settings.contribution));
        }

        public void OnGUI()
        {
            var settings = model.settings;
            var rect = new Rect(context.viewport.x * Screen.width + 8f, 8f, settings.lut.width, settings.lut.height);
            GUI.DrawTexture(rect, settings.lut);
        }
    }
}
