using UnityEngine;

public class InteractableAnimation : MonoBehaviour
{
    [Header("Rotación")]
    [SerializeField] private float rotationSpeed = 50f;

    [Header("Flotación")]
    [SerializeField] private float amplitude = 0.5f; // Qué tan arriba/abajo llega
    [SerializeField] private float frequency = 1f;   // Qué tan rápido oscila

    private Vector3 startPosition;

    void Start()
    {
        // Guardamos la posición inicial para que el objeto oscile respecto a ella
        startPosition = transform.position;
    }

    void Update()
    {
        // 1. Rotación (Multiplicamos por el valor actual para que sea constante)
        transform.eulerAngles += Vector3.up * Time.deltaTime * rotationSpeed;

        // 2. Movimiento de subida y bajada (Levitación)
        float newY = Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = startPosition + new Vector3(0, newY, 0);
    }
}

