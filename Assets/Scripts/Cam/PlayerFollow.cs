using UnityEngine;

public class PlayerFollow : MonoBehaviour
{

    public Transform player;
    public float moveSpeed = 1f;
    private Vector3 targetPosition;

    // Update is called once per frame
    void Update () 
    {   
        targetPosition = player.transform.position + new Vector3(0, 1, -5);
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);
    }

}
