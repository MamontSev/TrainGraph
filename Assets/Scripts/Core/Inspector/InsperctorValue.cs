using System.Collections.Generic;

using Mamont.Data.Graph.Builder;
using Mamont.Events;
using Mamont.Events.Signals;

using UnityEngine;

using Zenject;

namespace Mamont.Core.Inspector
{
	public class InsperctorValue:MonoBehaviour
	{
		private IEventBusService _eventBusService;
		[Inject]
		private void Construct
		(
			IEventBusService _eventBusService
		)
		{
			this._eventBusService = _eventBusService;
			_vertexList.Clear();
			_edgeList.Clear();
		}

		[SerializeField]
		private List<VertexValue> _vertexList = new();
		public void AddVertex( float startValue , int nameIndex , GraphVertexType vertexType )
		{
			var item = new VertexValue(startValue , nameIndex , vertexType);
			item.OnValueChanged += OnVetexValueChanged;
			_vertexList.Add(item);
		}

		[SerializeField]
		private  List<EdgeValue> _edgeList = new();
		public void AddEdge( int startValue , int nameIndex )
		{
			var item = new EdgeValue(startValue , nameIndex);
			item.OnValueChanged += OnEdgeValueChanged;
			_edgeList.Add(item);
		}

		[SerializeField]
		private TrainValue _movementSpeedValue;
		[SerializeField]
		private TrainValue _extractionTimeValue;
		public void AddTrain( (float movementSpeed, float extractionTime) tuple )
		{
			_movementSpeedValue = new(tuple.movementSpeed,"Скорость передвижения ");
			_movementSpeedValue.OnValueChanged += OnMovementSpeedChanged;

			_extractionTimeValue = new(tuple.extractionTime, "Скорость добычи ресурса ");
			_extractionTimeValue.OnValueChanged += OnExtractionTimeChanged;
		}

		private void OnMovementSpeedChanged( float value )
		{
			_eventBusService.Invoke(new TrainMovementSpeedChangedSignal(value));
		}
		private void OnExtractionTimeChanged( float value )
		{
			_eventBusService.Invoke(new TrainExtractionTimeChangedSignal(value));
		}

		private void OnVetexValueChanged( float value , int nameIndex )
		{
			_eventBusService.Invoke(new GraphVertexValueChangedSignal(value , nameIndex));
		}
		private void OnEdgeValueChanged( int value , int nameIndex )
		{
			_eventBusService.Invoke(new GraphEdgeValueChangedSignal(value , nameIndex));
		}

		private void Update()
		{
			_vertexList.ForEach(x => x.Update());
			_edgeList.ForEach(x => x.Update());
			_movementSpeedValue.Update();
			_extractionTimeValue.Update();
		}
	}
}
