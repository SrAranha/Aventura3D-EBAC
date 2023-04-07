using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float rotateSpeed;
    public float jumpForce;
    public float sprintFactor;
    [Header("Checkpoints")]
    public bool startAtLastCheckpoint;
    public bool hasRevive;
    public float timeToRespawn;
    [Header("Keycodes")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;

    private float gravity = -9.8f;
    private CharacterController _controller;
    private Animator _animator;
    private float _verticalSpeed;
    private HealthBase _healthBase;

    private void OnValidate()
    {
        _healthBase = GetComponent<HealthBase>();
        _controller = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();
    }
    private void Awake()
    {
        _healthBase.OnDeath += OnDeath;
    }
    private void Start()
    {
        SpawnAtLastCheckpoint();
    }
    // Update is called once per frame
    void Update()
    {
        if (_healthBase.IsAlive())
        {
            MovePlayer();
        }
    }
    private void SpawnAtLastCheckpoint()
    {
        if (PlayerPrefs.GetInt(CheckpointManager.instance.checkpointKey) > -1)
        {
            var __checkPos = CheckpointManager.instance.LastCheckpointPosition();
            __checkPos.z += 3f;
            transform.position = __checkPos;
        }
    }
    private void OnDeath(HealthBase health)
    {
        if (!_healthBase.IsAlive())
        {
            _animator.SetTrigger("Death");
            if (hasRevive)
            {
                StartCoroutine(Respawn());
            }
        }
    }
    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(timeToRespawn);
        SpawnAtLastCheckpoint();
        _healthBase.Revive();
    }
    private void MovePlayer()
    {
        transform.Rotate(0, Input.GetAxis("Horizontal") * Time.deltaTime * rotateSpeed, 0);

        float __inputAxisVertical = Input.GetAxis("Vertical");
        Vector3 __moveVector = __inputAxisVertical * moveSpeed * transform.forward;
        bool __isMoving = __inputAxisVertical != 0;

        /* BUG:
         * Quando segura ambos os valos/botões do eixo vertical (W & S || as setas ):
         * o inputAxisVertical fica entre 0,1 e -0,1, deixando o isMoving true,
         * assim a animação toca, mas o personagem nem sempre se mexe, ou sai muito lentamente do lugar
         * dando a senação de patinar.
         */

        // Handle Jump
        if (_controller.isGrounded)
        {
            if (Input.GetKey(jumpKey))
            {
                _verticalSpeed = jumpForce;
            }
        }
        _verticalSpeed += gravity * Time.deltaTime;
        __moveVector.y = _verticalSpeed;

        // Handle Sprint
        if (__isMoving)
        {
            if (Input.GetKey(sprintKey))
            {
                __moveVector *= sprintFactor;
                _animator.speed = sprintFactor;
            }
            else _animator.speed = 1f;
        }

        // Move and Animate
        _controller.Move(Time.deltaTime * __moveVector);
        _animator.SetBool("Run", __isMoving);
    }
}
