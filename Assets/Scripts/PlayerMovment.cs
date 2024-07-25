using UnityEngine;
using UnityEngine.AI;

public class PlayerMovment : MonoBehaviour
{
    public float rotationSpeed;
    public Animator animator;

    public static int Health = 100;
    public static bool Dead;

    public Transform orientation;

    float HorizontalInput;
    float VerticalInput;

    Vector3 moveDirection;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0)
        {
            Dead = true;
            if (!UIController.instance.inLoseMenue)
            {
                UIController.instance.ShowLoseMenu();
            }
        }
        GetInput();
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }
    private void MovePlayer()
    {
        //Calculate MoveDirection
        moveDirection = orientation.forward * VerticalInput + orientation.right * HorizontalInput;

        animator.SetFloat("Vertical", VerticalInput);
        animator.SetFloat("Horizontal", HorizontalInput);

        if (Input.GetKey(KeyCode.LeftShift) && (VerticalInput != 0 || HorizontalInput !=0))
        {
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }

        if (moveDirection != Vector3.zero && VerticalInput >= 0)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            rb.rotation = Quaternion.RotateTowards(rb.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        else if (moveDirection != Vector3.zero && VerticalInput < 0)
        {
            Quaternion toRotation = Quaternion.LookRotation(-moveDirection, Vector3.up);
            rb.rotation = Quaternion.RotateTowards(rb.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void GetInput()
    {
        HorizontalInput = Input.GetAxisRaw("Horizontal");
        VerticalInput = Input.GetAxisRaw("Vertical");
    }
}
