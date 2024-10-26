/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

namespace AcidTrip
{
	[CustomEditor(typeof(AcidTrip))]
	class AcidTripEditor : Editor {
		
		private SerializedObject s_target;
		private int theme;
		
		private string[] presets = new string[]
		{
			"Custom", "Low Level", "Medium Level", "High Level", "Colorful", "Lost"
		};
	
		public void OnEnable()
		{
			s_target = new SerializedObject (target);
		}
		
		public override void OnInspectorGUI()
		{
			s_target.Update ();
			
			EditorGUILayout.LabelField ("Reproduces view during an acid trip.", EditorStyles.miniLabel);

			theme = EditorGUILayout.Popup("Themes", theme, presets); 

			EditorGUILayout.PropertyField (s_target.FindProperty(string.Format("Wavelength")), new GUIContent("Waves Frequency"));
			EditorGUILayout.PropertyField (s_target.FindProperty(string.Format("DistortionStrength")), new GUIContent("Distortion Strength"));
			EditorGUILayout.PropertyField (s_target.FindProperty(string.Format("Sparkling")), new GUIContent("Sparkling"));

			EditorGUILayout.PropertyField (s_target.FindProperty(string.Format("SaturationBase")), new GUIContent("Saturation Base"));
			EditorGUILayout.PropertyField (s_target.FindProperty(string.Format("SaturationSpeed")), new GUIContent("Saturation Speed"));
			EditorGUILayout.PropertyField (s_target.FindProperty(string.Format("SaturationAmplitude")), new GUIContent("Saturation Amplitude"));

			switch (theme) {
			case 1:
				s_target.FindProperty(string.Format("Wavelength")).floatValue = 3.0f;
				s_target.FindProperty(string.Format("DistortionStrength")).floatValue = 0.1f;
				s_target.FindProperty(string.Format("Sparkling")).boolValue = false;
				s_target.FindProperty(string.Format("SaturationBase")).floatValue = 1.0f;
				s_target.FindProperty(string.Format("SaturationSpeed")).floatValue = 1.0f;
				s_target.FindProperty(string.Format("SaturationAmplitude")).floatValue = 0.1f;
				break;
			case 2:
				s_target.FindProperty(string.Format("Wavelength")).floatValue = 1.0f;
				s_target.FindProperty(string.Format("DistortionStrength")).floatValue = 0.25f;
				s_target.FindProperty(string.Format("Sparkling")).boolValue = false;
				s_target.FindProperty(string.Format("SaturationBase")).floatValue = 1.0f;
				s_target.FindProperty(string.Format("SaturationSpeed")).floatValue = 1.0f;
				s_target.FindProperty(string.Format("SaturationAmplitude")).floatValue = 0.3f;
				break;
			case 3:
				s_target.FindProperty(string.Format("Wavelength")).floatValue = 1.2f;
				s_target.FindProperty(string.Format("DistortionStrength")).floatValue = 0.35f;
				s_target.FindProperty(string.Format("Sparkling")).boolValue = true;
				s_target.FindProperty(string.Format("SaturationBase")).floatValue = 1.4f;
				s_target.FindProperty(string.Format("SaturationSpeed")).floatValue = 1.6f;
				s_target.FindProperty(string.Format("SaturationAmplitude")).floatValue = 0.5f;
				break;
			case 4:
				s_target.FindProperty(string.Format("Wavelength")).floatValue = 2.0f;
				s_target.FindProperty(string.Format("DistortionStrength")).floatValue = 0.3f;
				s_target.FindProperty(string.Format("Sparkling")).boolValue = false;
				s_target.FindProperty(string.Format("SaturationBase")).floatValue = 4.0f;
				s_target.FindProperty(string.Format("SaturationSpeed")).floatValue = 3.0f;
				s_target.FindProperty(string.Format("SaturationAmplitude")).floatValue = 1.0f;
				break;
			case 5:
				s_target.FindProperty(string.Format("Wavelength")).floatValue = 1.0f;
				s_target.FindProperty(string.Format("DistortionStrength")).floatValue = 0.6f;
				s_target.FindProperty(string.Format("Sparkling")).boolValue = false;
				s_target.FindProperty(string.Format("SaturationBase")).floatValue = 0.0f;
				s_target.FindProperty(string.Format("SaturationSpeed")).floatValue = 1.4f;
				s_target.FindProperty(string.Format("SaturationAmplitude")).floatValue = 3.0f;
				break;
			}
			
			s_target.ApplyModifiedProperties();
		}
	}
}
