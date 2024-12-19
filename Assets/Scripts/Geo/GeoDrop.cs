using System;
using UnityEngine;
using Random = UnityEngine.Random;

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            geoUi.AddGeo(1);
            Destroy(gameObject);
        }
    }
}
