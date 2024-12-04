using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeFall : MonoBehaviour
{
    [SerializeField] LayerMask layermaskPlayer; 
    [SerializeField] LayerMask layermaskGround;
    bool playerTrigger;
    bool isAirborn;
    float speed;


    void Start()
    {
        layermaskPlayer = LayerMask.GetMask("Entity");
        layermaskGround = LayerMask.GetMask("Ground");
    }   

    
    void Update()
    {
        Vector2 pos = new Vector2(
            transform.position.x,
            transform.position.y - transform.localScale.y / 2 - .1f
        );

        // playerTrigger = Physics2D.BoxCast(pos, new Vector2(1, transform.localScale.y), 0, Vector2.down, 3, layermaskPlayer);
        if (Physics2D.BoxCast(pos, new Vector2(1, transform.localScale.y), 0, Vector2.down, 5, layermaskPlayer) && !playerTrigger)
        {
            playerTrigger = true;
        }
    	if (playerTrigger)
        {
            if (Physics2D.Raycast(transform.position, Vector2.down, 0.1f, layermaskGround))
            {
                isAirborn = false;
            }else
            {
                isAirborn = true;
                transform.position += new Vector3(0, -20, 0) * speed * Time.deltaTime;
                speed += 0.004f;
            }
        }
    }
}
    