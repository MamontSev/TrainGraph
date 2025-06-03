using System;
using System.Collections.Generic;

using UnityEngine;

namespace Mamont.Data.GameLevels
{
	[CreateAssetMenu(menuName = "Data/GameLevelsConfig" , fileName = "GameLevelsConfig.asset")]
	public class GameLevelsConfig:ScriptableObject	
	{
		public List<String> GraphConfigPathList = new();

	}
}
