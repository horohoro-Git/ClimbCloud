using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigController : MonoBehaviour
{

    public int hp = 100;
    Animator animator;
    float hitTimer;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(hp>0 && hitTimer > 0f)
        {
            hitTimer -= Time.deltaTime;
            if(hitTimer < 0f)
            {
                animator.SetInteger("state", 0);    //적이 피해를 견딤
            }
        }
    }

    public void Hit()
    {
        hp -= 50;
        if(hp <=0)
        {
            hp = 0;
            animator.SetInteger("state", 2);    //적이 죽음
        }
        else
        {
            hitTimer = 0.1f;
            animator.SetInteger("state", 1);    //적이 피해를 받음
        }
    }
}
