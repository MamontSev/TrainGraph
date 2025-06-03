using System;
using System.Collections.Generic;

namespace Mamont.Core.Train.Model
{
	public interface ITrainModelActionsListener
	{
		event Action<int> OnSetCurrVetrex;
		event Action<List<int> , int> OnSetPathToBase;
		event Action<List<int> , int> OnSetPathToMine;
		event Action<float> OnMineProgress;
		event Action OnMineComplete;
		event Action<int , int , float> OnPathProgress;
		event Action OnPathComplete;
		event Action OnChangedGraphValue;
	}

}
