using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System.Collections.Generic;

public class TableZone : MonoBehaviour
{
    public ARPlaneManager planeManager;
    // Llista automàtica dels cubs que toquen la taula
    public List<TrackedCube> cubesOnTable = new List<TrackedCube>();

    void Update()
    {
        if (planeManager == null) return;

        // El collider es mou automàticament on detecti la taula real
        foreach (var plane in planeManager.trackables)
        {
            if (plane.alignment == UnityEngine.XR.ARSubsystems.PlaneAlignment.HorizontalUp)
            {
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
        }
    }

    private void OnTriggerExit(Collider other)
    {
        TrackedCube cube = other.GetComponent<TrackedCube>();
        if (cube != null && cubesOnTable.Contains(cube))
        {
            cubesOnTable.Remove(cube);
            
            // Avisem al GameManager que algú ha AIXECAT un cub
            if (GameManagerMR.Instance != null)
            {
                GameManagerMR.Instance.OnCubeLifted(cube);
            }
        }
    }
}