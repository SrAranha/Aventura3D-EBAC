using System.Collections;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    public Transform shootPoint;
    public ProjectileBase projectile;
    public float timeBetweenShoots;

    private Inputs inputs;
    private bool _canShoot = true;
    private void Start()
    {
        inputs = new Inputs();
        inputs.Enable();

        inputs.Gameplay.Shoot.performed += ctx => StartCoroutine(StartShoot());
        inputs.Gameplay.Shoot.canceled += ctx => StopCoroutine(StartShoot());
    }
    private void OnEnable()
    {
        inputs?.Enable();
    }
    private void OnDisable()
    {
        inputs?.Disable();
    }
    private void Shoot()
    {
        var shoot = Instantiate(projectile);
        shoot.transform.SetPositionAndRotation(shootPoint.position, shootPoint.rotation);
    }
    public IEnumerator StartShoot(Transform target = null)
    {
        if (_canShoot)
        {
            if (target)
            {
                Vector3 normalizedTarget = target.transform.position;
                normalizedTarget.y += 1f;
                shootPoint.transform.LookAt(normalizedTarget);
            }
            Shoot();
            _canShoot = false;
            yield return new WaitForSeconds(timeBetweenShoots);
            _canShoot = true;
        }
    }
}
