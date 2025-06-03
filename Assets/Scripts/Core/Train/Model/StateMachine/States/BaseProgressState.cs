using Mamont.Core.Score;

using Manmont.Tools.StateMashine;

namespace Mamont.Core.Train.Model
{
	public class BaseProgressState:ITrainModelState, IEnterState
	{
		private readonly ModelData _selfData;
		private readonly TrainModelStateMachine _stateMachine;
		private readonly IScoreControl _scoreControl;
		public BaseProgressState
		(
			ModelData _selfData ,
			TrainModelStateMachine _stateMachine,
			IScoreControl _scoreControl
		)
		{
			this._selfData = _selfData;
			this._stateMachine = _stateMachine;
			this._scoreControl = _scoreControl;
		}
		public void Enter()
		{
			float collectedCount = _selfData.VertexDict[_selfData.CurrVertexName].Value;
			_scoreControl.AddScore(collectedCount);
			_stateMachine.Enter<GoToMineStateState>();
		}

	}
}
