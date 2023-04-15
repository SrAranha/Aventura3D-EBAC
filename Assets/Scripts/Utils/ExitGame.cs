using UnityEngine;

public class ExitGame : MonoBehaviour
{
    public void SaveAndExit()
    {
        SaveManager.instance.SaveGame();
        Exit();
    }
    public void Exit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
}
