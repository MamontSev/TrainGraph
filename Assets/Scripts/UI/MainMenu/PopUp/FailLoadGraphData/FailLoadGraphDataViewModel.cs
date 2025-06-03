using Mamont.UI.MVVM;

namespace Mamont.UI.MainMenu.PopUp
{
	public class FailLoadGraphDataViewModel:IPopUpViewModel
	{
		private readonly string _message;
		public FailLoadGraphDataViewModel(string _message)
		{
			this._message = _message;
		}
		private IFailLoadGraphDataView _myView;

		public void OnShowView( IFailLoadGraphDataView _myView )
		{
			this._myView = _myView;
			this._myView.SetMessageText($"Fail to load Graph Data \n {_message}" );
		}
	}
}





