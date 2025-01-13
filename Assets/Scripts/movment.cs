using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    public LayerMask ground;
    public float jumpHeight;
    public float moveSpeed;
    public float forceMagnitude = 10f;
    Vector2 move;
    Rigidbody2D rb;
    public bool canJump = false;
  

   
    bool allowslow=true;
    bool allowspeed = true;
    
    // Start is called before the first frame update
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
     
        
}

 
    
    void FixedUpdate()
    {
        //canJump = Physics2D.OverlapCircle(transform.position,0.5f,ground);
        rb.AddForce(transform.forward * forceMagnitude, ForceMode2D.Force);
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
            {
            canJump = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            canJump = false;
        }
    }
    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.O))
        {
            if (allowslow==true)
            {
                rb.gravityScale = 50;
                jumpHeight = 60;
                moveSpeed = 70;
                allowslow = false;
            }
           else if (allowslow==false)
            {
                rb.gravityScale = 1;
                jumpHeight = 10;
                moveSpeed = 4;
                allowslow = true;
                    
            }

        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (allowslow == true)
            {
                rb.gravityScale = 0.1F;
                jumpHeight = 3.5f;
                moveSpeed = 1F;
                allowslow = false;
            }
            else if (allowslow == false)
            {
                rb.gravityScale = 1;
                jumpHeight = 10;
                moveSpeed = 4;
                allowslow = true;

            }

        }
        // Movement in the direction stated in code
        move = new Vector2(0, rb.velocity.y);
        if (Input.GetKey(KeyCode.A))
        {
            transform.localScale = new Vector2(1, 1);
            move += Vector2.left * moveSpeed;
            if (canJump)
            {

            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.localScale = new Vector2(-1, 1);
            move += Vector2.right * moveSpeed;
            if (canJump)
            {

            }
        }

        if (canJump && Input.GetKeyDown(KeyCode.Space))
        {
            move += Vector2.up * jumpHeight;

        }

        rb.velocity = move;

     
    }
}

