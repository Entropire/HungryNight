using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class GeoUi : MonoBehaviour
{
    [SerializeField] TMP_Text GeoAmount;
    [SerializeField] TMP_Text GeoAdded;
    [SerializeField] Color EventualColor;
    [SerializeField] private float DelayBeforeAdding = 2.5f;
    [SerializeField] private float AddDuration = 0.3f;

    private AudioSource GeoDeplition;

    private int TotalAmount = 0;
    private int AddedAmount = 0;

    private float CountdownTimer = 0f;
    private float AddProgressTimer = 0f;

    public UnityEvent OnGeoUpdateComplete;

    private void Start()
    {
        GeoDeplition = GetComponent<AudioSource>();
        OnGeoUpdateComplete ??= new UnityEvent();
    }

    private void Update()
    {
        if (CountdownTimer > 0)
        {
            GeoDeplition.Stop();
            CountdownTimer -= Time.deltaTime;
        }
        else if (AddedAmount > 0 && AddProgressTimer < AddDuration)
        {    
            AddProgressTimer += Time.deltaTime;
            float progress = Mathf.Clamp01(AddProgressTimer / AddDuration);
            if (!GeoDeplition.isPlaying)
            {
                GeoDeplition.Play();
            }
            int amountToAdd = Mathf.RoundToInt(progress * AddedAmount);
            GeoAmount.text = (TotalAmount + amountToAdd).ToString();
            GeoAdded.text = "+" + (AddedAmount - amountToAdd).ToString();

            if (progress >= 1f)
            {
                FinalizeGeoCount();
            }
        }
    }

    public void AddGeo(int amount)
    {
        GeoAdded.color = Color.white;
        AddedAmount += amount;
        GeoAdded.text = "+" + AddedAmount.ToString();
        CountdownTimer = DelayBeforeAdding;
        AddProgressTimer = 0f;
    }

    private void FinalizeGeoCount()
    {
        GeoDeplition.Stop();
        TotalAmount += AddedAmount;
        AddedAmount = 0;
        GeoAdded.text = "0";
        GeoAdded.color = EventualColor;
        OnGeoUpdateComplete.Invoke();
    }
}

