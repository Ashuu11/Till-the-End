using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class vfxScene : MonoBehaviour
{
    // Start is called before the first frame update
    float counter;

    void Start()
    {
        counter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;
        if(counter>=10)
        {
            SceneManager.LoadScene("final");
        }
    }
}
