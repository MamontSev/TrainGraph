using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mamont.SceneControl
{
	public class SceneControlService:MonoBehaviour, ISceneControlService
	{
		public void LoadGamePlay()
		{
			LoadScene("GamePlay");
		}


		public void LoadMenu()
		{
			LoadScene("Menu");
		}

		private void LoadScene( string name )
		{
			AsyncOperation sceneAO;
			StartCoroutine(LoadingSceneLevelRealProgress());
			IEnumerator LoadingSceneLevelRealProgress()
			{
				sceneAO = SceneManager.LoadSceneAsync(name , LoadSceneMode.Single);
				sceneAO.allowSceneActivation = false;

				while( !sceneAO.isDone )
				{
					if( sceneAO.progress >= 0.9f )
					{
						sceneAO.allowSceneActivation = true;
					}
					yield return null;
				}
			}
		}
	}
}
