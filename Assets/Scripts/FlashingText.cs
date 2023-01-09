using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FlashingText : MonoBehaviour
{
    private TextMeshProUGUI text;
    [SerializeField] private float value;
    [SerializeField] private float rateOfChange;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        value += rateOfChange;
        float sinValue = Mathf.Sin(value);
        float alpha = Mathf.Clamp01(0.2f + Mathf.Abs(sinValue));
        Color color = new Color(text.color.r, text.color.g, text.color.b, alpha);
        text.color = color;
    }
}
