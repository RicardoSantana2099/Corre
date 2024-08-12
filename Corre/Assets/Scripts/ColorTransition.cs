using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorTransition : MonoBehaviour
{
   
    public GameObject colorScenario;          // El escenario a color
    public GameObject blackWhiteScenario;     // El escenario en blanco y negro
    public float transitionSpeed = 1f;        // Velocidad de la transición

    private Renderer[] colorRenderers;
    private Renderer[] bwRenderers;
    private float transitionProgress = 0f;
    private bool isHoldingKey = false;

    private void Start()
    {
        // Obtener todos los renderers de los escenarios
        colorRenderers = colorScenario.GetComponentsInChildren<Renderer>();
        bwRenderers = blackWhiteScenario.GetComponentsInChildren<Renderer>();

        // Inicialmente, el escenario a color está totalmente visible, el B&W está invisible
        SetRenderersAlpha(colorRenderers, 1f);
        SetRenderersAlpha(bwRenderers, 0f);
    }

    private void Update()
    {
        // Detecta si se está presionando cualquier tecla
        if (Input.anyKey)
        {
            isHoldingKey = true;
        }
        else
        {
            isHoldingKey = false;
        }

        // Actualizar el progreso de la transición
        if (isHoldingKey)
        {
            transitionProgress = Mathf.Clamp01(transitionProgress + Time.deltaTime * transitionSpeed);
        }
        else
        {
            transitionProgress = Mathf.Clamp01(transitionProgress - Time.deltaTime * transitionSpeed);
        }

        // Ajustar la visibilidad de los renderers
        SetRenderersAlpha(colorRenderers, 1f - transitionProgress);
        SetRenderersAlpha(bwRenderers, transitionProgress);
    }

    // Método para ajustar el alpha de todos los renderers
    private void SetRenderersAlpha(Renderer[] renderers, float alpha)
    {
        foreach (var renderer in renderers)
        {
            foreach (var mat in renderer.materials)
            {
                Color color = mat.color;
                color.a = alpha;
                mat.color = color;
            }
        }
    }
}
