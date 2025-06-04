using System;
using System.Collections.Generic;

namespace Mamont.Core.Train.Model
{
	public interface ITrainModelActionsListener
	{
		event Action<int> OnCompleteGoToVertex;
		event Action<List<int>> OnSetPathToBase;
		event Action<List<int>> OnSetPathToMine;
		event Action<float> OnMineProgress;
		event Action OnMineComplete;
		event Action<int , int , float> OnPathProgress;
		event Action OnPathComplete;
		event Action OnChangedGraphValue;
		event Action<int> OnSetPathStartIndex;
	}

}
