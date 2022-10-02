using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTextManager : MonoBehaviour
{
    [SerializeField] Transform text;
    [SerializeField] Vector2 offset;

    Vector2 startPos;

    private void Start()
    {
        startPos = text.transform.localPosition;
    }

    public void OnClick()
    {
        text.transform.localPosition = startPos + offset;
    }

    public void OnRelease()
    {
        text.transform.localPosition = startPos;
    }
}
