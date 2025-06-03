using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Mamont.Data.Graph.Config;

using Newtonsoft.Json;

using UnityEditor;
using UnityEditor.SceneManagement;


using UnityEngine;

namespace Mamont.Data.Graph.Builder
{
	public class GraphDataBuilder:MonoBehaviour
	{
#if UNITY_EDITOR
		[SerializeField]
		private List<VertexItemBasic> _vertexList = new();

		[SerializeField]
		private List<EdgeItem> _edgeList = new();

		[Header("Vertex")]
		[SerializeField]
		private Transform _vertexParent;
		[SerializeField]
		private VertexItemEmpty _vertexItemEmptyPrefab;
		[SerializeField]
		private VertexItemBase _vertexItemBasePrefab;
		[SerializeField]
		private VertexItemMine _vertexItemMinePrefab;

		[Header("Edge")]
		[SerializeField]
		private Transform _edgeParent;
		[SerializeField]
		private EdgeItem _edgeItem;


		public void AddVertex( GraphVertexType vertexType )
		{
			_vertexList.RemoveAll(x => x == null);
			VertexItemBasic newVertex = vertexType switch
			{
				GraphVertexType.Empty => Instantiate(_vertexItemEmptyPrefab , _vertexParent).GetComponent<VertexItemEmpty>(),
				GraphVertexType.Base => Instantiate(_vertexItemBasePrefab , _vertexParent).GetComponent<VertexItemBase>(),
				GraphVertexType.Mine => Instantiate(_vertexItemMinePrefab , _vertexParent).GetComponent<VertexItemMine>(),
				_ => throw new NotImplementedException()
			};

			int nameIndex = 1;
			if( _vertexList.Count > 0 )
			{
				var list = _vertexList.Select(x => x.NameIndex).ToList();
				nameIndex = FindNameIndex(list);
			}

			newVertex.Init(vertexType , nameIndex);

			_vertexList.Add(newVertex);
			//Selection.activeTransform = newVertex.transform;
		}

		public void AddEdge()
		{
			_edgeList.RemoveAll(x => x == null);

			var newEdge = Instantiate(_edgeItem , _edgeParent).GetComponent<EdgeItem>();

			int nameIndex = 1;
			if( _edgeList.Count > 0 )
			{
				var list = _edgeList.Select(x => x.NameIndex).ToList();
				nameIndex = FindNameIndex(list);
			}

			newEdge.Init(nameIndex);

			_edgeList.Add(newEdge);
			//Selection.activeTransform = newEdge.transform;
		}
		private int FindNameIndex( List<int> list )
		{
			list.Sort();
			int j = 1;
			for( int i = 0; i < list.Count; i++ )
			{
				if( list[i] > j )
				{
					return j;
				}
				j++;
			}
			return list[list.Count - 1] + 1;
		}

		public void Validate( Action onComplete , Action<string> onFail )
		{
			_vertexList.RemoveAll(x => x == null);
			_edgeList.RemoveAll(x => x == null);
			if( _vertexList.Count < 2 )
			{
				onFail?.Invoke("Vertex Count < 2");
				return;
			}

			if( _edgeList.Where(x => x.Vertex1 == null || x.Vertex2 == null).Any() == true )
			{
				EdgeItem item = _edgeList.Where(x => x.Vertex1 == null || x.Vertex2 == null).First();
				onFail?.Invoke($"Edge `{item.name}`  Vertex is null");
				return;
			}

			if( _edgeList.Where(x => x.Vertex1 == x.Vertex2).Any() == true )
			{
				EdgeItem item = _edgeList.Where(x => x.Vertex1 == x.Vertex2).First();
				onFail?.Invoke($"Edge `{item.name}` Vertex1 == Vertex2");
				return;
			}


			for( int i = 0; i < _edgeList.Count; i++ )
			{
				(VertexItemBasic v1, VertexItemBasic v2) = (_edgeList[i].Vertex1, _edgeList[i].Vertex2);
				for( int j = i + 1; j < _edgeList.Count; j++ )
				{
					if( _edgeList[j].Vertex1 == v1 && _edgeList[j].Vertex2 == v2 )
					{
						onFail?.Invoke($"Ege duplicate `{_edgeList[j].name}`");
						return;
					}
				}
			}

			foreach( var vertex in _vertexList )
			{
				if( _edgeList.Where(x => x.Vertex1 == vertex || x.Vertex2 == vertex).Any() == false )
				{
					onFail?.Invoke($"Vertex `{vertex.name}` not contains Edge");
					return;
				}
			}

			onComplete?.Invoke();
		}

