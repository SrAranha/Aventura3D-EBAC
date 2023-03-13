using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGuns : MonoBehaviour
{
    public List<GunBase> gunsList;

    private Inputs inputs;
    [SerializeField] private GunBase _activeGun;

    private void OnEnable()
    {
        inputs?.Enable();
    }
    private void OnDisable()
    {
        inputs.Disable();
    }
    private void Awake()
    {
        inputs = new Inputs();
        inputs.Enable();
        foreach (GunBase gun in gunsList)
        {
            gun.gameObject.SetActive(false);
        }
        gunsList[0].gameObject.SetActive(true);
        _activeGun = gunsList[0];
    }
    private void Start()
    {
        inputs.Gameplay.SwitchGun01.performed += ctx => SwitchGun(0);
        inputs.Gameplay.SwitchGun02.performed += ctx => SwitchGun(1);
    }

    private void SwitchGun(int index)
    {
        Debug.Log("Switching Gun!");
        _activeGun.gameObject.SetActive(false);
        _activeGun = gunsList[index];
        _activeGun.gameObject.SetActive(true);
    }
}
