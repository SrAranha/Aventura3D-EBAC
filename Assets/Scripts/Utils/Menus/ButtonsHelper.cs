using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsHelper : MonoBehaviour
{
    #region SCENES
    public void LoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }
    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
    #endregion
    #region SAVE LOAD
    public void SaveGame()
    {
        SaveManager.instance.SaveGame();
    }
    public void SaveAndExit()
    {
        SaveManager.instance.SaveGame();
        Exit();
    }
    public void LoadGame()
    {
        SaveManager.instance.LoadGame();
    }
    public void NewGame()
    {
        SaveManager.instance.NewGame();
    }
    #endregion
    public void Exit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
            Application.Quit();
    }
}
