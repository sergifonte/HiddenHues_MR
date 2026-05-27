using UnityEngine;

public class LerpMov : MonoBehaviour
{
    [Header("Configuració de la Càmera")]
    public Transform cameraMeta;

    [Header("Configuració del Moviment")]
    public float distancia = 1.2f;
    public float velocitatLerp = 3.0f;

    void Update()
    {
        if (cameraMeta == null) return;

        // 1. Calcular la posició ideal davant de la cara de l'usuari
        Vector3 posicioIdeal = cameraMeta.position + (cameraMeta.forward * distancia);

        // 2. Aplicar el LERP per moure el Canvas de forma suau des d'on està cap a la posició ideal
        transform.position = Vector3.Lerp(transform.position, posicioIdeal, velocitatLerp * Time.deltaTime);

        // 3. Fer que el Canvas miri sempre cap a l'usuari de forma suau
        Quaternion rotacioIdeal = Quaternion.LookRotation(transform.position - cameraMeta.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotacioIdeal, velocitatLerp * Time.deltaTime);
    }
}