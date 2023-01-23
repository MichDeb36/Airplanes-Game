using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb;
    private float maxFlightAltitude = 3.0f;
    private float minFlightAltitude = -4.0f;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void airplaneControl()
    {
        if (Keyboard.current.upArrowKey.IsPressed())
        {
            if (transform.position.y > maxFlightAltitude)
                rb.velocity = transform.up * 0;
            else
                rb.velocity = transform.up * speed;
        }
        else if (Keyboard.current.downArrowKey.IsPressed())
        {
            if (transform.position.y < minFlightAltitude)
                rb.velocity = transform.up * 0;
            else
                rb.velocity = -transform.up * speed;
        }
    }

    public float GetMaxFlightAltitude()
    {
        return maxFlightAltitude;
    }
    public float GetMinFlightAltitude()
    {
        return minFlightAltitude;
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        Enemy enemy = target.GetComponent<Enemy>();
        if (target.tag == "Enemy")
        {
            GameManager.Instance.lifeLost();
            enemy.kill();
        }
    }

    void Update()
    {
        airplaneControl();
    }
}
