using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Private Inspector Variables
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float jumpForce = 500.0f;
    [SerializeField] private Transform groundCheckPos;  // Ground Check OverlapCircle position
    [SerializeField] private float groundCheckRadius;   // Ground Check OverlapCircle radius
    [SerializeField] private LayerMask whatIsGround;    // Ground Layer Mask

    // Private Variables
    private Rigidbody2D rBody;
    private Animator anim;
    private bool isGrounded = false;
    private bool isFacingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Used for physics
    void FixedUpdate()
    {
        float horiz = Input.GetAxis("Horizontal");

        // Check if I am on the ground
        isGrounded = GroundCheck();

        // Jump code goes here
        if(isGrounded && Input.GetAxis("Jump") > 0)
        {
            // Jumping!
            rBody.AddForce(new Vector2(0.0f, jumpForce));
        }


        // Debug.Log("Horizontal: " + horiz);
        rBody.velocity = new Vector2(horiz * speed, rBody.velocity.y);

        // Check if sprite needs to be flipped
        if(isFacingRight && rBody.velocity.x < 0)
        {
            Flip();
        }
        else if(!isFacingRight && rBody.velocity.x > 0)
        {
            Flip();
        }

        // Send values to animator
        anim.SetFloat("xSpeed", Mathf.Abs(rBody.velocity.x));
        anim.SetFloat("ySpeed", Mathf.Abs(rBody.velocity.y));
        anim.SetBool("isGrounded", isGrounded);
    }

    private bool GroundCheck()
    {
        return Physics2D.OverlapCircle(groundCheckPos.position, groundCheckRadius, whatIsGround);
    }

    private void Flip()
    {
        Vector3 temp = transform.localScale;
        temp.x *= -1;
        transform.localScale = temp;

        isFacingRight = !isFacingRight;
    }
}
