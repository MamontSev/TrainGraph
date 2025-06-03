using System;

using Mamont.Core.Graph.Notion;

using Manmont.Tools.StateMashine;

using UnityEngine;

namespace Mamont.Core.Train.Model
{
	public abstract class AbstractGoToState:ITrainModelState, IEnterState, IExitState, IUpdateState
	{
		protected readonly GraphNotion _graphNotion;
		protected readonly ModelData _selfData;
		protected readonly TrainModelActions _selfActions;
		protected readonly TrainModelStateMachine _stateMachine;
		public AbstractGoToState
		(
			GraphNotion _graphNotion ,
			ModelData _selfData ,
			TrainModelActions _selfActions ,
			TrainModelStateMachine _stateMachine
		)
		{
			this._graphNotion = _graphNotion;
			this._selfData = _selfData;
			this._selfActions = _selfActions;
			this._stateMachine = _stateMachine;
		}
		public virtual void Enter()
		{
			_selfActions.OnChangedGraphValue += OnGraVertexEdgeValueChanged;
		}

		public void Exit()
		{
			_selfActions.OnChangedGraphValue -= OnGraVertexEdgeValueChanged;
		}
		public abstract void Update();

		private void OnGraVertexEdgeValueChanged()
		{
			
			_wasChangeGraphValueOnGo = true;
		}

		

		private int _currPathIndex;
		private int _targetVertexName;
		private ModelEdgeData _currEdge;
		private float _currGoValue;
		private bool _wasChangeGraphValueOnGo;
		protected void GoToVertex( int _pathIndex )
		{
			_wasChangeGraphValueOnGo = false;
			_currGoValue = 0.0f;
			_currPathIndex = _pathIndex;
			_targetVertexName = _selfData.CurrWalkPath[_currPathIndex];

			foreach( var item in _selfData.EdgesDict )
			{
				if( item.Value.Vertex1 == _selfData.CurrVertexName && item.Value.Vertex2 == _targetVertexName )
				{
					_currEdge = item.Value;
					break;
				}
				if( item.Value.Vertex2 == _selfData.CurrVertexName && item.Value.Vertex1 == _targetVertexName )
				{
					_currEdge = item.Value;
					break;
				}
			}
		}

		protected void UpdateGo( Action onComplete , Action onVertexChanged )
		{
			_currGoValue += _selfData.MovementSpeed * Time.deltaTime;
			if(  _currEdge.Weight != 0.0f )
			{
				_selfActions.PathProgress(_selfData.CurrVertexName , _targetVertexName , _currGoValue / _currEdge.Weight);
			}
			if( _currGoValue < _currEdge.Weight )
			{
				return;
			}
			_currGoValue = _currEdge.Weight;
			_selfData.CurrVertexName = _targetVertexName;
			_selfActions.SetCurrVertex(_selfData.CurrVertexName);

			if( _currPathIndex == _selfData.CurrWalkPath.Count - 1 )
			{
				_selfActions.PathComplete();
				onComplete?.Invoke();
				return;
			}

			if( _wasChangeGraphValueOnGo )
			{
				_selfActions.PathComplete();
				onVertexChanged?.Invoke();
				return;
			}
			GoToVertex(_currPathIndex + 1);
		}


	}
}
