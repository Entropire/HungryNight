using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class GeoUi : MonoBehaviour
{
    [SerializeField] TMP_Text GeoAmount;
    [SerializeField] TMP_Text GeoAdded;

    [SerializeField] private float CountdownTimer = 0f;
    [SerializeField] private float GeoPickupTime = 2f; // 1 second for the total addition
    [SerializeField] private float UpdateInterval = 0.1f;

    private int TotalAmount = 0;
    private int AddedAmount = 0;

    private float NextUpdateTime = 0f;
    private float TimeToAddGeo = 0f; // Tracks how much time is left to add the current added amount

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
        if (CountdownTimer > 0)
        {
            CountdownTimer -= Time.deltaTime;
        }

        if (CountdownTimer <= 0 && AddedAmount > 0)
        {
            if (TimeToAddGeo > 0)
            {
                TimeToAddGeo -= Time.deltaTime; // Decrease time remaining to add all Geo
                int incrementAmount = Mathf.CeilToInt((float)AddedAmount * Time.deltaTime / GeoPickupTime); // Amount to add this frame
                TotalAmount += incrementAmount;
                AddedAmount -= incrementAmount;

                // Update the UI text
                GeoAmount.text = TotalAmount.ToString();
                GeoAdded.text = "+" + AddedAmount.ToString();

                // If there's no Geo left to add, finalize the process
                if (AddedAmount <= 0)
                {
                    FinalizeGeoCount();
                }
            }
        }
    }

    public void AddGeo(int amount)
    {
        AddedAmount += amount;
        GeoAdded.text = "+" + AddedAmount.ToString();

        // Ensure it always takes 1 second to add the total amount
        TimeToAddGeo = GeoPickupTime;
        CountdownTimer = GeoPickupTime; 
    }

    private void FinalizeGeoCount()
    {
        // Reset added amount and UI text once the addition is done
        AddedAmount = 0;
        GeoAdded.text = "0";

        OnGeoUpdateComplete.Invoke();
    }
}