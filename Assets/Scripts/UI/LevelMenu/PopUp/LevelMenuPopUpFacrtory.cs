using Mamont.Core.GameLoop;
using Mamont.Log;
using Mamont.UI.General.PopUp;
using Mamont.UI.MVVM;

using UnityEngine;

using Zenject;

namespace Mamont.UI.LevelMenu.PopUp
{
	public interface ILevelMenuPopUpFacrtory:IBasePopUpFactory
	{
	}
	public class LevelMenuPopUpFacrtory:BasePopUpFactory, ILevelMenuPopUpFacrtory
	{
		private IGameLoopControl _gameLoopControl;
		[Inject]
		private void Construct(
			ILogService _logService ,
			DiContainer _diContainer,
			IGameLoopControl _gameLoopControl
		)
		{
			this._logService = _logService;
			this._diContainer = _diContainer;
			this._gameLoopControl = _gameLoopControl;
		}


		protected sealed override void InitPrefabs()
		{
			_prefabDict.Add(typeof(LevelPauseMenuViewModel) , _levelPauseMenuPrefab.gameObject);
		}

		[SerializeField]
		private LevelPauseMenuView _levelPauseMenuPrefab;

		public sealed override void Show<T>( T vm )
		{
			base.Show(vm);
			_gameLoopControl.Pause();
		}


		protected sealed override void OnClozeItem( IPopUpView item )
		{
			base.OnClozeItem(item);
			_gameLoopControl.UnPause();
		}


	}
}
