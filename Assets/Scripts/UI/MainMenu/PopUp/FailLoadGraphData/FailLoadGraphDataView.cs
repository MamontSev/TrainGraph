using Mamont.UI.General.PopUp;

using TMPro;

using UnityEngine;

namespace Mamont.UI.MainMenu.PopUp
{
	public interface IFailLoadGraphDataView
	{
		void SetMessageText( string s );
	}
	public class FailLoadGraphDataView:PopUpBaseAnimator<FailLoadGraphDataViewModel>, IFailLoadGraphDataView
	{
		private FailLoadGraphDataViewModel _model;
		protected override void OnBind( FailLoadGraphDataViewModel model )
		{
			_model = model;
		}

		protected override void OnShow()
		{
			_model.OnShowView(this);
		}
		protected override void OnHide()
		{
			
		}

		

		[Header("MessageText")]
		[SerializeField]
		private TextMeshProUGUI MessageText;
		public void SetMessageText( string s )
		{
			MessageText.text = s;
		}

	}
}





