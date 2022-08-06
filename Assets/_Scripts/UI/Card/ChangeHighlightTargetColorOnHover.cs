using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChangeHighlightTargetColorOnHover : MonoBehaviour
{
    public Color defaultColor, highlightColor;
    Image highlightImage;
    TargetObject targetObject;

    void Awake()
    {
        highlightImage = GetComponentInChildren<Image>();

        targetObject = GetComponentInParent<TargetObject>();

        targetObject.onEnter += OnEnter;
        targetObject.onExit += OnExit;
        targetObject.onDropped += OnDrop;
    }

    void OnDestroy()
    {
        targetObject.onEnter -= OnEnter;
        targetObject.onExit -= OnExit;
    }

    public void OnEnter()
    {
        highlightImage.color = highlightColor;
    }

    public void OnExit()
    {
        highlightImage.color = defaultColor;
    }

    public void OnDrop()
    {
        highlightImage.color = defaultColor;
    }
}