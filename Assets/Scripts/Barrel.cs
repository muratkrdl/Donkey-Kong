using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    [SerializeField] Rigidbody2D myRigid;
    [SerializeField] CircleCollider2D circleCollider;

    [SerializeField] float moveSpeed = 5f;

    float firstMoveSpeed;

    void Start() 
    {
        firstMoveSpeed = moveSpeed;
    }

    void Update() 
    {
        Vector2 newVelocity = Vector2.zero;
        newVelocity.x += moveSpeed * Time.deltaTime;

        myRigid.velocity = newVelocity;
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.CompareTag("Obstacle"))
        {
            moveSpeed *= -1;
        }    
        else if(other.gameObject.CompareTag("BottomPlat"))
        {
            if(moveSpeed > 0)
            {
                moveSpeed = firstMoveSpeed * -1;
            }
        }
        else if(other.gameObject.CompareTag("Die"))
        {
            Destroy(gameObject, 2f);
            circleCollider.isTrigger = true;
        }
    }

}
