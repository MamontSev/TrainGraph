using Mamont.Data.GameLevels;
using Mamont.Data.Train;
using Mamont.Events;
using Mamont.GameLevels;
using Mamont.GeneralStateMashine;
using Mamont.Log;
using Mamont.SceneControl;
using Mamont.UI.General.Loading;

using UnityEngine;

using Zenject;

namespace Mamont.Installer
{
	public class BootstrapInstaller:MonoInstaller
	{
		public override void InstallBindings()
		{
			BindLoadingPanel();
			BindSceneControlService();
			BindGeneralGameStateMachine();
			BindBusService();
			BindLogService();
			BindGameLevelsService();
			BindGameLevelsConfig();
			BindTrainDataConfig();
		}

		[SerializeField]
		private LoadingPanel _loadingPanel;
		private void BindLoadingPanel()
		{
			Container
			.Bind<ILoadingPanel>()
			.To<LoadingPanel>()
			.FromInstance(_loadingPanel)
			.AsSingle()
			.NonLazy();
		}

		[SerializeField]
		private SceneControlService _sceneControlService;
		private void BindSceneControlService()
		{
			Container
			.Bind<ISceneControlService>()
			.To<SceneControlService>()
			.FromInstance(_sceneControlService)
			.AsSingle()
			.NonLazy();
		}

		private void BindGeneralGameStateMachine()
		{
			Container.Bind<GeneralGameStateMachine>().AsSingle().NonLazy();
			Container.Bind<GeneralStateFactory>().AsSingle().NonLazy();
		}

		private void BindBusService()
		{
			Container.Bind<IEventBusService>().To<EventBusService>().AsSingle().NonLazy();
		}

		private void BindLogService()
		{
			Container.Bind<ILogService>().To<LogService>().AsSingle().NonLazy();
		}

		private void BindGameLevelsService()
		{
			Container.Bind<IGameLevelsService>().To<GameLevelsService>().AsSingle().NonLazy();
		}

		[SerializeField]
		private GameLevelsConfig _gameLevelsConfig;
		private void BindGameLevelsConfig()
		{
			Container.Bind<GameLevelsConfig>().FromInstance(_gameLevelsConfig).AsSingle().NonLazy();
		}

		
		[SerializeField]
		private TrainDataConfigTable _trainDataConfigTable;
		private void BindTrainDataConfig()
		{
			Container
			.Bind<ITrainDataConfig>()
			.To<TrainDataConfig>()
			.AsSingle()
			.WithArguments(_trainDataConfigTable)
			.NonLazy();
		}
	}
}
