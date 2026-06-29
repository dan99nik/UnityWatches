using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace App.Core
{
    public class SceneLoader
    {
        public async Task LoadSceneAsync(string key)
        {
            AsyncOperationHandle<SceneInstance> handle = Addressables.LoadSceneAsync(key, LoadSceneMode.Additive);

            await handle.Task;

            if (handle.Status == AsyncOperationStatus.Succeeded)
                Debug.Log("Scene loaded");
            else
                Debug.LogError("Failed to load scene");
        }
    }
}