using System;
using System.Collections.Generic;

namespace Mamont.Core.Train.Model
{
	public class TrainModelActions:ITrainModelActionsListener
	{
		public event Action<int> OnCompleteGoToVertex;
		public event Action<List<int>> OnSetPathToBase;
		public event Action<List<int>> OnSetPathToMine;
		public event Action<int> OnSetPathStartIndex;
		public event Action<float> OnMineProgress;
		public event Action OnMineComplete;
		public event Action<int , int , float> OnPathProgress;
		public event Action OnPathComplete;
		public event Action OnChangedGraphValue;

		public void CompleteGoToVertex( int index )
		{
			OnCompleteGoToVertex?.Invoke(index);
		}
		public void SetPathToBase( List<int> path )
		{
			OnSetPathToBase?.Invoke(path);
		}
		public void SetPathToMine( List<int> path )
		{
			OnSetPathToMine?.Invoke(path);
		}
		public void SetPathStartIndex(int index )
		{
			OnSetPathStartIndex?.Invoke(index);
		}
		public void MineProgress( float timeMineRemaining )
		{
			OnMineProgress?.Invoke(timeMineRemaining);
		}
		public void MineComplete()
		{
			OnMineComplete?.Invoke();
		}
		public void PathProgress( int currVertexName , int targetVertexName , float progress )
		{
			OnPathProgress?.Invoke(currVertexName , targetVertexName , progress);
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
