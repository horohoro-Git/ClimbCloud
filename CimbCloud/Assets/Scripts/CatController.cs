using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour
{
    public Rigidbody2D rigid;
    public float moveForce;
    public float jumpForce;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            animator.SetInteger("state", 1);
            transform.localScale = new Vector3(-1, 1, 1);
            rigid.AddForce(new Vector2(moveForce, 0) * -transform.right);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            animator.SetInteger("state", 1);
            transform.localScale = new Vector3(1, 1, 1);
            rigid.AddForce(new Vector2(moveForce, 0) * transform.right);

        }
        else
        {
            animator.SetInteger("state", 0);

        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            rigid.AddForce(new Vector2(0, jumpForce) * transform.up);
        }
    }

    private void FixedUpdate()
    {
        
    }
}
