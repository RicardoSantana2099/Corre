using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Titulo : MonoBehaviour
{
    public CanvasGroup titleCanvasGroup;  // El CanvasGroup del título
    public float fadeOutSpeed = 1f;       // Velocidad para desvanecer
    public float fadeInSpeed = 0.5f;      // Velocidad para reaparecer
    public float delayBeforeFadeIn = 10f; // Retraso antes de volver a aparecer

    private bool isHoldingKey = false;
    private float targetAlpha = 1f;

    void Update()
    {
        // Detecta si se está presionando cualquier tecla
        if (Input.anyKey)
        {
            isHoldingKey = true;
            StopAllCoroutines(); // Detiene cualquier corrutina de fade-in que esté en progreso
        }
        else
        {
            if (isHoldingKey) // Si se acaba de soltar la tecla
            {
                isHoldingKey = false;
                StartCoroutine(FadeInWithDelay());
            }
        }

        // Si se está presionando la tecla, desvanecer de inmediato
        if (isHoldingKey)
        {
            targetAlpha = 0f;
            titleCanvasGroup.alpha = Mathf.Lerp(titleCanvasGroup.alpha, targetAlpha, Time.deltaTime * fadeOutSpeed);
        }
    }

    private IEnumerator FadeInWithDelay()
    {
        // Espera antes de comenzar el fade-in
        yield return new WaitForSeconds(delayBeforeFadeIn);

        // Iniciar el fade-in
        targetAlpha = 1f;
        while (Mathf.Abs(titleCanvasGroup.alpha - targetAlpha) > 0.01f)
        {
            titleCanvasGroup.alpha = Mathf.Lerp(titleCanvasGroup.alpha, targetAlpha, Time.deltaTime * fadeInSpeed);
            yield return null; // Espera al siguiente frame
        }
    }
}
