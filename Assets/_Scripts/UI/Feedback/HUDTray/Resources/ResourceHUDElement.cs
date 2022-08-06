using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceHUDElement : HUDElement
{
    public TextMeshProUGUI text;
    public Resource resource;

    public void Register(Resource resource)
    {
        this.resource = resource;
        
        UpdateUI(resource);
    }

    void UpdateUI(Resource resource)
    {
        text.SetText(resource.resourceType.ToString() + "\n" + resource.count);
    }
}
