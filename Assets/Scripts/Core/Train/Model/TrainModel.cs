using System;
using System.Collections.Generic;
using System.Linq;

using Mamont.Core.GameLoop;
using Mamont.Core.Graph.Notion;
using Mamont.Core.Score;
using Mamont.Data.Graph.Builder;
using Mamont.Events;
using Mamont.Events.Signals;

using UnityEngine;

namespace Mamont.Core.Train.Model
{
	public class TrainModel:IGameLoopUpdate
	{
		private readonly IEventBusService _eventBusService;
		private readonly GraphNotion _graphNotion;
		private readonly IScoreControl _scoreControl;
		private readonly ModelData _selfData;
		private readonly TrainModelActions _selfActions;
		private readonly IGameLoopControl _gameLoopControl;
		public TrainModel
		(
			IEventBusService _eventBusService ,
			GraphNotion _graphNotion ,
			IScoreControl _scoreControl ,
			ModelData _selfData ,
			TrainModelActions _selfActions ,
			IGameLoopControl _gameLoopControl
		)
		{
			this._eventBusService = _eventBusService;
			this._graphNotion = _graphNotion;
			this._scoreControl = _scoreControl;
			this._selfData = _selfData;
			this._selfActions = _selfActions;
			this._gameLoopControl = _gameLoopControl;
			CreateStateMachine();
			Subscribe();
			
		}

		private TrainModelStateMachine _stateMachine;
		private void CreateStateMachine()
		{
			_stateMachine = new TrainModelStateMachine();
			_stateMachine.Register<StartAwaitState>(new StartAwaitState());
			_stateMachine.Register<GoToMineStateState>(new GoToMineStateState(_graphNotion, _selfData, _selfActions, _stateMachine));
			_stateMachine.Register<MineProgressState>(new MineProgressState( _selfData, _selfActions, _stateMachine));
			_stateMachine.Register<GoToBaseState>(new GoToBaseState(_graphNotion , _selfData , _selfActions , _stateMachine));
			_stateMachine.Register<BaseProgressState>(new BaseProgressState(_selfData ,  _stateMachine, _scoreControl));

			_stateMachine.Enter<StartAwaitState>();
		}

		private void Subscribe()
		{
			_eventBusService.Subscribe<GraphEdgeValueChangedSignal>(OnGraphEdgeValueChangedSignal);
			_eventBusService.Subscribe<GraphVertexValueChangedSignal>(OnGraphVertexValueChangedSignal);
			_eventBusService.Subscribe<TrainMovementSpeedChangedSignal>(OnTrainMovementSpeedChangedSignal);
			_eventBusService.Subscribe<TrainExtractionTimeChangedSignal>(OnTrainExtractionTimeChangedSignal);
			_eventBusService.Subscribe<ExitGamePlayState>(OnExitGamePlayState);
			_eventBusService.Subscribe<LevelInitCompletedSignal>(OnLevelInitCompletedSignal);
		}

		private void Unsubscribe()
		{
			_eventBusService.Unsubscribe<GraphEdgeValueChangedSignal>(OnGraphEdgeValueChangedSignal);
			_eventBusService.Unsubscribe<GraphVertexValueChangedSignal>(OnGraphVertexValueChangedSignal);
			_eventBusService.Unsubscribe<ExitGamePlayState>(OnExitGamePlayState);
			_eventBusService.Unsubscribe<LevelInitCompletedSignal>(OnLevelInitCompletedSignal);
		}

		private void OnExitGamePlayState( ExitGamePlayState state )
		{
			Unsubscribe();
		}

		private void OnTrainExtractionTimeChangedSignal( TrainExtractionTimeChangedSignal signal )
		{
			_selfData.ExtractionTime = signal.Value;
		}

		private void OnTrainMovementSpeedChangedSignal( TrainMovementSpeedChangedSignal signal )
		{
			_selfData.MovementSpeed = signal.Value;
		}



		private void OnGraphVertexValueChangedSignal( GraphVertexValueChangedSignal signal )
		{
			_selfData.VertexDict[signal.NameIndex].Value = signal.Value;
			_selfActions.ChangedGraphValue();

		}

		private void OnGraphEdgeValueChangedSignal( GraphEdgeValueChangedSignal signal )
		{
			_selfData.EdgesDict[signal.NameIndex].Weight = signal.Value;
			_selfActions.ChangedGraphValue();
		}


		private void OnLevelInitCompletedSignal( LevelInitCompletedSignal signal )
		{
			_gameLoopControl.Register(this);

			var list = _selfData.VertexDict.Keys.ToList();
			int r = UnityEngine.Random.Range(0 , list.Count);
			_selfData.CurrVertexName = list[r];
			_selfData.TargetVertexName = _selfData.CurrVertexName;
			_selfActions.CompleteGoToVertex (_selfData.CurrVertexName);

			_stateMachine.Enter<GoToMineStateState>();
		}

		public void Update()
		{
			_stateMachine.Update();
		}
	}

}
