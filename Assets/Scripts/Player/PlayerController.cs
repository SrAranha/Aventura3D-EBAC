using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float rotateSpeed;
    public float jumpForce;
    public float sprintFactor;
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
    // Update is called once per frame
    void Update()
    {
        if (_healthBase.IsAlive())
        {
            MovePlayer();
        }
    }
    private void MovePlayer()
    {
        transform.Rotate(0, Input.GetAxis("Horizontal") * Time.deltaTime * rotateSpeed, 0);

        float inputAxisVertical = Input.GetAxis("Vertical");
        Vector3 moveVector = inputAxisVertical * moveSpeed * transform.forward;
        bool isMoving = inputAxisVertical != 0;

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
        moveVector.y = _verticalSpeed;

        // Handle Sprint
        if (isMoving)
        {
            if (Input.GetKey(sprintKey))
            {
                moveVector *= sprintFactor;
                _animator.speed = sprintFactor;
            }
            else _animator.speed = 1f;
        }

        // Move and Animate
        _controller.Move(Time.deltaTime * moveVector);
        _animator.SetBool("Run", isMoving);
    }
    private void OnDeath(HealthBase health)
    {
        if (!_healthBase.IsAlive())
        {
            _animator.SetTrigger("Death");
        }
    }
}
