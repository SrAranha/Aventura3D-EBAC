using System.IO;
using UnityEngine;

public class SaveManager : Singleton<SaveManager>
{
    [SerializeField] private SaveSetup _setup;
    private string _saveGamePath;
    private string _saveGame;
    private string _loadGame;
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
        LoadGame();
    }
    private void SetGamePath()
    {
        _saveGamePath = Application.dataPath + "/save.json";
    }
    #region LOAD
    [NaughtyAttributes.Button]
    public void LoadGame()
    {
        SetGamePath();
        if (File.Exists(_saveGamePath))
        {
            _loadGame = File.ReadAllText(_saveGamePath);
            _setup = JsonUtility.FromJson<SaveSetup>(_loadGame);
        }
        else
        {
            Debug.Log("NO SAVE GAME FOUND");
        }
    }
    public int LoadLastCheckpoint()
    {
        return _setup.lastCheckpoint;
    }
    #endregion
    #region SAVE
    [NaughtyAttributes.Button]
    public void SaveGame()
    {
        _saveGame = JsonUtility.ToJson(_setup, true);

        File.WriteAllText(_saveGamePath, _saveGame);
    }
    public void SaveCheckpoint(int lastCheckpoint)
    {
        _setup.lastCheckpoint = lastCheckpoint;
    }
    #endregion
}
[System.Serializable]
public class SaveSetup
{
    public int playerHealth;
    public int lastLevel;
    public int lastCheckpoint = -1;
    public int coins;
    public int lifepacks;
}
