﻿using System.Collections.Generic;

using Mamont.Events;
using Mamont.Events.Signals;

using UnityEngine;

using Zenject;

namespace Mamont.Core.Graph.Viewer
{
	public class GraphViewer:MonoBehaviour
	{
		private IEventBusService _eventBusService;
		[Inject]
		private void Construct( IEventBusService _eventBusService )
		{
			this._eventBusService = _eventBusService;
		}
		[SerializeField]
		private ViewVertexItemEmpty _emptyPrefab;
		[SerializeField]
		private ViewVertexItemMine _minePrefab;
		[SerializeField]
		private ViewVertexItemBase _basePrefab;

		[SerializeField]
		private Transform _vertexContainer;

		private readonly List<ViewVertexItemBasic> _vertexList = new();


		[SerializeField]
		private ViewEdgeItem _edgePrefab;

		[SerializeField]
		private Transform _edgeContainer;

		private readonly List<ViewEdgeItem> _edgeList = new();


		public void AddVertexEmpty( int index , Vector3 pos )
		{
			var item = Instantiate(_emptyPrefab , _vertexContainer).GetComponent<ViewVertexItemEmpty>();
			item.Init(index , pos);
			_vertexList.Add(item);
		}
		public void AddVertexMine( int index , Vector3 pos , float startValue )
		{
			var item = Instantiate(_minePrefab , _vertexContainer).GetComponent<ViewVertexItemMine>();
			item.Init(startValue , index , pos);
			item.OnValueChanged += OnVetexValueChanged;
			_vertexList.Add(item);
		}
		public void AddVertexBase( int index , Vector3 pos , float startValue )
		{
			var item = Instantiate(_basePrefab , _vertexContainer).GetComponent<ViewVertexItemBase>();
			item.Init(startValue , index , pos);
			item.OnValueChanged += OnVetexValueChanged;
			_vertexList.Add(item);
		}

		public void AddEdge( int vertexName1 , int vertexName2 , int startValue , int index )
		{
			Transform pos1 = _vertexList.Find(x => x.NameIndex == vertexName1).transform;
			Transform pos2 = _vertexList.Find(x => x.NameIndex == vertexName2).transform;

			var item = Instantiate(_edgePrefab , _edgeContainer).GetComponent<ViewEdgeItem>();
			item.Init(startValue , index , pos1 , pos2);
			item.OnValueChanged += OnEdgeValueChanged;
			_edgeList.Add(item);
		}

		public Transform GetVertexTransform(int vertexName)
		{
			return _vertexList.Find(x => x.NameIndex == vertexName).transform;
		}

		private void OnVetexValueChanged( float value , int nameIndex )
		{
			_eventBusService.Invoke(new GraphVertexValueChangedSignal(value , nameIndex));
		}
		private void OnEdgeValueChanged( int value , int nameIndex )
		{
			_eventBusService.Invoke(new GraphEdgeValueChangedSignal(value , nameIndex));
		}
	}
}
