using Mamont.GeneralStateMashine;

using UnityEngine;

using Zenject;

namespace Mamont.EntryPoint
{
	public class BootstrapEntryPoint:MonoBehaviour
	{
		private void Start()
		{
			Init();
		}

		private GeneralGameStateMachine _generalGameStateMachine;
		private GeneralStateFactory _stateFactry;
		[Inject]
		private void Construct( 
			GeneralGameStateMachine _generalGameStateMachine,
			GeneralStateFactory _stateFactry
		)
		{
			this._generalGameStateMachine = _generalGameStateMachine;
			this._stateFactry = _stateFactry;
		}

		private void Init()													   
		{
			_generalGameStateMachine.Register<MenuState>(_stateFactry.Create<MenuState>());
			_generalGameStateMachine.Register<GamePlayState>(_stateFactry.Create<GamePlayState>());

			_generalGameStateMachine.Enter<MenuState>();

		}
	}
}



