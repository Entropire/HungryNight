using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class GeoDeposit : MonoBehaviour
{
    int totalAmount = 20;
    int health = 4;
    private Animator Geo;
    [SerializeField] Transform childTransform;
    [SerializeField] List<GameObject> depositList;
    [SerializeField] List<Vector2> directionList;
    [SerializeField] GameObject food;

    private void Start()
    {
        Geo = childTransform.GetComponent<Animator>();
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
    IEnumerator geospawn(int amount = 3, int currentamount = 0)
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
            //geo.transform.position = directionList[Random.Range(1,3)];
            depositList.Add(geo);
            yield return new WaitForSeconds(0.01f);
        }

        if (totalAmount == currentamount)
        {
            Geo.Play("GeoStone");
            Destroy(this);
        }
    }
}




