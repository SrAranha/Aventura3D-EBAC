using System.IO;
using UnityEngine;

public class SaveManager : Singleton<SaveManager>
{
    [SerializeField] private SaveSetup _curSave;
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
            _curSave = JsonUtility.FromJson<SaveSetup>(_loadGame);
        }
        else
        {
            Debug.Log("NO SAVE GAME FOUND");
        }
    }
    public int LoadLastCheckpoint()
    {
        return _curSave.lastCheckpoint;
    }
    public void LoadCollectables()
    {
        foreach (var item in InventoryManager.instance.itemSetups)
        {
            switch (item.itemType)
            {
                case ItemType.COIN:
                    item.inventory.quantity = _curSave.coins;
                    break;
                case ItemType.LIFE_PACK:
                    item.inventory.quantity = _curSave.lifepacks;
                    break;
                default:
                    break;
            }
        }
    }
    public int LoadPlayerHealth()
    {
        return _curSave.playerHealth;
    }

    #endregion

    #region SAVE
    [NaughtyAttributes.Button]
    public void SaveGame()
    {   
        SetGamePath();
        _saveGame = JsonUtility.ToJson(_curSave, true);
        File.WriteAllText(_saveGamePath, _saveGame);
    }
    public void SaveCheckpoint(int lastCheckpoint)
    {
        _curSave.lastCheckpoint = lastCheckpoint;
    }
    public void SaveCollectables()
    {
        foreach (var item in InventoryManager.instance.itemSetups)
        {
            var amount = item.inventory.quantity;
            switch (item.itemType)
            {
                case ItemType.COIN:
                    _curSave.coins = amount;
                    break;
                case ItemType.LIFE_PACK:
                    _curSave.lifepacks = amount;
                    break;
                default:
                    break;
            }
        }
    }
    public void SavePlayerHealth(int currentLife)
    {
        _curSave.playerHealth = currentLife;
        Debug.Log(_curSave.playerHealth);
    }
    #endregion
    [NaughtyAttributes.Button]
    private void ResetSaveGame()
    {
        _curSave.playerHealth = 0;
        _curSave.lastLevel = 0;
        _curSave.lastCheckpoint = -1;
        _curSave.coins = 0;
        _curSave.lifepacks = 0;
        SaveGame();
    }
}
[System.Serializable]
public class SaveSetup
{
    public int playerHealth;
    public int lastLevel;
    public int lastCheckpoint;
    public int lifepacks;
    public int coins;
}
