using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class GeoUi : MonoBehaviour
{
    [SerializeField] TMP_Text GeoAmount;
    [SerializeField] TMP_Text GeoAdded;

    [SerializeField] private float countdownTimer = 0f;
    [SerializeField] private float geoPickupTime =  2f;
    [SerializeField] private float updateInterval = 0.05f;

    private int totalAmount = 0; 
    private int addedAmount = 0; 

    private float nextUpdateTime = 0f; 

    public UnityEvent OnGeoUpdateComplete;

    private void Start()
    {
        if (OnGeoUpdateComplete == null) 
        { 
            OnGeoUpdateComplete = new UnityEvent(); 
        }
    }

    private void Update()
    {
        if (countdownTimer > 0)
        {
            countdownTimer -= Time.deltaTime;
        }

        if (countdownTimer <= 0 && addedAmount > 0)
        {
            if (Time.time >= nextUpdateTime)
            {
                IncrementGeoCount();
                nextUpdateTime = Time.time + updateInterval;
            }

            if (addedAmount <= 0)
            {
                FinalizeGeoCount();
            }
        }
    }

    public void AddGeo(int amount)
    {
        addedAmount += amount;
        GeoAdded.text = addedAmount.ToString();

        countdownTimer = geoPickupTime;
     
        nextUpdateTime = Time.time + updateInterval;
    }

    private void IncrementGeoCount()
    { 
        totalAmount += 1;
        addedAmount -= 1;
 
        GeoAmount.text = totalAmount.ToString();
        GeoAdded.text = addedAmount.ToString();
    }

    private void FinalizeGeoCount()
    {
        addedAmount = 0;
        GeoAdded.text = "0";

        OnGeoUpdateComplete.Invoke();
    }
}

