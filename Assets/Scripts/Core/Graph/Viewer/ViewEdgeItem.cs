using System;

using TMPro;

using UnityEngine;

namespace Mamont.Core.Graph.Viewer
{
	public class ViewEdgeItem:MonoBehaviour
	{
		private int? _prevValue = null;
		public int Value = 10;

		protected int _nameIndex;
		public int NameIndex => _nameIndex;

		public event Action<int , int> OnValueChanged;
		private Transform _pos1;
		private Transform _pos2;
		public void Init( int startValue , int nameIndex , Transform pos1, Transform pos2 )
		{
			_prevValue = startValue;
			Value = startValue;
			_nameIndex = nameIndex;
			SetGizmoValueText();

			this.name = $"Edge {_nameIndex}";

			_pos1 = pos1;
			_pos2 = pos2;

			SelLineRenderer();

			SetPosition();
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
			if( Value < 0 )
				Value = 0;
			_prevValue = Value;
			OnValueChanged?.Invoke(Value , _nameIndex);
			SetGizmoValueText();
		}
		private void SetGizmoValueText()
		{
			string s = Value.ToString();
			if( s.Length > 5 )
				s = s.Substring(0 , 5);
			_gizmoValue.text = s;
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
