using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D body;
    [SerializeField] public int speed;
    [SerializeField] public float jumpforce;
    private Animator anim;
    public bool grounded;

    private float lastDirection = 1;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        bool isSprinting = Input.GetKey(KeyCode.LeftShift);
        float currentSpeed = isSprinting ? speed*(float)1.5 : speed;

        // Apply movement
        if (horizontalInput != 0)
        {
            body.linearVelocity = new Vector2(horizontalInput * currentSpeed, body.linearVelocity.y);
        }
        else if (grounded)
        {
            // Stop on the ground when no input
            body.linearVelocity = new Vector2(0f, body.linearVelocity.y);
        }
        

        if (Input.GetKey(KeyCode.Space) && grounded)
        {
            jump();
        }

        // Only flip when direction actually changes
        if (horizontalInput != 0)
        {
            float direction = Mathf.Sign(horizontalInput);
            if (direction != lastDirection)
            {
                lastDirection = direction;
                Flip(direction);
            }
        }

        if (horizontalInput != 0 && !Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetBool("Walk", true);
            anim.SetBool("Run", false);
            body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocityY);
        }
        else if (horizontalInput != 0 && Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetBool("Walk", false);
            anim.SetBool("Run", true);
            body.linearVelocity = new Vector2(horizontalInput * (float)1.5 * speed, body.linearVelocityY);
        }
        else
        {
            anim.SetBool("Walk", false);
            anim.SetBool("Run", false);
        }

    }

    private void Flip(float direction)
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * direction;
        transform.localScale = scale;
    }

    private void jump()
    {
        body.linearVelocity = new Vector2(body.linearVelocity.x, jumpforce);
        grounded = false;
        anim.SetBool("Jump", true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
            anim.SetBool("Jump", false);
        }
    }
}