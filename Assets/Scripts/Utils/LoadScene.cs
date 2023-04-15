using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void Load(int scene)
    {
        SceneManager.LoadScene(scene);
    }
    public void Load(string scene)
    {
        SceneManager.LoadScene(scene);
    }
    public void LoadAndPause(int scene)
    {
        SceneManager.LoadScene(scene);
        UnityEditor.EditorApplication.isPaused = true;
    }
}
