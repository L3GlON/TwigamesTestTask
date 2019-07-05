using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    /// <summary>
    /// Reloads current scene to restart game
    /// </summary>
    public void ReloadScene()
    {
        //Load Scene asynchronously and replace current one
        SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
    }
}
