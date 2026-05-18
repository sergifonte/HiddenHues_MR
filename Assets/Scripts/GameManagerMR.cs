using UnityEngine;
using TMPro;
using System.Collections;

public class GameManagerMR : MonoBehaviour
{
    public static GameManagerMR Instance;

    [Header("UI del Joc")]
    public TextMeshProUGUI textInstructions;
    public GameObject botoGoToLevel;
    public GameObject fletxaTornarAlMenu; 
    public GameObject fletxaAturarJoc;    

    [Header("Connexió amb la Taula")]
    public TableZone tableZone;

    private string[] colorNames;
    private int currentTargetColorID;
    private bool isGamePlaying = false;
    private bool isWaitingForNextRound = false;

    private Coroutine errorTextCoroutine; // Per controlar el temporitzador de l'error

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        colorNames = new string[] { "RED", "YELLOW", "BLUE", "GREEN" };
    }

    private void Start()
    {
        ResetUIInicial();
    }

    public void ResetUIInicial()
    {
        StopAllCoroutines();
        if (textInstructions != null) textInstructions.text = "";
        if (botoGoToLevel != null) botoGoToLevel.SetActive(true);
        if (fletxaTornarAlMenu != null) fletxaTornarAlMenu.SetActive(true);
        if (fletxaAturarJoc != null) fletxaAturarJoc.SetActive(false);
    }

    public void IntentarComencarJoc()
    {
        // Si hi havia un temporitzador d'error actiu de l'anterior clic, el cancel·lem
        if (errorTextCoroutine != null) StopCoroutine(errorTextCoroutine);

        if (ValidarCubsMinims())
        {
            isGamePlaying = true;

            // L'INTERCANVI NOMÉS PASSA AQUÍ (Quan tot és correcte)
            if (botoGoToLevel != null) botoGoToLevel.SetActive(false);
            if (fletxaTornarAlMenu != null) fletxaTornarAlMenu.SetActive(false);
            if (fletxaAturarJoc != null) fletxaAturarJoc.SetActive(true);

            NextRound();
        }
        else
        {
            // Si falten cubs, iniciem el temporitzador per esborrar el text automàticament
            errorTextCoroutine = StartCoroutine(MostrarErrorTemporitzat());
        }
    }

    private IEnumerator MostrarErrorTemporitzat()
    {
        textInstructions.text = "<size=140%><b>MISSING CUBES!</b></size>\n\n<size=90%>Make sure there is at least one cube of each color on the table:\n<i>Red, Yellow, Blue, and Green.</i></size>";
        
        // El missatge es queda visible durant 4 segons
        yield return new WaitForSeconds(4f);
        
        // S'esborra sol sense tocar cap botó
        textInstructions.text = "";
    }

    private bool ValidarCubsMinims()
    {
        if (tableZone == null) return false;

        // NOTA PER TESTEJAR A L'ORDENADOR: Desmarca la línia de sota si vols provar el joc sense tenir cubs reals a l'editor
        // return true;

        bool teVermell = false;
        bool teGroc = false;
        bool teBlau = false;
        bool teVerd = false;

        foreach (TrackedCube cube in tableZone.cubesOnTable)
        {
            if (cube.colorID == 0) teVermell = true;
            if (cube.colorID == 1) teGroc = true;
            if (cube.colorID == 2) teBlau = true;
            if (cube.colorID == 3) teVerd = true;
        }

        return (teVermell && teGroc && teBlau && teVerd);
    }

    public void NextRound()
    {
        isWaitingForNextRound = false;
        currentTargetColorID = Random.Range(0, colorNames.Length);
        textInstructions.text = "<size=90%>YOUR TARGET:</size>\n<size=150%><b>LIFT THE " + colorNames[currentTargetColorID] + " CUBE</b></size>";
    }

    public void OnCubeLifted(TrackedCube cube)
    {
        if (!isGamePlaying || isWaitingForNextRound) return;

        if (cube.colorID == currentTargetColorID)
        {
            StartCoroutine(MostrarCorrecte());
        }
        else
        {
            textInstructions.text = "<size=140%><b>INCORRECT!</b></size>\n\n<size=90%>That is the " + colorNames[cube.colorID] + " cube.\nTry again: Lift the <b>" + colorNames[currentTargetColorID] + "</b> cube.</size>";
        }
    }

    private IEnumerator MostrarCorrecte()
    {
        isWaitingForNextRound = true;
        textInstructions.text = "<size=160%><b>CORRECT!</b> 🎉</size>";
        
        yield return new WaitForSeconds(3f);
        
        NextRound();
    }

    public void NetejarIApagarJoc()
    {
        isGamePlaying = false;
        isWaitingForNextRound = false;
        
        StopAllCoroutines();
        
        if (textInstructions != null) textInstructions.text = "";
        if (botoGoToLevel != null) botoGoToLevel.SetActive(true); 
        if (fletxaTornarAlMenu != null) fletxaTornarAlMenu.SetActive(true);
        if (fletxaAturarJoc != null) fletxaAturarJoc.SetActive(false);
        
        if (tableZone != null && tableZone.cubesOnTable != null)
        {
            tableZone.cubesOnTable.Clear();
        }
    }

    public bool IsGamePlaying()
    {
        return isGamePlaying;
    }
}