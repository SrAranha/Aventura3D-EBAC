using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyPathWalk : MonoBehaviour
{
    public bool canWalk = true;
    public float speed;
    public float minDistance;
    public float timeTurnAround;
    public List<Transform> walkpoints;

    private int _index;
    private EnemyBase _enemyBase;
    private Tween _walkTween;

    private void OnValidate()
    {
        _enemyBase = GetComponent<EnemyBase>();
    }
    void Start()
    {
        OnValidate();
        StartWalking();
        ResetIndex();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    private void Move()
    {
        if (canWalk)
        {
            if(Vector3.Distance(transform.position, walkpoints[_index].position) < minDistance)
            {
                _index++;
                if (_index == walkpoints.Count) ResetIndex();
            }
            transform.position = Vector3.MoveTowards(transform.position, walkpoints[_index].position, Time.deltaTime * speed);
            _walkTween = transform.DOLookAt(walkpoints[_index].position, timeTurnAround);
        }
    }
    public void StopWalking(bool isIdle)
    {
        _walkTween.Kill();
        canWalk = false;
        if (isIdle) _enemyBase.PlayAnimationByTrigger("Idle");
    }
    public void StartWalking()
    {
        canWalk = true;
        _enemyBase.PlayAnimationByTrigger("Run");
    }
    private void ResetIndex()
    {
        _index = 0;
    }
}
