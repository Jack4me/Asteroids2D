using System;
using System.Collections;
using CodeBase.Infrastructure;
using UnityEngine.SceneManagement;

namespace Core
{
    public class SceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public SceneLoader(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public void Load(string name, Action onLoaded = null)
        {
            _coroutineRunner.StartCoroutine(LoadScene(name, onLoaded));
        }

        private IEnumerator LoadScene(string nextScene, Action onLoaded = null)
        {
            if (SceneManager.GetActiveScene().name == nextScene)
            {
                onLoaded?.Invoke();
                yield break;
            }

            var waitAsync = SceneManager.LoadSceneAsync(nextScene);
            while (!waitAsync.isDone) yield return null;
            onLoaded?.Invoke();
        }
    }
}