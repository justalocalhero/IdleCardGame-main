using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivateCanvasElementOnButtonClick : MonoBehaviour
{
    public CanvasElement canvasElement;
    Button button;    

    void Start()
    {
        button = GetComponentInChildren<Button>();
        button.onClick.AddListener(canvasElement.Activate);
    }
}
