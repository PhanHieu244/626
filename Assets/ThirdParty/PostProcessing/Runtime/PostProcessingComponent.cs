/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using UnityEngine.Rendering;

namespace UnityEngine.PostProcessing
{
    public abstract class PostProcessingComponentBase
    {
        public PostProcessingContext context;

        public virtual DepthTextureMode GetCameraFlags()
        {
            return DepthTextureMode.None;
        }

        public abstract bool active { get; }

        public virtual void OnEnable()
        {}

        public virtual void OnDisable()
        {}

        public abstract PostProcessingModel GetModel();
    }

    public abstract class PostProcessingComponent<T> : PostProcessingComponentBase
        where T : PostProcessingModel
    {
        public T model { get; internal set; }

        public virtual void Init(PostProcessingContext pcontext, T pmodel)
        {
            context = pcontext;
            model = pmodel;
        }

        public override PostProcessingModel GetModel()
        {
            return model;
        }
    }

    public abstract class PostProcessingComponentCommandBuffer<T> : PostProcessingComponent<T>
        where T : PostProcessingModel
    {
        public abstract CameraEvent GetCameraEvent();

        public abstract string GetName();

        public abstract void PopulateCommandBuffer(CommandBuffer cb);
    }

    public abstract class PostProcessingComponentRenderTexture<T> : PostProcessingComponent<T>
        where T : PostProcessingModel
    {
        public virtual void Prepare(Material material)
        {}
    }
}
