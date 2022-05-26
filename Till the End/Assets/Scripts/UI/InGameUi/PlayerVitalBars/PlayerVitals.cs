using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerVitals : MonoBehaviour
{
    //Attach the script to player
    public static int GainHealthValue;
    public static int GainThirstValue;
    public static int GainHungerValue;

    public static bool gainingHunger = false;
    public static bool gainingThirst = false;

    [SerializeField] Slider healthSlider;
    [SerializeField] public int maxHealth;
    [SerializeField] public int healthFallRate;

    [SerializeField] Slider thirstSlider;
    [SerializeField] public int maxThirst;
    [SerializeField] public int thirstFallRate;

    [SerializeField] Slider hungerSlider;
    [SerializeField] public int maxHunger;
    [SerializeField] public int hungerFallRate;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();

        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;

        thirstSlider.maxValue = maxThirst;
        thirstSlider.value = maxThirst;

        hungerSlider.maxValue = maxHunger;
        hungerSlider.value = maxHunger;
    }

   

    private void Update()
    {
        //Health controller 
        if(hungerSlider.value <= 0 && thirstSlider.value <= 0)
        {
            healthSlider.value -= Time.deltaTime / healthFallRate * 2;
        }
        else if(hungerSlider.value <= 0 || thirstSlider.value <=0 )
        {
            healthSlider.value -= Time.deltaTime / healthFallRate;
        }

        if(healthSlider.value <= 0)
        {
            CharacterDeath();
        }

        //Hunger controller 
        if(hungerSlider.value >= 0)
        {
            hungerSlider.value -= Time.deltaTime / hungerFallRate;
        }
        else if (hungerSlider.value <= 0)
        {
            hungerSlider.value = 0;
        }
        else if(hungerSlider.value >= maxHunger)
        {
            hungerSlider.value = maxHunger;
        }

        //Thirst Controller 
        if (thirstSlider.value >= 0)
        {
            thirstSlider.value -= Time.deltaTime / thirstFallRate;
        }
        else if (thirstSlider.value <= 0)
        {
            thirstSlider.value = 0;
        }
        else if (thirstSlider.value >= maxThirst)
        {
            thirstSlider.value = maxThirst;
        }


        CheckForGain();

    }

    public  void GainHunger()
    {
        hungerSlider.value += GainHealthValue ;
    }

    public  void GainThirst()
    {
        thirstSlider.value += GainThirstValue;
    }

    void CheckForGain()
    {
        if(gainingHunger)
        {
            GainHunger();
        }

        if(gainingThirst)
        {
            GainThirst();
        }
    }

    void CharacterDeath()
    {
        //make the character die
        
       
    }

}
