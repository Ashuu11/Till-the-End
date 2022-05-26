using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastCam : MonoBehaviour
{
    [SerializeField] private float raycastDistance;
    [SerializeField] int GainedHunger;
    [SerializeField] int GainedThirst;
    


    private void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.E))
        {
            HandleRaycast();
        }

    }

    private void HandleRaycast()
    {
        RaycastHit whatIHit;

        if (Physics.Raycast(transform.position, transform.forward, out whatIHit, raycastDistance))
        {
            Debug.Log(whatIHit.transform.gameObject.name);
            if(whatIHit.transform.gameObject.tag == "Food" )
            {
                Destroy(whatIHit.transform.gameObject);
                PlayerVitals.GainHungerValue = GainedHunger;
                PlayerVitals.gainingHunger = true;
                PlayerVitals.gainingThirst = false;
                Debug.Log("Ate");
            }

            else if (whatIHit.transform.gameObject.tag == "Water")
            {
                Destroy(whatIHit.transform.gameObject);
                PlayerVitals.GainThirstValue = GainedThirst;
                PlayerVitals.gainingThirst = true;
                PlayerVitals.gainingHunger = false;
                Debug.Log("Drunk");
            }
            else
            {
                PlayerVitals.gainingThirst = false;
                PlayerVitals.gainingHunger = false;
            }



        }
    }




}







