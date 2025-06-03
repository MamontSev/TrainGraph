using TMPro;

using UnityEngine;

namespace Mamont.Data.Graph.Builder
{
	[ExecuteInEditMode]
	public abstract class VertexItemBasic:MonoBehaviour
	{
		[HideInInspector]
		public GraphVertexType VertexType;

		[HideInInspector]
		public int NameIndex;


		public void Init( GraphVertexType vertexType, int nameIndex)
		{
			NameIndex = nameIndex;
			VertexType = vertexType;
			SetGizmoName();
		}

		[SerializeField]
		private GameObject _gizmoObject;


		[SerializeField]
		private TextMeshPro _gizmoName;
		private void SetGizmoName( )
		{
			string s = $"{VertexType.ToString()} {NameIndex}";
			_gizmoName.text = s;
			name = s;
		}



	}
}

