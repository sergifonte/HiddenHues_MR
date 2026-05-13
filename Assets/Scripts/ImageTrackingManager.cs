using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System.Collections.Generic;

public class ImageTrackingManager : MonoBehaviour
{
    public ARTrackedImageManager trackedImageManager;

    [Header("Prefabs dels cubs virtuals (amb TrackedCube script)")]
    public GameObject prefabVermell;
    public GameObject prefabGroc;
    public GameObject prefabBlau;
    public GameObject prefabVerd;

    private Dictionary<string, TrackedCube> spawnedCubes = new Dictionary<string, TrackedCube>();

    private void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnImagesChanged;
    }

    private void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnImagesChanged;
    }

    private void OnImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            string imageName = trackedImage.referenceImage.name;
            GameObject prefabToSpawn = null;

            switch (imageName)
            {
                case "PatroVermell": prefabToSpawn = prefabVermell; break;
                case "PatroGroc": prefabToSpawn = prefabGroc; break;
                case "PatroBlau": prefabToSpawn = prefabBlau; break;
                case "PatroVerd": prefabToSpawn = prefabVerd; break;
            }

            if (prefabToSpawn != null)
            {
                GameObject newCubeGO = Instantiate(prefabToSpawn, trackedImage.transform.position, trackedImage.transform.rotation);
                TrackedCube cubeScript = newCubeGO.GetComponent<TrackedCube>();
                cubeScript.trackedImage = trackedImage;

                spawnedCubes.Add(imageName, cubeScript);
            }
        }
    }
}