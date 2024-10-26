/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using System;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

namespace AcidTrip
{
	[ExecuteInEditMode]
	[RequireComponent (typeof(Camera))]
	[AddComponentMenu ("Image Effects/Acid Trip")]
	public class AcidTrip : PostEffectsBase
	{

		private float timer = 0;

		public float Wavelength = 1.0f, DistortionStrength = 0.25f;
		public bool Sparkling = false;

		public float SaturationBase = 1.0f, SaturationSpeed = 1.0f, SaturationAmplitude = 0.3f;
		
		public Shader currentShader = null;
		private Material currentMaterial = null;
		
		public override bool CheckResources ()
		{
			currentShader = Shader.Find ("AcidTrip/AcidTrip");
			CheckSupport (false);
			currentMaterial = CheckShaderAndCreateMaterial(currentShader, currentMaterial);
			
			if (!isSupported)
				ReportAutoDisable ();
			return isSupported;
		}
		
		void OnRenderImage (RenderTexture source, RenderTexture destination)
		{
			timer += Time.deltaTime;

			currentMaterial.SetFloat ("timer", timer);
			currentMaterial.SetFloat ("speed", 1);
			currentMaterial.SetFloat ("distortion", 0.25f);
			currentMaterial.SetFloat ("amplitude", 70.0f);
			currentMaterial.SetFloat ("satbase", SaturationBase);
			currentMaterial.SetFloat ("satSpeed", SaturationSpeed);
			currentMaterial.SetFloat ("satAmp", SaturationAmplitude);
			currentMaterial.SetFloat ("strength", Wavelength);
			currentMaterial.SetFloat ("distortion", DistortionStrength);
			currentMaterial.SetInt ("sparkling", (Sparkling) ? 1 : 0);

			if (!CheckResources())
			{
				Graphics.Blit (source, destination);
				return;
			}
			Graphics.Blit (source, destination, currentMaterial);
		}
	}
	
}
