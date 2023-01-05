using System;
using System.Collections;
using Coroutine_runner;
using LoadingScreen;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scene_service
{
    public class SceneLoader: ISceneLoader
    {
        private ICoroutineRunner _coroutineRunner;
        private ILoadingScreen _loadingScreen;
        
        public SceneLoader (
            ICoroutineRunner coroutineRunner,
            ILoadingScreen loadingScreen
            )
        {
            _coroutineRunner = coroutineRunner;
            _loadingScreen = loadingScreen;
        }

        public void LoadSceneAsync( string name, Action onLoaded = null) => 
            _coroutineRunner.StartCoroutine(LoadingScene(name, onLoaded));

        private IEnumerator LoadingScene(string name, Action onLoaded = null)
        {
            if (SceneManager.GetActiveScene().name.Equals(name))
            {
                onLoaded?.Invoke();
                yield break;
            }
            
            _loadingScreen.Show();

            yield return new WaitForSeconds(1f);
            
            AsyncOperation loading = SceneManager.LoadSceneAsync(name);

            while (loading.isDone is false)
            {
                _loadingScreen.UpdateLoadingProgress(loading.progress);
                yield return null;
            }

            _loadingScreen.UpdateLoadingProgress(1f);
            yield return new WaitForSeconds(1f);
            
            onLoaded?.Invoke();
        }
    }
}