using UnityEngine;
using TMPro;

public class SingletonTooltip : MonoBehaviour
{
    TextMeshProUGUI text;
    private string message;
    private Vector3 position;

    public static SingletonTooltip instance;

    void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();

        if(instance == null) instance = this;
        else Destroy(this);

        gameObject.SetActive(false);
    }

    public void Push(string message, Vector3 pos)
    {
        gameObject.SetActive(true);
        text.SetText(message);
        
        transform.position = pos;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}