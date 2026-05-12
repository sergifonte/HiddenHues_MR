using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ColorAdjustmentController : MonoBehaviour
{
    public Volume globalVolume;

    [Header("Textures LUT (Arrossega les de GitHub)")]
    public Texture2D lutProtanopia;
    public Texture2D lutDeuteranopia;
    public Texture2D lutTritanopia;

    private ColorLookup colorLookup;
    private float currentIntensity = 1.0f;

    void Start()
    {
        if (globalVolume != null && globalVolume.profile.TryGet(out colorLookup))
        {
            colorLookup.active = false;
        }
        else
        {
            Debug.LogError("Error: No s'ha trobat 'Color Lookup' al Global Volume!");
        }
    }

    public void SetFilter(int index)
    {
        if (colorLookup == null) return;

        if (index == 0)
        {
            colorLookup.active = false;
        }
        else
        {
            colorLookup.active = true;
            colorLookup.contribution.value = currentIntensity;

            switch (index)
            {
                case 1: colorLookup.texture.value = lutProtanopia; break;
                case 2: colorLookup.texture.value = lutDeuteranopia; break;
                case 3: colorLookup.texture.value = lutTritanopia; break;
            }
        }
    }

    public void SetIntensity(float value)
    {
        currentIntensity = value;
        if (colorLookup != null && colorLookup.active)
        {
            colorLookup.contribution.value = currentIntensity;
        }
    }
}