using System;
using System.Collections.Generic;

namespace Mamont.Data.Train
{
	public interface ITrainDataConfig
	{
		(float movementSpeed, float extractionTime) GetTrainData( TrainType type );
	}
	public class TrainDataConfig:ITrainDataConfig
	{
		private readonly TrainDataConfigTable _dataTable;
		public TrainDataConfig( TrainDataConfigTable _dataTable )
		{
			this._dataTable = _dataTable;
			Init();
		}


		public (float movementSpeed, float extractionTime) GetTrainData( TrainType type )
		{
			return _trainDict[type];
		}


		private Dictionary<TrainType ,(float movementSpeed, float extractionTime) > _trainDict = new();
		private void Init()
		{
			if( _dataTable == null )
			{
				throw new Exception("Error Load TrainDataConfigTable");
			}
			if( _dataTable.ItemList.Count != Enum.GetValues(typeof(TrainType)).Length )
			{
				throw new Exception("Error  TrainDataConfigTable not contain any");
			}

			foreach( TrainType type in Enum.GetValues(typeof(TrainType)) )
			{
				foreach( var item in _dataTable.ItemList )
				{
					if( item.SelfType == type )
					{
						_trainDict.Add(type , (item.MovementSpeed,item.ExtractionTime));
					}
				}
			}
		}
	}
}





