using System;

using Mamont.Data.Graph.Builder;

#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Mamont.Core.Inspector
{
	[Serializable]
	public class VertexValue
	{
		private float? _prevValue = null;
		public float Value = 2.0f;

		public event Action<float , int> OnValueChanged;

		private int _nameIndex;
		public string Name;
		public VertexValue( float startValue , int nameIndex, GraphVertexType vertexType  )
		{
			_nameIndex = nameIndex;
			_prevValue = startValue;
			Value = startValue;

			Name = $"{nameIndex} - {vertexType}";
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
			OnValueChanged?.Invoke(Value , _nameIndex);
		}
#if UNITY_EDITOR
		[CustomPropertyDrawer(typeof(VertexValue))]
		class VertexValueDrawer:PropertyDrawer
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
