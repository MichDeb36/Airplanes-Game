using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleaner : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    private float speed = 30f;
    Vector2 position;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        position = new Vector2(transform.position.x, transform.position.y);
    }

    public void clearSky()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        Enemy enemy = target.GetComponent<Enemy>();

        if (target.tag == "Enemy")
        {
            enemy.kill();
        }
        else if(target.tag == "Cleaner")
        {
            transform.position = position;
            rb.velocity = transform.right * 0;
        }
    }

}
