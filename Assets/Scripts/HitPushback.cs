using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions.Must;

public class HitPushback : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool ishitCooldown;
    private int YhitCords = 4;
    public static event Action Gethit;
    public Image Blackfade;
    [SerializeField] private LayerMask Spiketrap;
    [SerializeField] private Color BaseColor;
    [SerializeField] private Color CurrentColor;
    [SerializeField] private Color ToColor;
    [SerializeField] private float Hittime = 0.03f;
    [SerializeField] private float timeelepsed;
    [SerializeField] private float hitCooldowntime = 1f;
    void Start()
    {
        Blackfade.color = BaseColor;
        ToColor = BaseColor;
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
            if (timeelepsed > hitCooldowntime)
            {
                ishitCooldown = false;
                timeelepsed = 0;
            }
            
        }   
        if (Physics2D.CircleCast(transform.position, 2, Vector2.zero, 8f, Spiketrap))
        {
            Debug.Log("Ghello");
        }
        Blackfade.color = Color.Lerp(Blackfade.color, ToColor, 1f * Time.fixedDeltaTime);
        if (Blackfade.color == Color.black)
        {
            ToColor =  BaseColor;
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
                Debug.Log(direction.y);
                if(direction.y > 0 && other.GetComponent<SpikeFall>() == null)
                {
                    YhitCords = 16;
                    ToColor = Color.black;
                }else
                {
                    YhitCords = 7;
                    ToColor = BaseColor;
                }

                Gethit?.Invoke();

                if (direction.x > 0)
                {
                    rb.AddForce(new Vector2(4, YhitCords), ForceMode2D.Impulse);
                }
            else 
            {
                rb.AddForce(new Vector2(-4, YhitCords), ForceMode2D.Impulse);
            }
            Time.timeScale = 0.1f;
            }
            ishitCooldown = true;
        }
    }
}
