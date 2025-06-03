using Manmont.Tools.StateMashine;

using UnityEngine;

namespace Mamont.Core.Train.Model
{
	public class MineProgressState:ITrainModelState, IEnterState,IExitState, IUpdateState
	{
		private readonly ModelData _selfData;
		private readonly TrainModelActions _selfActions;
		private readonly TrainModelStateMachine _stateMachine;
		public MineProgressState
		(
			ModelData _selfData ,
			TrainModelActions _selfActions ,
			TrainModelStateMachine _stateMachine
		)
		{
			this._selfData = _selfData;
			this._selfActions = _selfActions;
			this._stateMachine = _stateMachine;
		}
		private float _timeMineRemaining;
		private float _timePassed;
		public void Enter()
		{
			_timePassed = 0.0f;
		}

		public void Update()
		{
			_timePassed += Time.deltaTime;
			_timeMineRemaining = _selfData.ExtractionTime * _selfData.VertexDict[_selfData.CurrVertexName].Value;
			if( _timePassed >= _timeMineRemaining )
			{
				_timePassed = _timeMineRemaining;
				_stateMachine.Enter<GoToBaseState>();
			}
			else
			{
				_selfActions.MineProgress(_timeMineRemaining - _timePassed);
			}
		}

		public void Exit()
		{
			_selfActions.MineComplete();
		}
	}
}
