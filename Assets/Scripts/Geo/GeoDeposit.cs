using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeoDeposit : MonoBehaviour
{
    int totalAmount = 40;
    int health = 4;


    [SerializeField] List<GameObject> depositList;
    [SerializeField] List<Vector2> directionList;
    [SerializeField] GameObject food;

    private void Start()
    {
        directionList.Add(Vector2.up);
        directionList.Add(Vector2.left);
        directionList.Add(Vector2.right);
    }
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(geospawn());
        }
    }
    IEnumerator geospawn(int amount = 5, int currentamount = 0)
    {
        health -= 1;
        if (health == 0)
        {
            amount = totalAmount - currentamount;
        }

        currentamount += amount;
        Debug.Log(currentamount);
        for (int i = 0; i < amount; i++)
        {
            GameObject geo = Instantiate(food, transform.position, Quaternion.identity);
            geo.transform.position = directionList[Random.RandomRange(1,3)];
            depositList.Add(geo);
            yield return new WaitForSeconds(0.001f);
        }

        if (totalAmount == currentamount)
        {
           Destroy(this.gameObject);
        }
    }
}
