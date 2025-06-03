using System.Collections.Generic;

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
			_asctionListener.OnSetCurrVetrex += SetOnPos;
			_asctionListener.OnSetPathToBase += SetPathBase;
			_asctionListener.OnSetPathToMine += SetPathMine;
			_asctionListener.OnMineProgress += ShowValue;
			_asctionListener.OnMineComplete += HideValue;
			_asctionListener.OnPathProgress += SetOnEdge;
			_asctionListener.OnPathComplete += HidePath;
		}
		private void OnDisable()
		{
			_asctionListener.OnSetCurrVetrex -= SetOnPos;
			_asctionListener.OnSetPathToBase -= SetPathBase;
			_asctionListener.OnSetPathToMine -= SetPathMine;
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
			if(_extractionTime < 0.0f)
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

		[SerializeField]
		private LineRenderer _lineRenderer;
		[SerializeField]
		private Material _mine;
		[SerializeField]
		private Material _base;
		private void SetPathMine( List<int> path , int startVertexName )
		{
			_lineRenderer.material = _mine;
			ShowLinerenderer(path , startVertexName);
		}
		private void SetPathBase( List<int> path , int startVertexName )
		{
			_lineRenderer.material = _base;
			ShowLinerenderer(path , startVertexName);
		}
		private void HidePath()
		{
			_lineRenderer.enabled = false;
		}
		private void ShowLinerenderer( List<int> path , int startVertexName )
		{
			path.Insert(0 , startVertexName);
			_lineRenderer.enabled = true;
			_lineRenderer.positionCount = path.Count;
			for( int i = 0; i < path.Count; i++ )
			{
				_lineRenderer.SetPosition(i , _graphViewer.GetVertexTransform(path[i]).position);
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
