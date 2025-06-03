namespace Mamont.Core.GameLoop
{
	public interface IGameLoopControl
	{
		bool IsPlaying
		{
			get;
		}

		void Pause();
		void Register( IGameLoopUpdate item );
		void Start();
		void UnPause();
		void Unregister( IGameLoopUpdate item );
	}
}
