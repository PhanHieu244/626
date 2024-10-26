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
    public sealed class DitheringComponent : PostProcessingComponentRenderTexture<DitheringModel>
    {
        static class Uniforms
        {
            internal static readonly int _DitheringTex = Shader.PropertyToID("_DitheringTex");
            internal static readonly int _DitheringCoords = Shader.PropertyToID("_DitheringCoords");
        }

        public override bool active
        {
            get
            {
                return model.enabled
                       && !context.interrupted;
            }
        }

        // Holds 64 64x64 Alpha8 textures (256kb total)
        Texture2D[] noiseTextures;
        int textureIndex = 0;

        const int k_TextureCount = 64;

        public override void OnDisable()
        {
            noiseTextures = null;
        }

        void LoadNoiseTextures()
        {
            noiseTextures = new Texture2D[k_TextureCount];

            for (int i = 0; i < k_TextureCount; i++)
                noiseTextures[i] = Resources.Load<Texture2D>("Bluenoise64/LDR_LLL1_" + i);
        }

        public override void Prepare(Material uberMaterial)
        {
            float rndOffsetX;
            float rndOffsetY;

#if POSTFX_DEBUG_STATIC_DITHERING
            textureIndex = 0;
            rndOffsetX = 0f;
            rndOffsetY = 0f;
#else
            if (++textureIndex >= k_TextureCount)
                textureIndex = 0;

            rndOffsetX = Random.value;
            rndOffsetY = Random.value;
#endif

            if (noiseTextures == null)
                LoadNoiseTextures();

            var noiseTex = noiseTextures[textureIndex];

            uberMaterial.EnableKeyword("DITHERING");
            uberMaterial.SetTexture(Uniforms._DitheringTex, noiseTex);
            uberMaterial.SetVector(Uniforms._DitheringCoords, new Vector4(
                (float)context.width / (float)noiseTex.width,
                (float)context.height / (float)noiseTex.height,
                rndOffsetX,
                rndOffsetY
            ));
        }
    }
}
