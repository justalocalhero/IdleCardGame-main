using UnityEngine;

public class PromptManager : MonoBehaviour
{
    public Transform container;
    public SearchPrompt prefab;
    private SearchPrompt searchPrompt;
    
    void Start()
    {
        Engine.instance.gameBus.onPrompt += Prompt;
        searchPrompt = Instantiate(prefab, container);
        searchPrompt.gameObject.SetActive(false);
    }

    void Prompt(PlayPackage playPackage, IPrompt prompt)
    {
        if(prompt is IPrompt<Card>)
        {
            IPrompt<Card> sc = prompt as IPrompt<Card>;
            searchPrompt.Register(sc);
        }
    }
}
