using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateOverlays : MonoBehaviour
{
    public List<GameObject> prefabs;
    public Transform container;
    
    void Start()
    {
        foreach(GameObject go in prefabs)
        {
            Instantiate(go, container);
        }
    }
}
