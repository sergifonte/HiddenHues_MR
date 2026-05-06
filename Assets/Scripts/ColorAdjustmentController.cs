using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ColorAdjustmentController : MonoBehaviour
{
    public Volume globalVolume;

    // Aquí arrossegarem les textures "simulate" de GitHub
    public Texture2D lutProtanopia;
    public Texture2D lutDeuteranopia;
    public Texture2D lutTritanopia;

    private ColorLookup colorLookup;

    void Start()
    {
        // Busquem l'efecte Color Lookup dins del perfil del Global Volume
        if (globalVolume != null && globalVolume.profile.TryGet(out colorLookup))
        {
            Debug.Log("Sistema de LUTs preparat!");
            // Comencem amb el filtre apagat (Visió Normal)
            colorLookup.active = false;
        }
        else
        {
            Debug.LogError("Error: No s'ha trobat l'override 'Color Lookup' al Global Volume. Afegeix-lo primer!");
        }
    }

    public void SetFilter(int index)
    {
        if (colorLookup == null) return;

        Debug.Log("Canviant a mode: " + index);

        switch (index)
        {
            case 0: // Normal
                colorLookup.active = false; // Simplement apaguem el filtre
                break;

            case 1: // Protanopia
                colorLookup.active = true;
                colorLookup.texture.value = lutProtanopia;
                break;

            case 2: // Deuteranopia
                colorLookup.active = true;
                colorLookup.texture.value = lutDeuteranopia;
                break;

            case 3: // Tritanopia
                colorLookup.active = true;
                colorLookup.texture.value = lutTritanopia;
                break;
        }
    }
}