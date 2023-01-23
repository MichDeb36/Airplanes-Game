using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }

    void kill()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        Enemy enemy = target.GetComponent<Enemy>();

        if (target.tag == "Enemy")
        {
            GameManager.Instance.AddScore();
            enemy.kill();
            kill();
        }
        else if(target.tag == "Cleaner")
        {
            kill();
        }
    }


}
