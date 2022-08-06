using UnityEngine;

public class DefaultCanvasElement : MonoBehaviour
{
    void Start()
    {
        GetComponent<CanvasElement>().Activate();
    }
}