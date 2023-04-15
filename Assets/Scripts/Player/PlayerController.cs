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
    [Header("Screen Shake")]
    public float shakeAmplitude;
    public float shakeFrequency;
    public float shakeTime;

    private readonly float gravity = -9.8f;
    private CharacterController _controller;
    private Animator _animator;
    private float _verticalSpeed;
    private HealthBase _healthBase;
    private Inputs _inputs;

    private void OnValidate()
    {
        _healthBase = GetComponent<HealthBase>();
        _controller = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();
    }
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
        _healthBase.OnDamage += OnDamage;
        _healthBase.OnDeath += OnDeath;
    }
    private void Start()
    {
        _inputs.Gameplay.Heal.performed += ctx => _healthBase.Heal();
        if (startAtLastCheckpoint)
        {
            SpawnAtLastCheckpoint();
        }
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
        if (SaveManager.instance.LoadLastCheckpoint() > -1)
        {
            var __checkPos = CheckpointManager.instance.LastCheckpointPosition();
            __checkPos.z += 3f;
            transform.position = __checkPos;
        }
    }
    private void OnDamage(HealthBase health)
    {
        if (ScreenShake.instance)
        {
            ScreenShake.instance.ShakeScreen(shakeAmplitude, shakeFrequency, shakeTime);
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
        _animator.SetTrigger("Respawn");
        SpawnAtLastCheckpoint();
        _healthBase.Revive();
    }
    #region SkinsEffects
    public void AddHealth(int healthToAdd, float duration)
    {
        StartCoroutine(AddHealthCoroutine(healthToAdd, duration));
    }
    IEnumerator AddHealthCoroutine(int healthToAdd, float duration)
    {
        _healthBase.startingHealth += healthToAdd;
        _healthBase.ResetHealth();
        yield return new WaitForSeconds(duration);
        _healthBase.startingHealth -= healthToAdd;
        _healthBase.ResetHealth();
    }
    public void ChangeSpeed(float speedFactor, float duration)
    {
        StartCoroutine(ChangeSpeedCoroutine(speedFactor, duration));
    }
    IEnumerator ChangeSpeedCoroutine(float speedFactor, float duration)
    {
        moveSpeed *= speedFactor;
        rotateSpeed *= speedFactor;
        yield return new WaitForSeconds(duration);
        moveSpeed /= speedFactor;
        rotateSpeed /= speedFactor;
    }
    #endregion
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
