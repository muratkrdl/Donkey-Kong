using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D myRigid;
    [SerializeField] Animator animator;
    [SerializeField] BoxCollider2D myBoxCollider;

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float climbForce = 5f;

    bool isClimb => Input.GetKey(KeyCode.W);

    float defaultGravityScale;

    void Start() 
    {
        defaultGravityScale = myRigid.gravityScale;    
    }

    void Update() 
    {
        Move();
        Jump();
        Climb();
    }

    void Move()
    {
        float inputValue = Input.GetAxis("Horizontal");

        Vector2 newPos = Vector2.zero;
        newPos.x += inputValue * moveSpeed * Time.deltaTime;

        if(Mathf.Abs(inputValue) > Mathf.Epsilon) { animator.SetBool("run", true); }
        else { animator.SetBool("run", false); }

        bool isPlayerMove = Mathf.Abs(myRigid.velocity.x) >= Mathf.Epsilon;
        if(isPlayerMove) { transform.localScale = new Vector2 (Mathf.Sign(myRigid.velocity.x), 1f); }

        Vector2 playerVelocity = new Vector2 (inputValue * moveSpeed, myRigid.velocity.y);
        myRigid.velocity = playerVelocity;
    }

    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Vector2 newJump = myRigid.velocity;
            newJump.y += jumpForce * Time.fixedDeltaTime;

            myRigid.velocity = new Vector2(myRigid.velocity.x, newJump.y);
        }
    }

    void Climb()
    {
        if(myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Ladder"))) 
        {
            if(isClimb)
            {
                myRigid.gravityScale = 0;

                Vector2 newPos = transform.position;
                newPos.y += climbForce * Time.deltaTime;
                transform.position = newPos;

                animator.SetBool("climb", true);
            }
            else
            {
                myRigid.gravityScale = defaultGravityScale;

                animator.SetBool("climb", false);
            }
        }
        else
        {
            myRigid.gravityScale = defaultGravityScale;

            animator.SetBool("climb", false);
        }
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            //Game Over Phase
            GameManager.Instance.ShowGameOverPanel();
            Time.timeScale = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Princess"))
        {
            //Player Win Phase
            GameManager.Instance.ShowWinPanel();
            Time.timeScale = 0;
        }
    }

}
