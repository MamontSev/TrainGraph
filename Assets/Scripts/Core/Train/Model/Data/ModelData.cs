using System.Collections.Generic;

using UnityEngine;

namespace Mamont.Core.Train.Model
{
	public class ModelData
	{
        public ModelData( (float movementSpeed, float extractionTime) tuple )
        {
			MovementSpeed = tuple.movementSpeed;
			ExtractionTime = tuple.extractionTime;
		}
        public float MovementSpeed;
		public float ExtractionTime;

		public Dictionary<int , ModelVertexData> VertexDict = new();
		public Dictionary<int , ModelEdgeData> EdgesDict = new();
		public int CurrVertexName;
		public int TargetVertexName;

	}

}
