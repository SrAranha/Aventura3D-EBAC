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
    private CharacterController controller;
    private Animator animator;
    private float verticalSpeed;

    private void OnValidate()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        MovePlayer();
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
        if (controller.isGrounded)
        {
            if (Input.GetKey(jumpKey))
            {
                verticalSpeed = jumpForce;
            }
        }
        verticalSpeed += gravity * Time.deltaTime;
        moveVector.y = verticalSpeed;

        // Handle Sprint
        if (isMoving)
        {
            if (Input.GetKey(sprintKey))
            {
                moveVector *= sprintFactor;
                animator.speed = sprintFactor;
            }
            else animator.speed = 1f;
        }

        // Move and Animate
        controller.Move(Time.deltaTime * moveVector);
        animator.SetBool("Run", isMoving);
    }
}
