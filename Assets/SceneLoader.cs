using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set; }

    private void Awake()
   {
       DontDestroyOnLoad(this);

       if (Instance == null)
       {
           Instance = this;
       }

       LoadScene();
   }

    private void LoadScene()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void ReloadGame()
    {
        StartCoroutine(ReloadGameScene());
    }
    
    public IEnumerator ReloadGameScene()
    {
        yield return SceneManager.UnloadSceneAsync(1);
        
        yield return SceneManager.LoadSceneAsync(1);
    }
}
