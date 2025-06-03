using System;

using TMPro;

using UnityEngine;

namespace Mamont.Data.Graph.Builder
{
	[ExecuteInEditMode]
	public class EdgeItem:MonoBehaviour
	{
		public VertexItemBasic Vertex1;

		public VertexItemBasic Vertex2;

		[HideInInspector]
		public int NameIndex;

		public void Init( int nameIndex )
		{
			NameIndex = nameIndex;
			name = $"Edge_{NameIndex}";
		}



		private bool IsCompleteVertex => Vertex1 != null && Vertex2 != null;
		

		private void Update()
		{
			SetGizmoWeight();

			if( IsCompleteVertex )
				SetGizmoPosition();
		}

		private float? _prevWeifgt = null;
		private void SetGizmoWeight()
		{
			if( Weight == _prevWeifgt )
				return;
			if( Weight < 0 )
				Weight = 0;
			_prevWeifgt = Weight;
			string s = Weight.ToString();
			if( s.Length > 5 )
				s = s.Substring(0 , 5);
			_gizmoValue.text = s;
		}



		private void OnDrawGizmos()
		{
			if( !IsCompleteVertex )
				return;
			Gizmos.color = Color.green;
			Gizmos.DrawLine(Vertex1.transform.position , Vertex2.transform.position);
		}
		[SerializeField]
		private TextMeshPro _gizmoValue;
		public void SetGizmoPosition()
		{
			if( !IsCompleteVertex )
				return;
			Debug.DrawLine(Vertex1.transform.position , Vertex2.transform.position, Color.green,Time.deltaTime);
			Vector3 middlePos = Vector3.Lerp(Vertex1.transform.position , Vertex2.transform.position , 0.5f);
			transform.position = middlePos;
		}

		public int Weight = 10;
	}
}
