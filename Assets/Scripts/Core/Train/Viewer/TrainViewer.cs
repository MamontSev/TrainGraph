﻿using System.Collections.Generic;
using System.IO;

using Mamont.Core.Graph.Viewer;
using Mamont.Core.Train.Model;
using Mamont.Events;
using Mamont.Events.Signals;

using TMPro;

using UnityEngine;

using Zenject;

namespace Mamont.Core.Train.Viewer
{
	public class TrainViewer:MonoBehaviour
	{
		private GraphViewer _graphViewer;
		private ITrainModelActionsListener _asctionListener;
		private IEventBusService _eventBusService;
		[Inject]
		private void Construct
		(
			GraphViewer _graphViewer ,
			ITrainModelActionsListener _asctionListener ,
			IEventBusService _eventBusService ,
			(float movementSpeed, float extractionTime) tuple
		)
		{
			this._graphViewer = _graphViewer;
			this._asctionListener = _asctionListener;
			this._eventBusService = _eventBusService;
			SetMovementExtraction(tuple);
		}
		private void OnEnable()
		{
			_asctionListener.OnCompleteGoToVertex += SetOnPos;
			_asctionListener.OnSetPathToBase += SetPathBase;
			_asctionListener.OnSetPathToMine += SetPathMine;
			_asctionListener.OnSetPathStartIndex += SetPathStartPos;
			_asctionListener.OnMineProgress += ShowValue;
			_asctionListener.OnMineComplete += HideValue;
			_asctionListener.OnPathProgress += SetOnEdge;
			_asctionListener.OnPathComplete += HidePath;
		}
		private void OnDisable()
		{
			_asctionListener.OnCompleteGoToVertex -= SetOnPos;
			_asctionListener.OnSetPathToBase -= SetPathBase;
			_asctionListener.OnSetPathToMine -= SetPathMine;
			_asctionListener.OnSetPathStartIndex -= SetPathStartPos;
			_asctionListener.OnMineProgress -= ShowValue;
			_asctionListener.OnMineComplete -= HideValue;
			_asctionListener.OnPathProgress -= SetOnEdge;
			_asctionListener.OnPathComplete -= HidePath;
		}

		[SerializeField]
		private float _movementSpeed;
		private float _prevMovementSpeed;
		[SerializeField]
		public float _extractionTime;
		private float _prevExtractionTime;

		private void SetMovementExtraction( (float movementSpeed, float extractionTime) tuple )
		{
			_movementSpeed = tuple.movementSpeed;
			_prevMovementSpeed = tuple.movementSpeed;

			_extractionTime = tuple.extractionTime;
			_prevExtractionTime = tuple.extractionTime;
		}

		private void Update()
		{
			CheckMovement();
			CheckExstraction();
			UpdateLinerenderer();
		}

		private void CheckMovement()
		{
			if( _movementSpeed == _prevMovementSpeed )
				return;
			if( _movementSpeed < 0.0f )
				_movementSpeed = 0.0f;
			_prevMovementSpeed = _movementSpeed;
			_eventBusService.Invoke(new TrainMovementSpeedChangedSignal(_movementSpeed));
		}
		private void CheckExstraction()
		{
			if( _extractionTime == _prevExtractionTime )
				return;
			if( _extractionTime < 0.0f )
				_extractionTime = 0.0f;
			_prevExtractionTime = _extractionTime;
			_eventBusService.Invoke(new TrainExtractionTimeChangedSignal(_extractionTime));
		}


		[SerializeField]
		private Transform _movableObj;
		private void SetOnPos( int vertexName )
		{
			_movableObj.position = _graphViewer.GetVertexTransform(vertexName).position;
		}
		private void SetOnEdge( int vertexName1 , int vertexName2 , float value )
		{
			Vector3 pos1 = _graphViewer.GetVertexTransform(vertexName1).position;
			Vector3 pos2 = _graphViewer.GetVertexTransform(vertexName2).position;
			_movableObj.position = Vector3.Lerp(pos1 , pos2 , value);
		}

		private List<int> _path;
		[SerializeField]
		private LineRenderer _lineRenderer;
		[SerializeField]
		private Material _mine;
		[SerializeField]
		private Material _base;
		private void SetPathMine( List<int> path )
		{
			_lineRenderer.material = _mine;
			_path = path;
			_lineRenderer.enabled = true;
			_pathStartIndex = 0;
		}
		private void SetPathBase( List<int> path )
		{
			_lineRenderer.material = _base;
			_path = path;
			_lineRenderer.enabled = true;
			_pathStartIndex = 0;
		}
		private int _pathStartIndex = 0;
		private void SetPathStartPos( int index )
		{
			_pathStartIndex = index;
		}
		private void HidePath()
		{
			_lineRenderer.enabled = false;
		}
		private void UpdateLinerenderer()
		{
			if( _lineRenderer.enabled == false )
				return;
			int partLength = _path.Count - _pathStartIndex;
			_lineRenderer.positionCount = partLength + 1;
			_lineRenderer.SetPosition(0 , _movableObj.position);
			int partIndex = _pathStartIndex;
			for( int i = 0; i < partLength; i++ )
			{
				_lineRenderer.SetPosition(i + 1 , _graphViewer.GetVertexTransform(_path[partIndex]).position);
				partIndex++;
			}
		}

		[SerializeField]
		private TextMeshPro _gizmoValue;
		private void ShowValue( float value )
		{
			string s = value.ToString();
			if( s.Length > 5 )
				s = s.Substring(0 , 5);
			_gizmoValue.text = s;
		}
		public void HideValue()
		{
			_gizmoValue.text = "";
		}
	}
}
