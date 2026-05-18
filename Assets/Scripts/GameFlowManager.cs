using UnityEngine;

public class GameFlowManager : MonoBehaviour
{
    public static GameFlowManager Instance;

    [Header("Canvases de l'Escena")]
    public GameObject canvasMenuPrincipal;
    public GameObject canvasPantallaPrincipal;

    [Header("Scripts de Lògica")]
    public GameManagerMR gameManagerScript;

    [Header("Objectes del Joc (Opcional)")]
    public GameObject objecteGAME; // Arrossega aquí l'objecte GAME de la Hierarchy si vols desactivar-lo sencer

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        MostrarMenuPrincipal();
    }

    public void MostrarMenuPrincipal()
    {
        canvasMenuPrincipal.SetActive(true);
        canvasPantallaPrincipal.SetActive(false);

        if (gameManagerScript != null)
        {
            gameManagerScript.StopAllCoroutines();
        }
        
        // Si vols que l'objecte GAME estigui apagat al menú del principi:
        if (objecteGAME != null) objecteGAME.SetActive(false);
    }

    public void ObrirPantallaPrincipal()
    {
        canvasMenuPrincipal.SetActive(false);
        canvasPantallaPrincipal.SetActive(true);
        
        // Quan entrem a la pantalla dels filtres, activem el GAME perquè l'ImageTracking comenci a detectar cubs
        if (objecteGAME != null) objecteGAME.SetActive(true);
    }

    public void PremerBotoComencarJoc()
    {
        if (gameManagerScript != null)
        {
            gameManagerScript.IntentarComencarJoc();
        }
    }

    // Aquesta és la funció que s'ha d'executar quan es premi la fletxa d'enrere
    public void PremerBotoTornarEnrere()
    {
        if (gameManagerScript != null && gameManagerScript.IsGamePlaying())
        {
            // 1. Apaguem la lògica del joc, netegem text i reactivem el botó Go To Level
            gameManagerScript.NetejarIApagarJoc();
        }

        // 2. Si vols desactivar del tot l'objecte GAME (amb la TableZone i els cubs) en sortir:
        if (objecteGAME != null) objecteGAME.SetActive(false);

        // 3. Portem l'usuari directament al menú de botons de Play i Exit
        MostrarMenuPrincipal();
    }

    public void SortirDeLApp()
    {
        Debug.Log("Sortint de l'aplicació...");
        Application.Quit();
    }
}