using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyGlobe : MonoBehaviour
{
    public Image image;
    public float speed, dif;
    bool active;
    float target;
    
    void Start()
    {
        Engine.instance.gameBus.onChanged += UpdateUI;
    }

    private void UpdateUI(PlayPackage playPackage)
    {
        active = true;
        float ratio = (float) playPackage.gameBoard.energy / playPackage.gameBoard.maxEnergy;

        target = Mathf.Clamp01(ratio);        
        TryClamp();
    }

    void TryClamp()
    {
        float currentDif = Mathf.Abs(image.fillAmount - target);

        if(currentDif < dif) 
        {
            image.fillAmount = Mathf.Clamp01(target);
            active = false;
        }
    }

    void Update()
    {
        if(!active) return;

        Vector3 result = Vector3.Lerp(new Vector3(image.fillAmount, 0, 0), new Vector3(target, 0, 0), Time.deltaTime * speed);

        image.fillAmount = Mathf.Clamp01(result.x);

        TryClamp();
    }
}
