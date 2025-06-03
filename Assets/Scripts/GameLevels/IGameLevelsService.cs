using System;

using Mamont.Data.Graph.Config;
using Mamont.Data.Train;

namespace Mamont.GameLevels
{
	public interface IGameLevelsService
	{
		int LevelsCount
		{
			get;
		}

		GraphDataConfig GetDataConfig( int levelIndex );
		void LoadData( Action onComplete , Action<string> onFail );

		public int CurrPlayedLevel
		{
			get;
			set;
		}
		public TrainType CurrSelectedTrain
		{
			get;
			set;
		}
	}
}