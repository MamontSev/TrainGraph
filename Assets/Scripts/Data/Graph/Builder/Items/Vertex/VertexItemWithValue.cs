using TMPro;

using UnityEngine;

namespace Mamont.Data.Graph.Builder
{
	public abstract class VertexItemWithValue:VertexItemBasic
	{

		private float? _prevValue = null;

		private void Update()
		{
			SetGizmoValue();
		}


		[SerializeField]
		private TextMeshPro _gizmoValue;
		public void SetGizmoValue()
		{
			if( Value == _prevValue )
				return;
			if( Value < 0.0f )
				Value = 0.0f;
			_prevValue = Value;
			string s = Value.ToString();
			if( s.Length > 5 )
				s = s.Substring(0 , 5);
			_gizmoValue.text = s;
		}

		public float Value = 2.0f;
	}
}
