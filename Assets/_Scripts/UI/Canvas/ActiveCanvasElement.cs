using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveCanvasElement : MonoBehaviour
{
    public CanvasElement activeCanvasElement {get; set;}

    void Start()
    {
        activeCanvasElement.Activate();
    }
}
