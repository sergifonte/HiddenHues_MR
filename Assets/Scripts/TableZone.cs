using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System.Collections.Generic;

public class TableZone : MonoBehaviour
{
    public ARPlaneManager planeManager;
    public List<TrackedCube> cubesOnTable = new List<TrackedCube>();

    void Update()
    {
        // Si encara no hem fixat la taula, busquem un pla horitzontal
        foreach (var plane in planeManager.trackables)
        {
            if (plane.alignment == UnityEngine.XR.ARSubsystems.PlaneAlignment.HorizontalUp)
            {
                // Posem la zona de detecció sobre la taula detectada
                transform.position = plane.center;
                break;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        TrackedCube cube = other.GetComponent<TrackedCube>();
        if (cube != null && !cubesOnTable.Contains(cube))
        {
            cubesOnTable.Add(cube);
            GameManagerMR.Instance.OnCubePlaced(); // Avisem que hi ha un cub més
        }
    }

    private void OnTriggerExit(Collider other)
    {
        TrackedCube cube = other.GetComponent<TrackedCube>();
        if (cube != null && cubesOnTable.Contains(cube))
        {
            cubesOnTable.Remove(cube);
            GameManagerMR.Instance.OnCubeLifted(cube);
        }
    }
}