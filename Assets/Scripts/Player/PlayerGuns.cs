using System.Collections.Generic;
using UnityEngine;

public class PlayerGuns : MonoBehaviour
{
    public List<GunBase> gunsList;

    private Inputs _inputs;
    [SerializeField] private GunBase _activeGun;
    [SerializeField] private int _activeGunIndex;

    private void OnEnable()
    {
        _inputs?.Enable();
    }
    private void OnDisable()
    {
        _inputs?.Disable();
    }
    private void Awake()
    {
        _inputs = new Inputs();
        _inputs.Enable();

        foreach (GunBase gun in gunsList)
        {
            gun.gameObject.SetActive(false);
        }
        ResetActiveGun();
        _activeGun.gameObject.SetActive(true);
    }
    private void Start()
    {
        _inputs.Gameplay.SwitchGuns.performed += ctx => SwitchGun();
    }

    private void SwitchGun()
    {
        _activeGun.ResetGun();
        _activeGun.gameObject.SetActive(false);
        _activeGunIndex++;
        if (_activeGunIndex == gunsList.Count)
        {
            ResetActiveGun();
        }
        _activeGun = gunsList[_activeGunIndex];
        _activeGun.gameObject.SetActive(true);
    }
    private void ResetActiveGun()
    {
        _activeGunIndex = 0;
        _activeGun = gunsList[_activeGunIndex];
    }
}
