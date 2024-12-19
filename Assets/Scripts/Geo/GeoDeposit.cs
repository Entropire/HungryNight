using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeoDeposit : MonoBehaviour
{
    int totalAmount = 20;
    int health = 4;
    private Animator Geo;
    [SerializeField] Transform ChildTransform;
    [SerializeField] List<GameObject> DepositList;
    [SerializeField] List<Vector2> DirectionList;
    [SerializeField] GameObject[] Food;

    private void Start()
    {
        Geo = ChildTransform.GetComponent<Animator>();
        DirectionList.Add(Vector2.up);
        DirectionList.Add(Vector2.left);
        DirectionList.Add(Vector2.right);

        AttackCollider.OnPlayerAttacked += (gb) =>
        {
            Debug.Log("recieved attack event");
            if (gb == gameObject)
            {
                StartCoroutine(geospawn());
                Debug.Log("hit geo");
            }
        };
    }

    IEnumerator geospawn(int amount = 3, int currentamount = 0)
    {
        health -= 1;
        if (health == 0)
        {
            amount = totalAmount - currentamount;
        }

        currentamount += amount;
        for (int i = 0; i < amount; i++)
        {
            GameObject geo = Instantiate(Food[Random.Range(0, 3)], transform.position, Quaternion.identity);
            //geo.transform.position = directionList[Random.Range(1,3)];
            DepositList.Add(geo);
            yield return new WaitForSeconds(0.01f);
        }

        if (totalAmount == currentamount)
        {
            Geo.Play("GeoStone");
            Destroy(this);
        }
    }
}




