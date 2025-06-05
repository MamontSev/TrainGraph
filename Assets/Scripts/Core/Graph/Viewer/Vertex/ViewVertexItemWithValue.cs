using TMPro;

using UnityEngine;

namespace Mamont.Core.Graph.Viewer
{
	public abstract class ViewVertexItemWithValue:ViewVertexItemBasic
	{
		public void Init( float startValue, int nameIndex , Vector3 position )
		{
			base.Init(nameIndex , position);
			SetGizmoValueText(startValue);
		}


		[SerializeField]
		private TextMeshPro _gizmoValue;


		public void SetGizmoValueText(float value)
		{
			string s = value.ToString();
			if( s.Length > 5 )
				s = s.Substring(0 , 5);
			_gizmoValue.text = s;
		}


	}
}
