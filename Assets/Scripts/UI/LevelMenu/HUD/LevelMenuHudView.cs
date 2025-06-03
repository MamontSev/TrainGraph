using Mamont.UI.MVVM;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

namespace Mamont.UI.LevelMenu.HUD
{
	public interface ILevelMenuHudView
	{
		void SetLevelText( string text );
		void SetScoreText( string text );
	}
	public class LevelMenuHudView:BaseHudView<LevelMenuHudViewModel>, ILevelMenuHudView
	{
		private LevelMenuHudViewModel _model;
		protected override void OnBind( LevelMenuHudViewModel model )
		{
			_model = model;
			Init();
		}


		private void Init()
		{
			_model.OnInitView(this);

			PauseButton.onClick.RemoveAllListeners();
			PauseButton.onClick.AddListener(_model.PressedPause);
		}

		private void OnDestroy()
		{
			PauseButton.onClick.RemoveAllListeners();
		}


		[Header("PauseButton")]
		[SerializeField]
		private Button PauseButton;



		[Header("LevelText")]
		[SerializeField]
		private TextMeshProUGUI LevelText;
		public void SetLevelText( string text )
		{
			LevelText.text = text;
		}

		[Header("ScoreText")]
		[SerializeField]
		private TextMeshProUGUI ScoreText;
		public void SetScoreText( string text )
		{
			ScoreText.text = text;
		}





	}

}
