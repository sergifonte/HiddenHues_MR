using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class TrackedCube : MonoBehaviour
{
    // 0=Red, 1=Yellow, 2=Blue, 3=Green
    public int colorID;
    public ARTrackedImage trackedImage;

    private void OnEnable()
    {
        // Ens assegurem que l'objecte virtual segueix la imatge virtualinstanciada
        if (trackedImage != null)
        {
            transform.position = trackedImage.transform.position;
            transform.rotation = trackedImage.transform.rotation;
        }
    }

    private void Update()
    {
        // Segueix la posició en temps real de la imatge trackejada
        if (trackedImage != null && trackedImage.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Tracking)
        {
            transform.position = trackedImage.transform.position;
            transform.rotation = trackedImage.transform.rotation;
        }
    }
}