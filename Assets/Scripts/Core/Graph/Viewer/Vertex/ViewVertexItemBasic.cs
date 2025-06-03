using TMPro;

using UnityEngine;

namespace Mamont.Core.Graph.Viewer
{
	public abstract class ViewVertexItemBasic:MonoBehaviour
	{
		protected int _nameIndex;
		public int NameIndex => _nameIndex;

		[SerializeField]
		private TextMeshPro _gizmoName;

		public void Init( int nameIndex , Vector3 position)
		{
			this._nameIndex = nameIndex;
			transform.position = position;
			_gizmoName.text = this._nameIndex.ToString();
			name = this._nameIndex.ToString();

		}
	}
}
