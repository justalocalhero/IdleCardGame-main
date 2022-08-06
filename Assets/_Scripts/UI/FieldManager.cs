using UnityEngine;
using System.Collections.Generic;

public class FieldManager : MonoBehaviour
{
    public Transform container;
    public FieldUI prefab;

    private List<FieldUI> fields = new List<FieldUI>();

    void Start()
    {
        Engine.instance.gameBus.onBeginGame += OnBeginGame;
        Engine.instance.gameBus.onFieldsChanged += HandleUpdate;
    }

    void OnBeginGame(PlayPackage playPackage)
    {
        foreach(FieldUI field in fields)
        {
            Destroy(field.gameObject);
        }

        fields = new List<FieldUI>();

        
        for(int i = 0; i < playPackage.gameBoard.startingFields; i++)
        {
            AddField(playPackage);
        }
    }

    void HandleUpdate(PlayPackage playPackage)
    {
        int delta = playPackage.gameBoard.startingFields - fields.Count;

        for(int i = 0; i < delta; i++)
        {
            AddField(playPackage);
        }

        for(int i = 0; i < -delta; i++)
        {
            RemoveField(playPackage);
        }
    }

    private void AddField(PlayPackage playPackage)
    {
        FieldUI field = Instantiate(prefab, container);
        playPackage.gameBoard.fields.Add(field.Value);
        fields.Add(field);
    }

    private void RemoveField(PlayPackage playPackage)
    {
        if(fields.Count == 0) return;
        
        FieldUI field = fields[fields.Count - 1];
        field?.Value?.Permanent?.Remove(playPackage);
        if(playPackage.gameBoard.fields.Contains(field.Value)) playPackage.gameBoard.fields.Remove(field.Value);   
        fields.RemoveAt(fields.Count - 1);
        Destroy(field.gameObject);
    }
}