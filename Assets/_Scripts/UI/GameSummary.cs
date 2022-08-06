using UnityEngine;
using TMPro;

public class GameSummary : MonoBehaviour
{
    public Engine engine;
    public TextMeshProUGUI textMesh;
    private GameBus gameBus;

    void Start()
    {
        gameBus = engine.gameBus;

        gameBus.onChanged += UpdateText;
    }

    void UpdateText(PlayPackage playPackage)
    {
        textMesh.SetText(engine.gameBoard.Summary());
    }
}