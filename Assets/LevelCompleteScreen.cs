using LDJAM51.UI;
using System;
using TMPro;
using UnityEngine;

public class LevelCompleteScreen : MonoBehaviour
{
    [SerializeField] AudioClip winClip;
    [SerializeField] TMP_Text content;

    ElementScaler scaler;

    DateTime startTime;
    void Start()
    {
        startTime = DateTime.Now;
        scaler = GetComponentInChildren<ElementScaler>();
    }

    internal void Show(int flips)
    {
        scaler.SetTargetScale(1, 5);
        SoundManager.instance.PlaySfx(winClip);
        var diff = DateTime.Now - startTime;
        string text = $"Time taken:";
        if (diff.Minutes > 0)
            text += $"{diff.Minutes} + minues and";

        text += $" {diff.Seconds} seconds";
        text += $"\nFlips performed: {flips}";

        content.text = text;
    }
}
