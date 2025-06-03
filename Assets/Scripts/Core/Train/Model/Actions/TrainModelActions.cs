using System;
using System.Collections.Generic;

namespace Mamont.Core.Train.Model
{
	public class TrainModelActions:ITrainModelActionsListener
	{
		public event Action<int> OnSetCurrVetrex;
		public event Action<List<int> , int> OnSetPathToBase;
		public event Action<List<int> , int> OnSetPathToMine;
		public event Action<float> OnMineProgress;
		public event Action OnMineComplete;
		public event Action<int,int,float> OnPathProgress;
		public event Action OnPathComplete;
		public event Action OnChangedGraphValue;

		public void SetCurrVertex( int index )
		{
			OnSetCurrVetrex?.Invoke(index);
		}
		public void SetPathToBase( List<int> path , int startVertex )
		{
			OnSetPathToBase?.Invoke(path , startVertex);
		}
		public void SetPathToMine( List<int> path , int startVertex )
		{
			OnSetPathToMine?.Invoke(path , startVertex);
		}
		public void MineProgress( float timeMineRemaining )
		{
			OnMineProgress?.Invoke(timeMineRemaining);
		}
		public void MineComplete()
		{
			OnMineComplete?.Invoke();
		}
		public void PathProgress(int currVertexName, int targetVertexName, float progress )
		{
			OnPathProgress?.Invoke(currVertexName, targetVertexName,progress);
		}
		public void PathComplete()
		{
			OnPathComplete?.Invoke();
		}
		public void ChangedGraphValue()
		{
			OnChangedGraphValue?.Invoke();
		}
	}

}
