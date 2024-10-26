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
using System.Collections.Generic;
using System.Threading;
using Moments.Encoder;
using ThreadPriority = System.Threading.ThreadPriority;

namespace Moments
{
    internal sealed class Worker
    {
        Thread m_Thread;
        int m_Id;

        internal List<GifFrame> m_Frames;
        internal GifEncoder m_Encoder;
        internal string m_FilePath;
        internal Action<int, string> m_OnFileSaved;
        internal Action<int, float> m_OnFileSaveProgress;

        internal Worker(int taskId, ThreadPriority priority, List<GifFrame> frames, GifEncoder encoder, string filepath, Action<int, float> onFileSaveProgress, Action<int, string> onFileSaved)
        {
            m_Id = taskId;
            m_Thread = new Thread(Run);
            m_Thread.Priority = priority;
            m_Frames = frames;
            m_Encoder = encoder;
            m_FilePath = filepath;
            m_OnFileSaved = onFileSaved;
            m_OnFileSaveProgress = onFileSaveProgress;
        }

        internal void Start()
        {
            m_Thread.Start();
        }

        void Run()
        {
            m_Encoder.Start(m_FilePath);

            for (int i = 0; i < m_Frames.Count; i++)
            {
                GifFrame frame = m_Frames[i];
                m_Encoder.AddFrame(frame);

                if (m_OnFileSaveProgress != null)
                {
                    float percent = (float)i / (float)m_Frames.Count;
                    m_OnFileSaveProgress(m_Id, percent);
                }
            }

            m_Encoder.Finish();

            if (m_OnFileSaved != null)
                m_OnFileSaved(m_Id, m_FilePath);
        }
    }
}
