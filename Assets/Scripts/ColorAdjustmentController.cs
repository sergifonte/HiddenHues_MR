using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ColorAdjustmentController : MonoBehaviour
{
    public Volume globalVolume;

    [Header("Textures LUT")]
    public Texture2D lutProtanopia;
    public Texture2D lutDeuteranopia;
    public Texture2D lutTritanopia;

    private ColorLookup colorLookup;
    private float currentIntensity = 1.0f;

    void Start()
    {
        // Verificamos que el objeto Global Volume esté asignado
        if (globalVolume != null)
        {
            // Intentamos obtener el perfil y el efecto Color Lookup
            if (globalVolume.profile.TryGet<ColorLookup>(out colorLookup))
            {
                colorLookup.active = false;
                Debug.Log("¡Color Lookup configurado correctamente!");
            }
            else
            {
                Debug.LogError("Error: El perfil del Global Volume NO tiene el efecto 'Color Lookup' añadido.");
            }
        }
        else
        {
            Debug.LogError("Error: No has arrastrado el objeto 'Global Volume' al script en el Inspector.");
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