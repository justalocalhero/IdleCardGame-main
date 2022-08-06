using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndTurn : MonoBehaviour
{
    void Awake()
    {
        GetComponentInChildren<Button>().onClick.AddListener(Fire);
    }
    
    void Fire()
    {
        Engine.instance.EndTurn();
    }
}
