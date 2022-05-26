using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="falseGate")
        {
            SceneManager.LoadScene("desert");
        }

        if (collision.gameObject.tag == "trueGate")
        {
            SceneManager.LoadScene("Moksh");
        }

    }
}
