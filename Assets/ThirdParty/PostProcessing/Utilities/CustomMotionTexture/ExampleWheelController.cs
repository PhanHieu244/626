/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using UnityEngine;

public class ExampleWheelController : MonoBehaviour
{
    public float acceleration;
    public Renderer motionVectorRenderer; // Reference to the custom motion vector renderer

    Rigidbody m_Rigidbody;

    static class Uniforms
    {
        internal static readonly int _MotionAmount = Shader.PropertyToID("_MotionAmount");
    }

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>(); // Get reference to rigidbody
        m_Rigidbody.maxAngularVelocity = 100; // Set max velocity for rigidbody
    }

    void Update()
    {
        if (Input.GetKey (KeyCode.UpArrow)) // Rotate forward
            m_Rigidbody.AddRelativeTorque(new Vector3(-1 * acceleration, 0, 0), ForceMode.Acceleration); // Add forward torque to mesh
        else if (Input.GetKey (KeyCode.DownArrow)) // Rotate backward
            m_Rigidbody.AddRelativeTorque(new Vector3(1 * acceleration, 0, 0), ForceMode.Acceleration); // Add backward torque to mesh

        float m = -m_Rigidbody.angularVelocity.x / 100; // Calculate multiplier for motion vector texture

        if (motionVectorRenderer) // If the custom motion vector texture renderer exists
            motionVectorRenderer.material.SetFloat(Uniforms._MotionAmount, Mathf.Clamp(m, -0.25f, 0.25f)); // Set the multiplier on the renderer's material
    }
}
