using Mamont.UI.General.PopUp;
using Mamont.UI.MVVM;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

namespace Mamont.UI.MainMenu.PopUp
{
	public interface IStartLevelView
	{
		void SetHeaderText( string s );
	}
	public class StartLevelView: PopUpBaseAnimator<StartLevelViewModel>, IStartLevelView, IHideOnSystemBack
	{
		private StartLevelViewModel _model;
		protected override void OnBind( StartLevelViewModel model )
		{
			_model = model;
		}

		protected override void OnShow()
		{
			_model.OnShowView(this);
			FonClozeButton.onClick.AddListener(Cloze);
			BackButton.onClick.AddListener(Cloze);
			StartButton.onClick.AddListener(_model.OnPressedStartGame);
		}
		protected override void OnHide()
		{
			FonClozeButton.onClick.RemoveAllListeners();
			BackButton.onClick.RemoveAllListeners();
			StartButton.onClick.RemoveAllListeners();
		}

		private void OnDestroy()
		{
			FonClozeButton.onClick.RemoveAllListeners();
			BackButton.onClick.RemoveAllListeners();
			StartButton.onClick.RemoveAllListeners();
		}

		private void Cloze()
		{
			Hide();
		}

		[Header("HeaderText")]
		[SerializeField]
		private TextMeshProUGUI HeaderText;
		public void SetHeaderText( string s )
		{
			HeaderText.text = s;
		}

		[Header("FonClozeButton")]
		[SerializeField]
		private Button FonClozeButton;

		[Header("BackButton")]
		[SerializeField]
		private Button BackButton;

		[Header("StartButton")]
		[SerializeField]
		private Button StartButton;
	}
}
