using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public ProjectileBase prefabProjectile;
    public Transform shootPoint;
    public float shootCooldown;
    public KeyCode keyToShot = KeyCode.Tab;

    private PlayerController player;
    private Coroutine _currentCoroutine;
    private void Awake()
    {
        player = GetComponent<PlayerController>();
    }
    // Update is called once per frame
    void Update()
    {
        // TODO: Desse modo, ao segurar o botão, têm o cooldown, mas se ficar apertando o botão, ele ignora o cooldown entre os tiros.
        if (Input.GetKeyDown(keyToShot))
        {
            _currentCoroutine = StartCoroutine(CoroutineShoot());
        }    
        if (Input.GetKeyUp(keyToShot))
        {
            if (_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
            }
        }
    }

    IEnumerator CoroutineShoot()
    {
        while (true)
        {
            Shoot();   
            yield return new WaitForSeconds(shootCooldown);
        }
    }
    private void Shoot()
    {
        var projectile = Instantiate(prefabProjectile);
        projectile.transform.position = shootPoint.position;
        projectile.transform.rotation = shootPoint.rotation;
    }
}
