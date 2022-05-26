using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageColorChange : MonoBehaviour
{
    public Image Img;
    public Gradient myGradient;
    public float strobeDuration = 2f;

    public void Start()
    {
        Img = GetComponent<Image>();
    }

    public void Update()
    {
        float t = Mathf.PingPong(Time.time / strobeDuration, 1f);
        Camera.main.backgroundColor = myGradient.Evaluate(t);
        Img.color = myGradient.Evaluate(t);
    }
}
