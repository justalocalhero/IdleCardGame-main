using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateCanvasElementntOnEndGame : MonoBehaviour
{
    public CanvasElement canvasElement;

    void Start()
    {
        Engine.instance.gameBus.onEndGame += (PlayPackage playPackage) => canvasElement.Activate();
    }
}
