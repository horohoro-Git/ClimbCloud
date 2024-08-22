using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingController : MonoBehaviour
{
    enum PlayerState
    {
        STAND,
        FALLING,
        DEAD
    }

    PlayerState ps;
    Rigidbody2D rigid;
    Animator animator;
    bool attack;
    float attackTimer;
    public PigController enemy;
    float direction =1f;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ps != PlayerState.DEAD)
        {

            //���� Ÿ�̸�
            if (attackTimer > 0f && attack == true)
            {

                attackTimer -= Time.deltaTime;
                if (attackTimer < 0f)
                {
                    attack = false;
                    animator.SetBool("attack", false);
                    Debug.Log("������ ����");

                }
            }

            float horizontal = Input.GetAxisRaw("Horizontal");

            //������ -1,0,1
            //0�̾ƴϸ� �̵���
            if (horizontal == 0f)
            {
                animator.SetInteger("state", 0);
            }
            else
            {
                animator.SetInteger("state", 1);
                transform.localScale = new Vector3(horizontal, 1, 1);
                direction = horizontal;
                if (ps != PlayerState.DEAD && rigid.velocity.y == 0f)
                {
                    Vector2 vel = new Vector2(horizontal * 1, 0);
                    if (attack == false) rigid.velocity = vel;
                }
            }

            //�÷��̾� ����
            if (attack == false)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    attackTimer = 0.375f;
                    attack = true;
                    animator.SetBool("attack", true);
                    if (Mathf.Abs(transform.position.x - enemy.transform.position.x + direction) < 1.5f)
                    {
                        enemy.Hit();

                    }
                }
            }

            // �÷��̾� ����
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (ps != PlayerState.DEAD && rigid.velocity.y == 0)
                {
                    Vector2 vel = new Vector2(0, 10f * 1);
                    rigid.AddForce(vel * 20f);
                }
            }
            if (rigid.velocity.y > 0f)
            {
                animator.SetInteger("jump", 1);
                ps = PlayerState.FALLING;
            }
            else if (rigid.velocity.y < 0f)
            {
                animator.SetInteger("jump", 2);
                ps = PlayerState.FALLING;
            }
            else
            {
                animator.SetInteger("jump", 0);
                ps = PlayerState.STAND;

            }


            //�÷��̾� ���
            if (Input.GetKey(KeyCode.Q))
            {
                animator.SetInteger("state", 3);
                ps = PlayerState.DEAD;
            }
        }
    }
}
