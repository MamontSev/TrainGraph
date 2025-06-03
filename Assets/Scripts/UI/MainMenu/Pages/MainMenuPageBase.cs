using Mamont.UI.MVVM;

using UnityEngine;

namespace Mamont.UI.MainMenu.Pages
{
	public abstract class MainMenuPageBase<T> :BaseShowHideView<T>  where T: IMainMenuPageViewModel 
	{
		[SerializeField]
		private GameObject _skinGO;

		private bool? _isShow = null;
		public sealed override void Hide()
		{
			if( _isShow == false )
			{
				return;
			}
			OnHide();
			_isShow = false;
			_skinGO.SetActive(false);
		}

		public sealed override void Show()
		{
			if( _isShow == true )
			{
				return;
			}
			OnShow();
			_isShow = true;
			_skinGO.SetActive(true);
		}


	}
}
