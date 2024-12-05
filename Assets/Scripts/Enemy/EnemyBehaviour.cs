using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemyState
{
    public EnemyStates currentState;
}

public enum EnemyStates
{
    Atacking = 0,
    Fleeing = 1,
    Pursuit = 2,
    Stay = 3,
    Dead = 4,
    Confused = 5,
    LookAtPlayer
}
public class EnemyBehaviour : MonoBehaviour
{
    //private bool isPlayerInSight;
    //private float currentHealt;
    //private bool isConfused;

    [SerializeField] private EnemyStates startingtState;
    [SerializeField] private MainCharacter player;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float moveRandomSpeed;
    [SerializeField] private float pursuitThreshold;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Animator characterAnimator;
    [SerializeField] private float damage;
    [SerializeField] private Transform[] puntosMov;
    [SerializeField] private float distminima;
    private int siguientepaso =0;
    private SpriteRenderer spriteRenderer;




    void Update()
    {
        CheckStateUpdate();
        //Todo
        switch (startingtState)
        {
            case EnemyStates.Atacking:
                Atacking();
                break;
            case EnemyStates.Fleeing:
                Flee();
                break;
            case EnemyStates.Pursuit:
                Pursuit();
                break;
            case EnemyStates.Stay:
                Stay();
                break;
            case EnemyStates.LookAtPlayer:
                LookRotationQuaternion();
                break;
            case EnemyStates.Confused:
                RandomMovement();
                break;
            default:
                Stay();
                break;
        }
    }

    //Modo mas simple de mirar al jugador 
    private void LookAtPlayer()
    {
        transform.LookAt(player.transform.position);
    }

    //Modo alternativo para mirar al jugador
    private void LookRotationQuaternion()
    {
        var newRotation = Quaternion.LookRotation(player.transform.position - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * rotationSpeed);
    }

    //Se checkea el estado del enemigo
    private void CheckStateUpdate()
    {
        //Si el player esta muy lejos, me quedo quieto.
        var diff = transform.position - player.transform.position;
        var distance = diff.magnitude;

        if (distance > pursuitThreshold)
        {
            if (startingtState == EnemyStates.Pursuit)
            {
                startingtState = EnemyStates.Stay;
            }
        }
        else
        {
            startingtState = EnemyStates.Pursuit;
            
        }
    }

    //El enemigo esta quieto
    private void Stay()
    {
        Idle();
    }

    //El enemigo gira hacia el jugador y lo persigue
    private void Pursuit()
    {
        LookRotationQuaternion();

        transform.position += transform.forward * (Time.deltaTime * movementSpeed);
        StartRuning();
    }

    //El enemigo se aleja del jugador
    public void Flee()
    {
        Vector3 a = player.transform.position;
        Vector3 b = transform.position;
        Vector3 diff = (b - a).normalized;


        transform.position += diff * (Time.deltaTime * movementSpeed);
    }
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();  
    }

    public void RandomMovement()
    {
        characterAnimator.SetBool("isRunning", true);

        if (puntosMov == null || puntosMov.Length == 0) return;

        Vector3 targetPosition = puntosMov[siguientepaso].position;
        Vector3 direction = (targetPosition - transform.position).normalized;

        
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }

        
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveRandomSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < distminima)
        {
            siguientepaso = (siguientepaso + 1) % puntosMov.Length;
        }
    }
   



    //    if (puntosMov == null || puntosMov.Length == 0) return;

    //    transform.position = Vector3.MoveTowards(transform.position, puntosMov[siguientepaso].position, moveRandomSpeed * Time.deltaTime);

    //    if (Vector3.Distance(transform.position, puntosMov[siguientepaso].position) < distminima)
    //    {
    //        siguientepaso = (siguientepaso + 1) % puntosMov.Length;
    //    }
    //}
    //public void WalkRandom()
    //{

    //    characterAnimator.SetBool("isRunning", true);
    //    transform.position = Vector3.MoveTowards(transform.position, puntosMov[siguientepaso].position, movementSpeed * Time.deltaTime);

    //    if (Vector3.Distance(transform.position, puntosMov[siguientepaso].position) < distminima)
    //    {
    //        siguientepaso += 1;
    //        if (siguientepaso >= puntosMov.Length)
    //        {
    //            siguientepaso = 0;
    //        }
    //    }
    //}

    private void StartRuning()
    {
        characterAnimator.SetBool("isRunning", true);
    }
    private void Idle()
    {
        characterAnimator.SetBool("isRunning", false);
    }

    private void Atacking()
    {
        characterAnimator.SetBool("isAtacking", true);
    }


    private void OnCollisionStay(Collision other)
    {
        var colliderGameObject = other.gameObject;
        //Necesito chequear la tag/label/etiqueta de el gameobject

        MainCharacter player = colliderGameObject.GetComponent<MainCharacter>();

        if (player != null) //Tiene el componente player
        {
            //Es un player
            Debug.Log("Choco contra el player");
            Atacking();
            player.TakeDamage(damage * Time.fixedDeltaTime);
        }
    }
}
