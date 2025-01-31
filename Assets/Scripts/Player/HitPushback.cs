using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitPushback : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool ishitCooldown;
    private int YhitCords = 4;
    private bool isntFallSpike;
    private PlayerMovement playMovement;
    [SerializeField] private Image Blackfade;
    [SerializeField] private List<Vector3> LastPos;
    [SerializeField] private Color BaseColor;
    [SerializeField] private Color ToColor;
    [SerializeField] private float Hittime = 0.03f;
    [SerializeField] private float timeelepsed;
    [SerializeField] private float hitCooldowntime = 1f;
    void Start()
    {
        Blackfade.color = BaseColor;
        ToColor = BaseColor;
        rb = GetComponent<Rigidbody2D>();
        playMovement = GetComponent<PlayerMovement>();
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
            if (timeelepsed > 0.7f)
            {
                
                if (isntFallSpike)
                {
                    transform.position = LastPos[0];
                    rb.velocity = new Vector3(0, 0, 0);
                }
            }
            if (timeelepsed > 0.7)
            {
                playMovement.enabled = true;
            }
            if (timeelepsed > hitCooldowntime)
            {
                ToColor =  BaseColor;
                ishitCooldown = false;
                timeelepsed = 0;
            }
            
        }   
        Blackfade.color = Color.Lerp(Blackfade.color, ToColor, 1f * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Spike"))
        {
            Vector2 direction = (transform.position - other.gameObject.transform.position).normalized;
            if (!ishitCooldown)
            {
                if(direction.y > 0 && other.GetComponent<SpikeFall>() == null)
                {
                    YhitCords = 32;
                    ToColor = Color.black;
                    isntFallSpike = true;
                }else
                {
                    YhitCords = 18;
                    ToColor = BaseColor;
                    isntFallSpike = false;
                }

                // Gethit?.Invoke();

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
            playMovement.enabled = false;
        }
    }    
}
