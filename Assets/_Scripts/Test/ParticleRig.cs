using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleRig : MonoBehaviour
{
    public List<GameObject> prefabs;

    void Start()
    {
        foreach(GameObject prefab in prefabs)
        {
            Instantiate(prefab, transform);
        }
    }
}
