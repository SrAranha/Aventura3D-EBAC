using System.Collections;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    public Transform shootPoint;
    public Projectile_Base projectile;
    public float timeBetweenShoots;

    private Inputs inputs;
    private void Start()
    {
        inputs = new Inputs();
        inputs.Enable();

        inputs.Gameplay.Shoot.performed += ctx => StartCoroutine(Fire());
        inputs.Gameplay.Shoot.canceled += ctx => StopCoroutine(Fire());
    }
    private void OnEnable()
    {
        inputs?.Enable();
    }
    private void OnDisable()
    {
        inputs.Disable();
    }
    private void Shoot()
    {
        var shoot = Instantiate(projectile);
        shoot.transform.SetPositionAndRotation(shootPoint.position, shootPoint.rotation);
    }
    IEnumerator Fire()
    {
        Shoot();
        yield return new WaitForSeconds(timeBetweenShoots);
    }
}
