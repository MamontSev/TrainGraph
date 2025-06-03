using System;

using TMPro;

using UnityEngine;

namespace Mamont.Core.Graph.Viewer
{
	public abstract class ViewVertexItemWithValue:ViewVertexItemBasic
	{
		private float? _prevValue = null;
		public float Value = 2.0f;

		public event Action<float , int> OnValueChanged;
		public void Init( float startValue, int nameIndex , Vector3 position )
		{
			base.Init(nameIndex , position);
			_prevValue = startValue;
			Value = startValue;
			SetGizmoValueText();
		}

		private void Update()
		{
			CheckChangeValue();
		}


		[SerializeField]
		private TextMeshPro _gizmoValue;
		public void CheckChangeValue()
		{
			if( Value == _prevValue )
				return;
			if( Value < 0.0f )
				Value = 0.0f;
			_prevValue = Value;
			OnValueChanged?.Invoke(Value,_nameIndex);
			SetGizmoValueText();
		}
		private void SetGizmoValueText()
		{
			string s = Value.ToString();
			if( s.Length > 5 )
				s = s.Substring(0 , 5);
			_gizmoValue.text = s;
		}

		
	}
}
