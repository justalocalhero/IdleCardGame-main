using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceHUD : MonoBehaviour
{
    private HUDTray hudTray;
    public ResourceHUDElement prefab;
    private List<ResourceHUDElement> elements = new List<ResourceHUDElement>();

    void Awake()
    {
        hudTray = GetComponentInParent<HUDTray>();
    }

    void Start()
    {
        Engine.instance.gameBus.onChanged += UpdateUI;
    }

    void UpdateUI(PlayPackage playPackage)
    {
        List<Resource> resources = playPackage.gameBoard.resources;

        Trim(resources);
        UpdateElements(resources);
    }

    void Trim(List<Resource> resources)
    {
        for(int i = elements.Count - 1; i >= 0; i--)
        {
            
            ResourceHUDElement element = elements[i];
            bool found = false;

            foreach(Resource resource in resources)
            {
                if(resource.resourceType != element.resource.resourceType) continue;

                found = true;
                break;                
            }

            if(found) continue;

            elements.Remove(element);
            hudTray.Remove(element);
        }
    }

    void UpdateElements(List<Resource> resources)
    {
        foreach(Resource resource in resources)
        {
            ResourceHUDElement found = null;

            for(int i = 0; i < elements.Count; i++)
            {
                if(resource.resourceType != elements[i].resource.resourceType) continue;
                found = elements[i];
                break;
            }

            if(!found) 
            {
                found = Instantiate(prefab, hudTray.container);
                elements.Add(found);
                hudTray.Add(found);
            }

            found.Register(resource);
        }
    }
}
