using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatAndDrink : MonoBehaviour
{

    // Start is called before the first frame update

    private GameObject[] Eatable;
    private GameObject[] Drinkable;

    private float[] distanceToFood;
    private float[] distanceToWater;

    void Start()
    {
        Eatable = GameObject.FindGameObjectsWithTag("Food");
        Drinkable = GameObject.FindGameObjectsWithTag("Water");
       
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i<=Eatable.Length; i++)
        {
            distanceToFood[i] = Vector3.Distance(transform.position, Eatable[i].transform.position );
            

            if (distanceToFood[i] < 4)
            {
               if(Input.GetKey(KeyCode.E))
                {
                    Destroy(Eatable[i]);
                    Debug.Log("Ate");
                   
                }
            }
            
        
        }

        for (int i = 0; i <= Drinkable.Length; i++)
        {
            distanceToWater[i] = Vector3.Distance(transform.position, Drinkable[i].transform.position);


            if (distanceToWater[i] < 4)
            {
                if (Input.GetKey(KeyCode.E))
                {
                    Destroy(Drinkable[i]);
                    Debug.Log("Drunk");

                }
            }


        }



    }
}
