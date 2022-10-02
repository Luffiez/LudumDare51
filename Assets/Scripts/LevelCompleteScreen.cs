using LDJAM51.UI;
using System;
using TMPro;
using UnityEngine;

public class LevelCompleteScreen : MonoBehaviour
{
    [SerializeField] GameObject window;
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
        FindObjectOfType<EffectHandler>().enabled = false;
        window.SetActive(true);
        //scaler.SetTargetScale(1, 5);
        SoundManager.instance.PlaySfx("Win");
        var diff = DateTime.Now - startTime;
        string text = $"Time taken:";
        if (diff.Minutes > 0)
            text += $"{diff.Minutes} minute(s) and";

        text += $" {diff.Seconds} seconds";
        text += $"\nFlips performed: {flips}";

        content.text = text;
    }
}
