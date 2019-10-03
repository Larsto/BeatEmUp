using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public int maxHealth = 10;
    int HitPoints;
    Rigidbody2D rigidbody2d;
    public float speed = 5.0f;
    public bool facingRight = true;
    bool isAlive = true;
    Animator animator;
    int damage;
    public int hit1;
    public int hit2;

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        HitPoints = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive) { 
        return; 
        }
        Walk();
        Hit();
    }

    void Walk()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 position = rigidbody2d.position;
        Vector2 move = new Vector2(horizontal, vertical);

        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;

        rigidbody2d.MovePosition(position);

        animator.SetFloat("Walk", move.magnitude);

        if (horizontal > 0 && !facingRight)
            Flip();
        else if (horizontal < 0 && facingRight)
            Flip();
    }

    void Flip()
    {
        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void Hit()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            animator.SetTrigger("Hit");
            damage = hit1;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            animator.SetTrigger("PowerHit");
            damage = hit2;
        }
    }

    public void GotHit(int amount)
    {
        HitPoints -= amount;

        if (HitPoints > 0)
        {
        //    animator.SetTrigger("GotHit");
            Debug.Log("Hitpoints " + HitPoints);
        }
        if (HitPoints <= 0)
        {
         //   animator.SetTrigger("Death");
            isAlive = false;
            Destroy(gameObject, 5);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        EnemyScript e = other.GetComponent<EnemyScript>();
        if (e != null)
        {
            e.GotHit(damage);
        }
    }


}
