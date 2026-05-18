using UnityEngine;

public class GameFlowManager : MonoBehaviour
{
    public static GameFlowManager Instance;

    [Header("Canvases de l'Escena")]
    public GameObject canvasMenuPrincipal;
    public GameObject canvasPantallaPrincipal;

    [Header("Scripts de Lògica")]
    public GameManagerMR gameManagerScript;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // Al començar, només es veu el Menú Principal
        MostrarMenuPrincipal();
    }

    public void MostrarMenuPrincipal()
    {
        canvasMenuPrincipal.SetActive(true);
        canvasPantallaPrincipal.SetActive(false);

        // Si l'usuari torna al menú, parem la partida si estava en marxa
        if (gameManagerScript != null)
        {
            gameManagerScript.StopAllCoroutines();
        }
    }

    // Aquest botó està al Menú Principal (Play) i t'obre la pantalla de configuració/joc
    public void ObrirPantallaPrincipal()
    {
        canvasMenuPrincipal.SetActive(false);
        canvasPantallaPrincipal.SetActive(true);
    }

    public void PremerBotoComencarJoc()
    {
        if (gameManagerScript != null)
        {
            gameManagerScript.StartGame();
        }
    }

    public void SortirDeLApp()
    {
        Debug.Log("Sortint de l'aplicació...");
        Application.Quit();
    }
}