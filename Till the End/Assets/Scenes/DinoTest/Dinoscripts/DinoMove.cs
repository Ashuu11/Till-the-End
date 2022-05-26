using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DinoMove : MonoBehaviour
{
    enum States
    {
        walk,
        chase
    }
    [SerializeField] States currentStage;
    [SerializeField] Transform[] movePositions;
    NavMeshAgent nav_agent;
    Transform random_position;
    Transform Player;
    [SerializeField] float walkspeed, chasespeed;
    float distance;
    bool seen, walking;
    bool damage, canAttack;
    [SerializeField] GameObject dinoCanvas=null;
  //  Animator anim;

    int damagePoints;
    // animation needed chase,walk,attack,idle
    // Start is called before the first frame update
    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    void Start()
    {
        nav_agent = GetComponent<NavMeshAgent>();
        random_position = movePositions[Random.Range(0, movePositions.Length)];
        seen = false;
      //  anim = GetComponent<Animator>();
        damage = false;
        canAttack = false;
        if (currentStage == States.walk)
            nav_agent.SetDestination(random_position.position);
        Time.timeScale = 1;

    }

    // Update is called once per frame
    void Update()
    {
        if (seen == false)
        {
            nav_agent.speed = walkspeed;
             // nav_agent.SetDestination(random_position.position);
          // Debug.Log("Destination is" + random_position);
            
                
        }
        if(currentStage==States.walk)
        {
            nav_agent.SetDestination(random_position.position);
            if (!nav_agent.pathPending && nav_agent.remainingDistance <= nav_agent.stoppingDistance)
                nextDes();
        }
        if(currentStage==States.chase)
        {
            nav_agent.SetDestination(Player.position);
            Debug.Log("Mode is chase");
        }

        distance = Vector3.Distance(transform.position, Player.transform.position);
       // StartCoroutine(waitDino());
        damageSystem();
        animationController();
        detectPlayer();
        chase();
    }
    void nextDes()
    {
        random_position = movePositions[Random.Range(0, movePositions.Length)];
    }
    //this method will control animations untill player is seen
    void animationController()
    {
        if (seen == false)
        {
            if (nav_agent.remainingDistance > nav_agent.stoppingDistance)
                walking = true;
            if (nav_agent.remainingDistance < nav_agent.stoppingDistance)
                walking = false;
          //  anim.SetBool("walk", walking);
        }

    }
    //line of sight of Dino
    void detectPlayer()
    {
        var direction = Player.transform.position - transform.position;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, 100))
        {
            if (hit.transform.gameObject.tag == "Player" && distance<100)
            {
                currentStage = States.chase;
                seen = true;
                Debug.Log("seen");
            }
           
            else
            {
                seen = false;
                currentStage = States.walk;
            }

        }
    }
    //chase scene will be called whenever player is seen by Dino
    void chase()
    {
        if (seen == true && distance > 10)
        {
            nav_agent.speed = chasespeed;
            //nav_agent.Stop();
            Debug.Log("stopped");
          //  nav_agent.ResetPath();
           // random_position = Player.position;
            //nav_agent.SetDestination(Player.transform.position);
            Debug.Log("Destination is player");
            // anim.SetBool("chase", true);
        }
        if (seen == true && distance < 10)
        {
          //  anim.SetBool("chase", false);
            canAttack = true;
          //  anim.SetBool("attack", true);

        }
        else
        {
           // anim.SetBool("chase", false);
           // anim.SetBool("attack", false);
        }

    }

    //after every position Dino will wait for some time before going to next position
    IEnumerator waitDino()
    {
        if (seen == false)
        {
            if (nav_agent.remainingDistance < nav_agent.stoppingDistance)
            {
                yield return new WaitForSeconds(8);
                random_position = movePositions[Random.Range(0, movePositions.Length)];
            }

        }

    }
    void damageSystem()
    {
        if (damage == true)
        {
           // Player.SendMessage("getDamage", damagePoints);
            damage = false;
            Cursor.lockState = CursorLockMode.None; //cursor locked and hidden 
            Cursor.visible = true;
            dinoCanvas.SetActive(true);
            Time.timeScale = 0;

        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
            damage = true;
    }
}