		[HideInInspector]
		public string SaveFileJsonName;

		public void Save( Action onComplete , Action<string> onFail )
		{
			if( string.IsNullOrEmpty(SaveFileJsonName) )
			{
				onFail?.Invoke("File name is Empty");
				return;
			}
			try
			{
				save();
			}
			catch (Exception ex)
			{
				onFail?.Invoke(ex.Message);
				return;
			}
			onComplete?.Invoke();

			void save()
			{
				GraphDataConfig config = new();
				foreach( var edge in _edgeList )
				{
					config.EdgeData.Add(new EdgeData
					{
						NameIndex = edge.NameIndex ,
						Vertex1 = edge.Vertex1.NameIndex ,
						Vertex2 = edge.Vertex2.NameIndex ,
						Weight = edge.Weight
					});
				}
				foreach( var vertex in _vertexList )
				{
					float value = ( vertex is VertexItemWithValue valueObj ) ? valueObj.Value : 0.0f;
					VertexPosition pos = new()
					{
						X = vertex.transform.position.x ,
						Y = vertex.transform.position.y ,
						Z = vertex.transform.position.z ,
					};
					config.VertexData.Add(new VertexData
					{
						NameIndex = vertex.NameIndex ,
						Position = pos ,
						VertexType = vertex.VertexType ,
						Value = value
					});
				}

				string json = JsonConvert.SerializeObject(config , Formatting.Indented);
				using FileStream stream = new($"Assets/Resources/Levels/{SaveFileJsonName}.json" , FileMode.Create);
				using StreamWriter writer = new(stream);
				writer.Write(json);
			}
		}




#endif
		[CustomEditor(typeof(GraphDataBuilder))]
		class GraphDataBuilderCustomizer:Editor
		{
			public override void OnInspectorGUI()
			{
				if( GUI.changed )
				{
					EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
				}

				GraphDataBuilder mt = (GraphDataBuilder)target;
				Undo.RegisterCompleteObjectUndo(mt , "GraphDataBuilderCustomizer");

				GUILayout.Space(10);
				EditorGUILayout.BeginHorizontal();

				if( GUILayout.Button("Add Empty" , GUILayout.ExpandWidth(false)) )
				{
					mt.AddVertex(GraphVertexType.Empty);
				}
				if( GUILayout.Button("Add Base" , GUILayout.ExpandWidth(false)) )
				{
					mt.AddVertex(GraphVertexType.Base);
				}
				if( GUILayout.Button("Add Mine" , GUILayout.ExpandWidth(false)) )
				{
					mt.AddVertex(GraphVertexType.Mine);
				}

				EditorGUILayout.EndHorizontal();

				GUILayout.Space(10);
				EditorGUILayout.BeginHorizontal();

				if( GUILayout.Button("Add Edge" , GUILayout.ExpandWidth(false)) )
				{
					mt.AddEdge();
				}

				EditorGUILayout.EndHorizontal();


				GUILayout.Space(10);
				EditorGUILayout.BeginHorizontal();
				if( GUILayout.Button("Validate" , GUILayout.ExpandWidth(false)) )
				{
					mt.Validate
					(
						() => { Debug.Log("Validation Completed"); } ,
						s => { Debug.LogError(s); }

					);
				}
				EditorGUILayout.EndHorizontal();

				GUILayout.Space(10);
				EditorGUILayout.BeginHorizontal();

				EditorGUILayout.LabelField($"FileName to save Json - " , GUILayout.Width(150.0f));
				mt.SaveFileJsonName = (string)EditorGUILayout.TextField(mt.SaveFileJsonName , GUILayout.Width(200.0f));

				if( GUILayout.Button("Save" , GUILayout.ExpandWidth(false)) )
				{
					mt.Validate
					(
						save ,
						s => { Debug.LogError(s); }

					);
					void save()
					{
						mt.Save
						(
							()=> { Debug.Log("Save Completed"); },
							s=> { Debug.LogError(s); }
						);
					}
				}
				EditorGUILayout.EndHorizontal();


				DrawDefaultInspector();


			}
		}
	}
}
