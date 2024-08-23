using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;
public class KingController : MonoBehaviour
{
    struct Stat
    {
        public const int minDamage = 25;
        public const int maxDamage = 50;
    }
    enum PlayerState
    {
        STAND,
        FALLING,
        DEAD
    }
    public Action<PigController> KilledEnemy;
    PlayerState ps;
    public Rigidbody2D rigid;
    Animator animator;
    bool attack;
    float attackTimer;
    public PigController enemy;
    List<PigController> enemies;
    public float direction =1f;
    bool isCanKnockBack;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        enemies = new List<PigController>();
        KilledEnemy = (PigController controller) => { enemies.Remove(controller); };
    }

    // Update is called once per frame
    void Update()
    {
        isCanKnockBack = true;
        if (ps != PlayerState.DEAD)
        {

            if(Input.GetKeyDown(KeyCode.Return))
            {
                PigController go = GameObject.Instantiate(enemy);
                GameObject game = go.gameObject;
                game.transform.position = new Vector2(transform.position.x + 3f, transform.position.y);
                enemies.Add(go);
            }

            //공격 타이머
            if (attackTimer > 0f && attack == true)
            {

                attackTimer -= Time.deltaTime;
                if (attackTimer < 0f)
                {
                    attack = false;
                    animator.SetBool("attack", false);
                    Debug.Log("공격이 끝남");

                }
            }

            float horizontal = Input.GetAxisRaw("Horizontal");

            //무조건 -1,0,1
            //0이아니면 이동중
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

            //플레이어 공격
            if (attack == false)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    attackTimer = 0.375f;
                    attack = true;
                    animator.SetBool("attack", true);

                    for(int i =0; i<enemies.Count; i++)
                    {
                        if (Mathf.Abs(transform.position.x - enemies[i].transform.position.x + direction) < 1.5f
                            && Mathf.Abs(transform.position.y - enemies[i].transform.position.y) < 2f)
                        {
                            
                            enemies[i].Hit(Random.Range(Stat.minDamage, Stat.maxDamage + 1), direction);
                        }
                    }
                }
            }

            // 플레이어 점프
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


            //플레이어 사망
            if (Input.GetKey(KeyCode.Q))
            {
                animator.SetInteger("state", 3);
                ps = PlayerState.DEAD;
            }
        }
    }

    public void Knockback(float dir)
    {
        if (isCanKnockBack)
        {
            isCanKnockBack = false;
            rigid.AddForce(new Vector2(5f * dir, -1));
        }
    }
}
