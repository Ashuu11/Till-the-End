using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class daynightcycle : MonoBehaviour
{

    [SerializeField] private Light sun;
    [SerializeField] private float secondsInFullDay = 120f;
    [Range(0, 1)] [SerializeField] private float currentTimeofDay = 0;
    private float timeMultiplier = 1f;
    private float sunInitialIntensity;
    [SerializeField] GameObject loseCanvas;

    void Start()
    {
        sunInitialIntensity = sun.intensity;
        Time.timeScale = 1;
    }

    void Update()
    {
        UpdateSun();
        currentTimeofDay += (Time.deltaTime / secondsInFullDay) * timeMultiplier;
        if (currentTimeofDay >= 1)
        {
            currentTimeofDay = 0;
            Debug.Log("Day complete");
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None; //cursor locked and hidden 
            Cursor.visible = true;
            loseCanvas.SetActive(true);
            
        }
    }

    void UpdateSun()
    {
        sun.transform.localRotation = Quaternion.Euler((currentTimeofDay * 360f) - 90, 170, 0);
        float intensityMultiplier = 1;
        if (currentTimeofDay <= .23f || currentTimeofDay >= .75f)
        {
            intensityMultiplier = 0.1f;
        }

        else if (currentTimeofDay <= .25f)
        {
            intensityMultiplier = Mathf.Clamp01((currentTimeofDay - 0.23f) * (1 / 0.02f));
        }
        else if (currentTimeofDay <= .73f)
        {
            intensityMultiplier = Mathf.Clamp01(1 - ((currentTimeofDay - 0.73f) * (1 / 0.02f)));
        }
        sun.intensity = sunInitialIntensity * intensityMultiplier;
    }
}
 

