using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class GeoDrop : MonoBehaviour
{
    Rigidbody2D body;
    [SerializeField] private GeoUi geoUi;
    [SerializeField] GameObject GeoObject;

    private void Start()
    {
        GeoObject = FindObjectOfType<GeoUi>().gameObject;
        geoUi = GeoObject.GetComponent<GeoUi>();
        body = GetComponent<Rigidbody2D>();
        Vector2 Push = new Vector2(Random.Range(-200, 200), Random.Range(0, 600));
        body.AddForce(Push);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            geoUi.AddGeo(1);
            Destroy(this.gameObject);
        }
    }
}
