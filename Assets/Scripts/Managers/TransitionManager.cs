using Assets.Scripts.Components;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Managers
{
    internal sealed class TransitionManager : MonoBehaviour
    {
        [SerializeReference]
        private ProgressBar _progress;

        private static string _lastSceneName;
        private AsyncOperation _loadOperation;

        internal static void GoToScene(string sceneName)
        {
            _lastSceneName = sceneName;
            SceneManager.LoadScene("Transition");
        }

        internal static void ReloadScene()
        {
            SceneManager.LoadScene("Transition");
        }

        private void Start()
        {
            _progress.SetPercent(0f);
            _loadOperation = SceneManager.LoadSceneAsync(_lastSceneName);
        }

        private void Update()
        {
            _progress.SetPercent(_loadOperation.progress);
        }
    }
}
