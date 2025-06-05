using System;
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

#endif

namespace Mamont.Core.Inspector
{
	[Serializable]
	public class TrainValue
	{
		private float? _prevValue = null;
		public float Value = 2.0f;

		public event Action<float> OnValueChanged;

		public string Name;
		public TrainValue( float startValue, string name )
		{
			_prevValue = startValue;
			Value = startValue;
			Name = name;
		}

		public void Update()
		{
			CheckChangeValue();
		}

		public void CheckChangeValue()
		{
			if( Value == _prevValue )
				return;
			if( Value < 0.0f )
				Value = 0.0f;
			_prevValue = Value;
			OnValueChanged?.Invoke(Value);
		}
#if UNITY_EDITOR
		[CustomPropertyDrawer(typeof(TrainValue))]
		class TrainValueDrawer:PropertyDrawer
		{
			public override void OnGUI( Rect position , SerializedProperty property , GUIContent label )
			{
				float x = position.x;
				float y = position.y;
				float w = position.width;
				float h = position.height;
				Rect r = new Rect(x , y , w , h);

				EditorGUI.indentLevel = 1;

				float px = position.x;
				float py = position.y;
				float width = 100.0f;
				float height = position.height;

				width = 150.0f;
				Rect valueRect = new Rect(px , py , width , height);
				string name = property.FindPropertyRelative("Name").stringValue;
				EditorGUI.LabelField(valueRect , $"{name}");
				px += width;


				width = 120.0f;
				valueRect = new Rect(px , py , width , height);
				EditorGUI.PropertyField(valueRect , property.FindPropertyRelative("Value") , GUIContent.none);
			}
		}
#endif
	}
}
