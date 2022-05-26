using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PickUpItem : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] CanvasGroup pickupCanvas;
    private GameObject player;
    private float distance;
    
    void Start()
    {
        pickupCanvas.alpha = 0;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, player.transform.position);

        if(distance < 4)
        {
            //show ui
            pickupCanvas.alpha = 1;
        }
        else
        {
            //hide ui
            pickupCanvas.alpha = 0;
        }


    }

    
}
