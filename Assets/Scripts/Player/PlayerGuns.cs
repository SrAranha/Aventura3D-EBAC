using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGuns : MonoBehaviour
{
    public List<GunBase> gunsList;

    private Inputs inputs;
    [SerializeField] private GunBase _activeGun;
    [SerializeField] private int _activeGunIndex;

    private void OnEnable()
    {
        inputs?.Enable();
    }
    private void OnDisable()
    {
        inputs?.Disable();
    }
    private void Awake()
    {
        inputs = new Inputs();
        inputs.Enable();

        foreach (GunBase gun in gunsList)
        {
            gun.gameObject.SetActive(false);
        }
        ResetActiveGun();
        _activeGun.gameObject.SetActive(true);
    }
    private void Start()
    {
        inputs.Gameplay.SwitchGuns.performed += ctx => SwitchGun();
    }

    private void SwitchGun()
    {
        _activeGun.gameObject.SetActive(false);
        _activeGunIndex++;
        if (_activeGunIndex == gunsList.Count)
        {
            ResetActiveGun();
        }
        _activeGun = gunsList[_activeGunIndex];
        _activeGun.gameObject.SetActive(true);
        Debug.Log("Swithing to " + _activeGun);
    }
    private void ResetActiveGun()
    {
        _activeGunIndex = 0;
        _activeGun = gunsList[_activeGunIndex];
    }
}
