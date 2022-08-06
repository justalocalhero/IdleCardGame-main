using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasElement : MonoBehaviour
{
    Canvas canvas;
    ActiveCanvasElement activeCanvas;
    
    void Awake()
    {
        canvas = GetComponentInChildren<Canvas>();
        activeCanvas = GetComponentInParent<ActiveCanvasElement>();
    }

    void Start()
    {
        if(this != activeCanvas.activeCanvasElement) canvas.enabled = false;
    }

    public void Activate()
    {
        if(activeCanvas.activeCanvasElement != null) activeCanvas.activeCanvasElement.Deactivate();
        canvas.enabled = true;
        activeCanvas.activeCanvasElement = this;
    }

    public void Deactivate()
    {
        canvas.enabled = false;
    }
}
