/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using System;
using System.Collections.Generic;

namespace UnityEngine.PostProcessing
{
    public sealed class RenderTextureFactory : IDisposable
    {
        HashSet<RenderTexture> m_TemporaryRTs;

        public RenderTextureFactory()
        {
            m_TemporaryRTs = new HashSet<RenderTexture>();
        }

        public RenderTexture Get(RenderTexture baseRenderTexture)
        {
            return Get(
                baseRenderTexture.width,
                baseRenderTexture.height,
                baseRenderTexture.depth,
                baseRenderTexture.format,
                baseRenderTexture.sRGB ? RenderTextureReadWrite.sRGB : RenderTextureReadWrite.Linear,
                baseRenderTexture.filterMode,
                baseRenderTexture.wrapMode
                );
        }

        public RenderTexture Get(int width, int height, int depthBuffer = 0, RenderTextureFormat format = RenderTextureFormat.ARGBHalf, RenderTextureReadWrite rw = RenderTextureReadWrite.Default, FilterMode filterMode = FilterMode.Bilinear, TextureWrapMode wrapMode = TextureWrapMode.Clamp, string name = "FactoryTempTexture")
        {
            if (width <= 0) width = 1;
            if (height <= 0) height = 1;
            var rt = RenderTexture.GetTemporary(width, height, depthBuffer, format, rw); // add forgotten param rw
            rt.filterMode = filterMode;
            rt.wrapMode = wrapMode;
            rt.name = name;
            m_TemporaryRTs.Add(rt);
            return rt;
        }

        public void Release(RenderTexture rt)
        {
            if (rt == null)
                return;

            if (!m_TemporaryRTs.Contains(rt))
                throw new ArgumentException(string.Format("Attempting to remove a RenderTexture that was not allocated: {0}", rt));

            m_TemporaryRTs.Remove(rt);
            RenderTexture.ReleaseTemporary(rt);
        }

        public void ReleaseAll()
        {
            var enumerator = m_TemporaryRTs.GetEnumerator();
            while (enumerator.MoveNext())
                RenderTexture.ReleaseTemporary(enumerator.Current);

            m_TemporaryRTs.Clear();
        }

        public void Dispose()
        {
            ReleaseAll();
        }
    }
}
