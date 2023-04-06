using DG.Tweening;
using UnityEngine;

public class EnemyShooter : EnemyBase
{
    [Header("EnemyShooter")]
    public GunBase gunBase;
    public float alertRadius;

    private PlayerController _playerController;
    private bool _playerInRange;
    private void OnValidate()
    {
        gunBase = GetComponent<GunBase>();
    }
    protected override void Init()
    {
        base.Init();
        _playerController = FindObjectOfType<PlayerController>();
    }
    private void Update()
    {
        _playerInRange = Vector3.Distance(transform.position, _playerController.transform.position) < alertRadius;
        if (_playerInRange)
        {
            Debug.Log("Player is in range");
            ShootAt(_playerController.transform);
            _pathWalk?.StopWalking(false);
        }
        else
        {
            _pathWalk?.StartWalking();
            StopCoroutine(gunBase.StartShoot());
        }
    }
    /* As vezes o inimigo atira antes virar completamente para o alvo
     * fazendo que o tiro saia na posição anterior.
     */
    private void ShootAt(Transform target)
    {
        transform.DOLookAt(target.position, .2f);
        StartCoroutine(gunBase.StartShoot(target));
    }
}
