using UnityEngine;
using TMPro;

public class GameManagerMR : MonoBehaviour
{
    public static GameManagerMR Instance;

    public TextMeshProUGUI textInstructions;
    public string[] colorNames = { "VERMELL", "GROC", "BLAU", "VERD" };

    private int currentTargetColorID;
    private bool isWaitingForAction = false;

    private void Awake()
    {
        Instance = this;
    }

    public void OnCubePlaced()
    {
        // Simplement per saber que l'usuari ha posat els cubs a la taula
        Debug.Log("Cub detectat a la taula.");
    }

    // Aquesta funció es crida des de TableZone
    public void OnCubeLifted(TrackedCube cube)
    {
        if (!isWaitingForAction) return;

        if (cube.colorID == currentTargetColorID)
        {
            textInstructions.text = "CORRECTE! Has aixecat el " + colorNames[cube.colorID];
            isWaitingForAction = false;
            Invoke("NextRound", 3f);
        }
        else
        {
            textInstructions.text = "ERROR! Aquest és el " + colorNames[cube.colorID];
        }
    }

    public void NextRound()
    {
        currentTargetColorID = Random.Range(0, colorNames.Length);
        textInstructions.text = "AIXECA EL CUB: " + colorNames[currentTargetColorID];
        isWaitingForAction = true;
    }

    // Botó per començar el joc des de la UI
    public void StartGame()
    {
        NextRound();
    }
}