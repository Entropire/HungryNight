using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{

    public Transform player;
    // private Vector3 Pos;
    public float moveSpeed = 1f;
    private Vector3 targetPosition;

    // Update is called once per frame
    void Update () 
    {   
        targetPosition = player.transform.position + new Vector3(0, 1, -5);
        if(player.transform.position.y < 0)
        {
            transform.position = player.transform.position + new Vector3(0, 3, -5);
        } else
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);
        }
        
    }

}
