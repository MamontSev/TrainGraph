using System;
using System.Collections.Generic;

using Mamont.Data.GameLevels;
using Mamont.Data.Graph.Config;
using Mamont.Data.Train;

using Newtonsoft.Json;

using UnityEngine;

namespace Mamont.GameLevels
{
	public class GameLevelsService:IGameLevelsService
	{
		private readonly GameLevelsConfig _gameLevelsConfig;

		public GameLevelsService( GameLevelsConfig gameLevelsConfig )
		{
			this._gameLevelsConfig = gameLevelsConfig;
		}

		private readonly Dictionary<int , GraphDataConfig> _graphConfigDict = new();

		private bool _isLoadedData = false;
		public void LoadData( Action onComplete , Action<string> onFail )
		{
			if( _isLoadedData )
			{
				onComplete?.Invoke();
				return;
			}

			for( int i = 0; i < _gameLevelsConfig.GraphConfigPathList.Count; i++ )
			{
				try
				{
					TextAsset targetFile = Resources.Load<TextAsset>(_gameLevelsConfig.GraphConfigPathList[i]);
					string json = targetFile.text;
					var data = JsonConvert.DeserializeObject<GraphDataConfig>(json);
					_graphConfigDict.Add(i , data);
				}
				catch(Exception ex)
				{
					onFail?.Invoke(ex.Message);
					return;
				}
				
			}
			_isLoadedData = true;
			onComplete?.Invoke();
		}

		public int LevelsCount => _graphConfigDict.Count;

		public int CurrPlayedLevel
		{
			get;
			set;
		} = 0;
		public TrainType CurrSelectedTrain
		{
			get;
			set;
		} = TrainType.Train1;

		public GraphDataConfig GetDataConfig( int levelIndex ) => _graphConfigDict[levelIndex];
	}
}
