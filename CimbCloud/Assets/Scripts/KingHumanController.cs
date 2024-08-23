using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KingHumanController : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb2D;
    public float moveSpeed = 1f;
    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {/*
        Debug.Log(collision);
        Debug.Log(collision.gameObject);
*/

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other);
        Debug.Log(other.gameObject);


        SceneManager.LoadScene("GameOverScene"); 
    }

    void Update()
    {
        //-1, 0, 1
        float h = Input.GetAxisRaw("Horizontal");
        if (h == 0)
        {
            animator.SetInteger("state", 0);   //Idle
        }
        else
        {
            //-1, 1
            animator.SetInteger("state", 1);    //Run
            this.transform.localScale = new Vector3(h, 1, 1);

            this.rb2D.velocity = new Vector2(h * moveSpeed, 0);
        }
    }
}
