using UnityEngine;
using UnityEngine.Rendering.Universal; // Required for Light2D

public class LightFlicker : MonoBehaviour
{
    public Light2D torchLight;             // Drag your Light2D here
    public float minIntensity = 0.6f;      // Soft flicker low point
    public float maxIntensity = 1.2f;      // Brighter flicker peak
    public float flickerSpeed = 0.1f;      // Speed of change (lower = faster flicker)

    private float targetIntensity;
    private float flickerTimer;

    void Start()
    {
        if (torchLight == null)
            torchLight = GetComponent<Light2D>();

        targetIntensity = torchLight.intensity;
        flickerTimer = flickerSpeed;
    }

    void Update()
    {
        flickerTimer -= Time.deltaTime;

        if (flickerTimer <= 0f)
        {
            // Set a new random target intensity
            targetIntensity = Random.Range(minIntensity, maxIntensity);
            flickerTimer = Random.Range(flickerSpeed * 0.8f, flickerSpeed * 1.2f);
        }

        // Smoothly flicker toward the target intensity
        torchLight.intensity = Mathf.Lerp(torchLight.intensity, targetIntensity, Time.deltaTime * 5f);
    }
}
