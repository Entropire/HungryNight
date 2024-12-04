using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class HitPushback : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool ishitCooldown;
    private float timeelepsed;
    public static event Action Gethit;
    [SerializeField] private float Hittime = 0.03f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame, 
    void Update()
    {
        if (ishitCooldown)
        {
            timeelepsed += Time.deltaTime;
            if (timeelepsed > Hittime)
            {
                Time.timeScale = 1;
            }
            if (timeelepsed > 1f)
            {
                ishitCooldown = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Spike"))
        {
            Vector2 direction = (transform.position - other.gameObject.transform.position).normalized;
            // Debug.Log(direction);
            if (!ishitCooldown)
            {
                Gethit?.Invoke();
                if (direction.x > 0)
            {
                rb.AddForce(new Vector2(4, 4f), ForceMode2D.Impulse);
            }
            else 
            {
                rb.AddForce(new Vector2(-4, 4f), ForceMode2D.Impulse);
            }
            Time.timeScale = 0.1f;
            }
            ishitCooldown = true;
        }
    }
}
