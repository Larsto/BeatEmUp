using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    Animator animator;
    public int HitPoints = 10;
    Transform target;
    public int Speed = 5;
    public int MinDistance = 2;
    public bool facingRight = true;
    bool isAlive = true;
    public int damage;
    float hitTime;
    public float setHitTime = 1;
    float oldPosition;
    float newPosition;

    void Start()
    {
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        hitTime = setHitTime;
        oldPosition = transform.position.x;
    }

    void Update()
    {
        if(!isAlive)
        {
            return;
        }
        Move();

        newPosition = oldPosition;

        if (transform.position.x > oldPosition && !facingRight)
            Flip();
        if (transform.position.x < oldPosition && facingRight)
            Flip();
        oldPosition = transform.position.x;
    }

    private void Move()
    {


        if (Vector2.Distance(transform.position, target.position) > MinDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, Speed * Time.deltaTime);
            animator.SetBool("Walk", true);
        }
        else
        {
            Hit();
            animator.SetBool("Walk", false);
        }

    }

    void Hit()
    {
        hitTime -= Time.deltaTime;
        if(hitTime < 0)
        animator.SetTrigger("Hit");
    }

    void Flip()
    {
        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void GotHit(int amount)
    {
        HitPoints -= amount;

        if (HitPoints > 0)
        {
            hitTime = setHitTime;
            animator.SetTrigger("GotHit");
            Debug.Log("Hitpoints " + HitPoints);
        }
        if (HitPoints <= 0)
        {
            animator.SetTrigger("Death");
            isAlive = false;
            Destroy(gameObject, 5);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerScript e = other.GetComponent<PlayerScript>();
        if (e != null)
        {
            e.GotHit(damage);
        }
    }
}
