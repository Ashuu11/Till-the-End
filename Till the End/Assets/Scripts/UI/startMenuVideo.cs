using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startMenuVideo : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject canvas1;
    float count;
    bool pressed;
    void Start()
    {
        count = 0;
        pressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        count += Time.deltaTime;
        if(count >=10 && pressed==false)
        {
            canvas1.SetActive(true);
        }
        if(Input.anyKey)
        {
            canvas.SetActive(true);
            canvas1.SetActive(false);
            pressed = true;
        }
    }
   
}
