using UnityEngine;

using Zenject;

namespace Mamont.UI.MainMenu.HUD
{
	public class MainMenuHudFactory:MonoBehaviour
	{
		private DiContainer _diContainer;
		[Inject]
		private void Construct
		(
			DiContainer _diContainer
		)
		{
			this._diContainer = _diContainer;
		}

		private void Start()
		{
			InitHUD();
		}

		[SerializeField]
		private MainMenuHudOverlayView _hudMenu;

		private void InitHUD()
		{
			_hudMenu.Bind(_diContainer.Instantiate<MainMenuHudOverlayViewModel>());
		}
	}
}
