using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDTray : MonoBehaviour
{
    public Transform container;
    private List<HUDElement> elements = new List<HUDElement>();

    public void Add(HUDElement element)
    {
        if(elements.Contains(element)) return;
        elements.Add(element);

        Reorder();
    }

    public void Remove(HUDElement element)
    {
        if(!elements.Contains(element)) return;
        elements.Remove(element);

        Destroy(element.gameObject);

        Reorder();
    }

    public void Reorder()
    {
        List<HUDElement> ordered = new List<HUDElement>();

        foreach(HUDElement element in elements)
        {
            bool found = false;
            for(int i = 0; i < ordered.Count; i++)
            {
                if(ordered[i].priority 
                    <= element.priority) continue;

                found = true;
                ordered.Insert(i, element);

                break;
            }

            if(!found) ordered.Add(element);
        }

        elements = ordered;
        for(int i = 0; i < elements.Count; i++)
        {
            elements[i].transform.SetSiblingIndex(i);
        }
    }
}