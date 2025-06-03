using Mamont.Core.GameLoop;
using Mamont.Core.Graph;
using Mamont.Core.Graph.Notion;
using Mamont.Core.Graph.Viewer;
using Mamont.Core.Score;
using Mamont.Core.Train;
using Mamont.UI.LevelMenu.HUD;
using Mamont.UI.LevelMenu.PopUp;

using Zenject;

namespace Mamont.Installer
{
	public class GamePlayInstaller:MonoInstaller
	{
		public override void InstallBindings()
		{
			BindGraphViewer();
			BindLevelScoreControl();
			BindLevelStateControl();
			BindGraphCreator();
			BindGraphNotion();
			BindLevelMenuPopUpFacrtory();
			BindLevelMenuHudFactory();
			BindTrainViewerFactory();
		}



		private void BindGraphViewer()
		{
			Container
			.Bind<GraphViewer>()
			.FromComponentInHierarchy()
			.AsSingle()
			.NonLazy();
		}
		private void BindLevelScoreControl()
		{
			Container.Bind<IScoreControl>().To<ScoreControl>().AsSingle().NonLazy();
		}
		
		private void BindLevelStateControl()
		{
			Container.BindInterfacesAndSelfTo<GameLoopControl>().AsSingle().NonLazy();
		}
		private void BindGraphCreator()
		{
			Container.Bind<GraphCreator>().AsSingle().NonLazy();
		}
		private void BindGraphNotion()
		{
			Container.BindInterfacesAndSelfTo<GraphNotion>().AsSingle().NonLazy();
		}
		

		private void BindLevelMenuPopUpFacrtory()
		{
			Container
			.Bind<ILevelMenuPopUpFacrtory>()
			.To<LevelMenuPopUpFacrtory>()
			.FromComponentInHierarchy()
			.AsSingle()
			.NonLazy();
		}

		private void BindLevelMenuHudFactory()
		{
			Container
			.Bind<LevelMenuHudFactory>()
			.FromComponentInHierarchy()
			.AsSingle()
			.NonLazy();
		}

		private void BindTrainViewerFactory()
		{
			Container
			.Bind<TrainFactory>()
			.FromComponentInHierarchy()
			.AsSingle()
			.NonLazy();
		}

	}
}
