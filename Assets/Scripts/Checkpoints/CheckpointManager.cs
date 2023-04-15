using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : Singleton<CheckpointManager>
{
    public List<CheckpointBase> checkpoints;
    public readonly string checkpointKey = "checkpoint_ID";

    [SerializeField] private int _lastCheckpoint;
    private SaveManager _saveManager;
    // Start is called before the first frame update
    void Start()
    {
        _saveManager = SaveManager.instance;
        _lastCheckpoint = _saveManager.LoadLastCheckpoint();
        if (_lastCheckpoint > -1)
        {
            for (int i = 0; i <= _lastCheckpoint; i++)
            {
                checkpoints[i].CheckpointOn();
            }
        }
    }
    public Vector3 LastCheckpointPosition()
    {
        return checkpoints[_lastCheckpoint].transform.position;
    }
    [NaughtyAttributes.Button]
    private void ResetCheckpoints()
    {
        _lastCheckpoint = -1;
        foreach (var checkpoint in checkpoints)
        {
            checkpoint.CheckpointOff();
        }
        _saveManager.SaveCheckpoint(_lastCheckpoint);
    }
}
