using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class LoadSpiritScene : MonoBehaviour
{
    public GameObject loadingGUI;
    // Start is called before the first frame update
    void Start()
    {
        loadingGUI = GameObject.FindWithTag("loadingGUI");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Load()
    {
        //SceneManager.LoadScene(sceneBuildIndex: 1);
        AsyncOperationHandle<SceneInstance> handle = Addressables.LoadSceneAsync("spirit");
        handle.Completed += OnSceneLoaded;
        if (loadingGUI)
        {
            var tmp = loadingGUI.GetComponent<TextMeshProUGUI>();
            tmp.enabled = true;
        }
    }
    
    private void OnSceneLoaded(AsyncOperationHandle<SceneInstance> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.Log("Scene loaded.");
        }
        else
        {
            Debug.Log("Scene not loaded: "+handle.Status);
        }
    }
}
