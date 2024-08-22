using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingController : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator animator;
    float fallingSpeed;
    bool bPressedJump;
    bool attack;
    float attackTimer;
    float moveSpeed;
    public PigController enemy;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        

        if(attackTimer > 0f && attack ==true)
        {
            
            attackTimer -= Time.deltaTime;
            if(attackTimer < 0f)
            {
                attack = false;
                animator.SetBool("attack", false);
                Debug.Log("공격이 끝남");
              
            } 
        }

        float horizontal = Input.GetAxisRaw("Horizontal");

        moveSpeed = horizontal;
      //  Debug.Log(vertical + " " + horizontal);
       
        //무조건 -1,0,1
        //0이아니면 이동중
        if(horizontal == 0f)
        {
            animator.SetInteger("state", 0);
        }
        else
        {
            animator.SetInteger("state", 1);
            transform.localScale = new Vector3(horizontal, 1, 1);

            if (rigid.velocity.y == 0f)
            {
                Vector2 vel = new Vector2(horizontal * 1, 0);
                if (attack == false) rigid.velocity = vel;
            }
        }

        if (attack == false)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                attackTimer = 0.375f;
                attack = true;
                animator.SetBool("attack", true);
                if (Mathf.Abs(transform.position.x - enemy.transform.position.x + transform.localScale.x * 2) < 1f)
                {
                    Debug.Log("공격이 끝남");
                    enemy.Hit();

                }
            }
        }
         if(Input.GetKey(KeyCode.Q))
         {
             animator.SetInteger("state", 3);
         }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(rigid.velocity.y == 0f)
            {
                Vector2 vel = new Vector2(0,10f * 1);
                rigid.AddForce(vel * 20f);
              //  rigid.velocity = vel;
                bPressedJump = true;
             //   animator.SetBool("jump", true);

            }
        }


        if(rigid.velocity.y > 0f)
        {
            animator.SetInteger("jump", 1);
        }
        else if(rigid.velocity.y < 0f)
        {
            animator.SetInteger("jump", 2);
        }
        else
        {
            animator.SetInteger("jump", 0);
            
        }
        fallingSpeed = rigid.velocity.y;
    }
}
