using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class PushHoveredLinkToTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    private bool active = false;
    private TextMeshProUGUI text;
    private Camera mainCamera;
    public float timer;
    private float minTime;

    private int linkIndex = -1;

    void Awake()
    {
        mainCamera = Camera.main;
        text = GetComponentInChildren<CardDescription>().GetComponentInChildren<TextMeshProUGUI>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Deactivate();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(eventData.dragging) return;
        Activate();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Deactivate();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(eventData.dragging) return;
        Activate();
    }

    private void Activate()
    {
        active = true;
        minTime = Time.time + timer;
        linkIndex = -1;
    }

    private void Deactivate()
    {
        active = false;

        if(linkIndex != -1)
        {
            SingletonTooltip.instance.Hide();
            return;
        };
    }

    void Update()
    {
        if(!active) return;
        if(Time.time < minTime) return;
        
        int index = TMP_TextUtilities.FindIntersectingLink(text, Input.mousePosition, mainCamera);

        if(index == linkIndex) return;

        linkIndex = index;

        if(linkIndex == -1)
        {
            SingletonTooltip.instance.Hide();
            return;
        };

        TMP_TextInfo textInfo = text.textInfo;
        TMP_LinkInfo linkInfo = textInfo.linkInfo[linkIndex];
        TMP_CharacterInfo charInfo = textInfo.characterInfo[linkInfo.linkTextfirstCharacterIndex + linkInfo.linkTextLength - 1];
        Vector2 charPos = new Vector2(charInfo.topRight.x, charInfo.descender);

        string id = linkInfo.GetLinkID();

        Vector2 pos = transform.TransformPoint(charPos);
        
        SingletonTooltip.instance.Push(KeywordUtility.Description(id), pos);

    }
}

public static class LinkUtility
{
    public static string GetLink(string content, string tag)
    {
        return ("<link=" + tag + "><size=+2><color=#d4c633>" + content + "</color></size></link>");
    }
}

public enum Keyword
{
    Gain,
    Pay,
    Advance,
    Incinerate,
    Expand
}

public static class KeywordUtility
{
    public static string Description(string keyString)
    {
        Keyword key = (Keyword)System.Enum.Parse(typeof(Keyword), keyString, true);

        switch(key)
        {
            case Keyword.Gain:
                return "Gain Stuff";
            case Keyword.Pay:
                return "Pay Stuff";
            case Keyword.Advance:
                return "Advance the Energy Meter by One Turn";
            default:
                return "";
        }
    }
}

public static class KeywordExtensions
{
    public static string Link(this Keyword keyword)
    {
        return LinkUtility.GetLink(keyword.ToString(), keyword.ToString());
    }
}