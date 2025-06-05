using System;

using TMPro;

using UnityEngine;

namespace Mamont.Core.Graph.Viewer
{
	public class ViewEdgeItem:MonoBehaviour
	{
		protected int _nameIndex;
		public int NameIndex => _nameIndex;

		private Transform _pos1;
		private Transform _pos2;
		public void Init( int startValue , int nameIndex , Transform pos1, Transform pos2 )
		{
			_nameIndex = nameIndex;
			SetGizmoValueText(startValue);

			this.name = $"Edge {_nameIndex}";

			_pos1 = pos1;
			_pos2 = pos2;

			SelLineRenderer();

			SetPosition();
		}



		[SerializeField]
		private TextMeshPro _gizmoValue;

		public void SetGizmoValueText(int value)
		{
			_gizmoValue.text = $"{_nameIndex} ({value})";
		}

		[SerializeField] private LineRenderer _lineRenderer;		   

		private void SelLineRenderer()
		{
			_lineRenderer.positionCount = 2;
			_lineRenderer.SetPosition(0 , _pos1.position);
			_lineRenderer.SetPosition(1 , _pos2.position);
		}

		private void SetPosition()
		{
			transform.position = Vector3.Lerp(_pos1.position , _pos2.position , 0.5f);
		}
	}
}
