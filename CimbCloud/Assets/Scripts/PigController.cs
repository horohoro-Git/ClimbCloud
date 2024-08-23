using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class PigController : MonoBehaviour
{

    public int hp = 100;
    Animator animator;
    float hitTimer;
    KingController king;
    float direction;
    float fadeTimer = 3f;
    Rigidbody2D rigid;
    bool bAttack;
    float attackTimer;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        king = GameObject.Find("King").GetComponent<KingController>();
        hp = 100;
        direction = -1f;
        fadeTimer = 3f;
    }

    // Update is called once per frame
    void Update()
    {

        if (hp > 0)
        {

           
           /* if (transform.position.x > king.transform.position.x)
            {
               direction = 1f;
            }
            else if(transform.position.x < king.transform.position.x)
            {
                
                direction = -1f;
            }

            if(Mathf.Abs(transform.position.x - king.transform.position.x) > 3f)
            {
                rigid.velocity = (new Vector2(-1 * direction, -5));
                animator.SetInteger("state", 3);
            }*/
          /*  else
            {
              
                if (bAttack == false)
                {
                    Debug.Log("AAa");
                    attackTimer = 0.5f;
                    bAttack = true;
                    animator.SetBool("attack", true);
                }
            }*/
          //  transform.localScale = new Vector3(direction, 1, 1);


            if (hitTimer > 0f)
            {
                hitTimer -= Time.deltaTime;
                if (hitTimer < 0f)
                {
                    animator.SetInteger("state", 0);    //적이 피해를 견딤
                }
            }
            //플레이어가 끼지 않도록 밀어줌
            if (Mathf.Abs(transform.position.x - king.transform.position.x) < 0.95f)
            {
                if (transform.position.x > king.transform.position.x)
                {
                    king.Knockback(-1);
                   
                }
                else if (transform.position.x < king.transform.position.x)
                {
                    king.Knockback(1);
                }
            }
        }
        else
        {
            fadeTimer -= Time.deltaTime;
            if (fadeTimer < 0f)
            {
                king.KilledEnemy(this);
                Destroy(this.gameObject);
            }
        }
    }
    public void Hit(int damage, float dir)
    {
        if (hp > 0)
        {
            rigid.AddForce(new Vector2(dir * 2f, 1.5f), ForceMode2D.Impulse);

            hp -= damage;
            if (hp <= 0)
            {
                hp = 0;
                animator.SetInteger("state", 2);    //적이 죽음
                GetComponent<CircleCollider2D>().excludeLayers = LayerMask.GetMask("Player");
            }
            else
            {

                hitTimer = 0.333f;
                animator.SetInteger("state", 1);    //적이 피해를 받음
            }
        }
    }
}
