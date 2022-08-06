using System.Text;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public delegate void OnCoinsSet(int count);
    public OnCoinsSet onCoinsSet;

    public delegate void OnCoinsChanged(int count);
    public OnCoinsChanged onCoinsChanged;

    private int _Coins;
    public int Coins 
    { 
        get => _Coins;
        set 
        {
            int old = _Coins;
            int dif = value - _Coins;
            _Coins = value;

            if(dif != 0 && onCoinsSet != null) onCoinsSet(value);
            if(dif != 0 && onCoinsChanged != null) onCoinsChanged(dif);
        }
    }

    public static ResourceManager instance;

    void Awake()
    {
        if(instance == null) instance = this;
        else
        {
            Destroy(this);
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            Coins += 9;
        }
    }

    public string GetDescription()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine("Coins: " + Coins);

        return sb.ToString();
    }
}
