using Mamont.UI.MainMenu.HUD;
using Mamont.UI.MainMenu.Pages;
using Mamont.UI.MainMenu.PopUp;

using Zenject;

namespace Mamont.Installer
{
	public class MenuInstaller:MonoInstaller
	{
		public override void InstallBindings()
		{
			BindMainMenuPopUpFactory();
			BindMainMenuHudFactory();
			BindMainMenuPagesFactory();
		}

		private void BindMainMenuPopUpFactory()
		{
			Container
			.Bind<IMainMenuPopUpFactory>()
			.To<MainMenuPopUpFactory>()
			.FromComponentInHierarchy()
			.AsSingle()
			.NonLazy();
		}

		private void BindMainMenuHudFactory()
		{
			Container
			.Bind<MainMenuHudFactory>()
			.FromComponentInHierarchy()
			.AsSingle()
			.NonLazy();
		}

		private void BindMainMenuPagesFactory()
		{
			Container
			.Bind<IMainMenuPagesFactory>()
			.To<MainMenuPagesFactory>()
			.FromComponentInHierarchy()
			.AsSingle()
			.NonLazy();
		}
	}
}
