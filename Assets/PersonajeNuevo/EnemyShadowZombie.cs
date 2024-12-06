using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShadowZombie : MonoBehaviour
{
    [SerializeField] private float visionRange;
    [SerializeField] private float attackRange;
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float timeWaiting;
    private float timeWaitingInit;
    [SerializeField] private float timeWatching;
    private float timeWatchingInit;
    [SerializeField] private Vector3[] waypoints;
    [SerializeField] private Animator myAnimator;
    [SerializeField] private GameObject particleEffect;
    
    private int index;
    private MainCharacter player;



    void Start()
    {
        player = FindObjectOfType<MainCharacter>();
        myAnimator.SetBool("Walking", true);
        timeWaitingInit = timeWaiting;
        timeWatchingInit = timeWatching;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance < visionRange)
        {
            if (timeWatching <= 0)
            {
                //perseguimos
                LookToPlayer();
                if (distance > attackRange)
                {

                    transform.position += transform.forward * speed * Time.deltaTime;
                    myAnimator.SetBool("Walking", true);
                }
                else
                {
                    myAnimator.SetBool("Walking", false);
                    myAnimator.SetBool("Attacking", true);
                }
            }
            else
            {
                myAnimator.SetBool("Walking", false);
                LookToPlayer();
                timeWatching -= Time.deltaTime;
            }
        }
        else
        {
            timeWatching = timeWatchingInit;
            myAnimator.SetBool("Walking", true);
            float distanceToPoint = Vector3.Distance(waypoints[index], transform.position);
            if (distanceToPoint >= 1.5f)
            {
                LookToPosition();
                transform.position += transform.forward * speed * Time.deltaTime;
            }
            else
            {
                if (timeWaiting <= 0)
                {
                    index++;
                    if (index >= waypoints.Length)
                    {
                        index = 0;
                    }
                    timeWaiting = timeWaitingInit;
                }
                else
                {
                    myAnimator.SetBool("Walking", false);
                    timeWaiting -= Time.deltaTime;
                }
            }
        }
    }

    private void LookToPosition()
    {
        Vector3 direction = waypoints[index] - transform.position;
        direction.y = 0;
        var newRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * rotationSpeed);
    }
    private void LookToPlayer()
    {
        Vector3 direction = player.transform.position - transform.position;
        direction.y = 0;
        var newRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * rotationSpeed);
    }

    public void AttackZombie()
    {
        Quaternion rotation=Quaternion.Euler(-90,0,0);
 
        Instantiate(particleEffect,transform.position, rotation);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, visionRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.green;
        foreach (var waypoint in waypoints)
        {
            Gizmos.DrawSphere(waypoint, 0.2f);
        }
    }
}
