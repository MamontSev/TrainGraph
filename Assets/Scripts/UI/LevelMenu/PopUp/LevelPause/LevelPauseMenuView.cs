using Mamont.UI.General.PopUp;
using Mamont.UI.MVVM;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

namespace Mamont.UI.LevelMenu.PopUp
{
	public interface ILevelPauseMenuView
	{
		void SetHeaderText( string s );
	}
	public class LevelPauseMenuView:PopUpBaseAnimator<LevelPauseMenuViewModel> , ILevelPauseMenuView, IHideOnSystemBack
	{
		private LevelPauseMenuViewModel _model;
		protected override void OnBind( LevelPauseMenuViewModel model )
		{
			_model = model;
		}

		protected override void OnHide()
		{
			FonClozeButton.onClick.RemoveAllListeners();
			ContinueButton.onClick.RemoveAllListeners();
			ToMenuButton.onClick.RemoveAllListeners();
		}

		protected override void OnShow()
		{
			FonClozeButton.onClick.AddListener(PressedContinueGame);
			ContinueButton.onClick.AddListener(PressedContinueGame);
			ToMenuButton.onClick.AddListener(PressedToMainMenu);
			_model.OnShowView(this);
		}

		private void PressedContinueGame()
		{
			Hide();
		}
		private void PressedToMainMenu()
		{
			Hide();
			_model.OnBackToMenu();
		}

		[Header("Header")]
		[SerializeField]
		private TextMeshProUGUI HeaderText;

		public void SetHeaderText( string s )
		{
			HeaderText.text = s;
		}


		[Header("ContinueButton")]
		[SerializeField]
		private Button ContinueButton;


		[Header("ToMenuButton")]
		[SerializeField]
		private Button ToMenuButton;


		[Header("FonClozeButton")]
		[SerializeField]
		private Button FonClozeButton;
	}
}
