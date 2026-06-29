using App.Times;
using UnityEngine;

namespace App.Core
{
    public sealed class AppManager
    {
        private SceneLoader _sceneLoader;

        private const string MainScene = "MainScene";
        private const string YandexUrl = "https://yandex.com/time/sync.json";

        public static AppManager Instance { get; private set; }

        public TimeManager TimeManager { get; private set; }

        private AppManager()
        {
            TimeManager = new TimeManager();
            _sceneLoader = new SceneLoader();
            Initialize();
        }

        private async void Initialize()
        {
            await TimeManager.GetRequest(YandexUrl);
            await _sceneLoader.LoadSceneAsync(MainScene);
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void AfterSceneLoadStatic()
        {
            Instance = new AppManager();
        }
    }
}