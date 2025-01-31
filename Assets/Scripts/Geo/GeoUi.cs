using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class GeoUi : MonoBehaviour
{
    [Header("Geo Text")] 
    [SerializeField] TMP_Text GeoAmount; // UI text to display the total geo amount
    [SerializeField] TMP_Text GeoAdded; // UI text to display the added geo amount

    [Header("Geo Depletion Time")] 
    [SerializeField] Color EventualColor; // Color to change geo text after update
    [SerializeField] private float DelayBeforeAdding = 2.5f; // Delay before geo starts adding
    [SerializeField] private float AddDuration = 0.3f; // Duration over which geo is added

    private AudioSource GeoDeplition; // Audio source for geo depletion sound

    private int TotalAmount = 0; // Tracks total geo amount
    private int AddedAmount = 0; // Tracks the amount of geo to be added

    private float CountdownTimer = 0f; // Timer before adding geo starts
    private float AddProgressTimer = 0f; // Timer for tracking geo addition progress


    private void Start()
    {
        GeoDeplition = GetComponent<AudioSource>(); // Gets the AudioSource component
    }

    private void Update()
    {
        if (CountdownTimer > 0)
        {
            GeoDeplition.Stop(); // Stops audio while waiting
            CountdownTimer -= Time.deltaTime; // Decrease countdown timer
        }
        else if (AddedAmount > 0 && AddProgressTimer < AddDuration)
        {    
            AddProgressTimer += Time.deltaTime; // Increase progress timer
            float progress = Mathf.Clamp01(AddProgressTimer / AddDuration); // Calculate progress ratio
            
            if (!GeoDeplition.isPlaying)
            {
                GeoDeplition.Play(); // Play audio when adding geo
            }
            
            int amountToAdd = Mathf.RoundToInt(progress * AddedAmount); // Calculate the amount to add based on progress
            GeoAmount.text = (TotalAmount + amountToAdd).ToString(); // Update geo amount display
            GeoAdded.text = "+" + (AddedAmount - amountToAdd).ToString(); // Update added geo text
            
            if (progress >= 1f)
            {
                FinalizeGeoCount(); // Finalize geo count when addition is complete
            }
        }
    }

    public void AddGeo(int amount)
    {
        GeoAdded.color = Color.white; // Reset added geo text color
        AddedAmount += amount; // Add amount to pending geo addition
        GeoAdded.text = "+" + AddedAmount.ToString(); // Update added geo text
        CountdownTimer = DelayBeforeAdding; // Set countdown timer before addition starts
        AddProgressTimer = 0f; // Reset progress timer
    }

    private void FinalizeGeoCount()
    {
        GeoDeplition.Stop(); // Stop audio when geo addition is complete
        TotalAmount += AddedAmount; // Update total geo amount
        AddedAmount = 0; // Reset added amount
        GeoAdded.text = "0"; // Reset added geo text
        GeoAdded.color = EventualColor; // Change geo text color to final color
    }
}

