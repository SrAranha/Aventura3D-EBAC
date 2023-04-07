using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : Singleton<CheckpointManager>
{
    public List<CheckpointBase> checkpoints;
    public readonly string checkpointKey = "checkpoint_ID";

    private int _lastCheckpoint;
    // Start is called before the first frame update
    void Start()
    {
        _lastCheckpoint = PlayerPrefs.GetInt(checkpointKey);
        Debug.Log("Last Checkpoint => " + _lastCheckpoint);
        if (_lastCheckpoint > -1)
        {
            for (int i = 0; i < _lastCheckpoint; i++)
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
        PlayerPrefs.SetInt(checkpointKey, _lastCheckpoint);
        Debug.Log("Reseting Checkpoints");
    }
}
