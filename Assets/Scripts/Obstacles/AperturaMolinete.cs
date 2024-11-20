using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Molinete : MonoBehaviour
{
    [SerializeField] private Animator molinetedoor;
    [SerializeField] private float restaSaldo;
    [SerializeField] private AudioClip sonidopuerta;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float initialTemp;

    public void Opendoor()
    {
        molinetedoor.SetBool("Abrir", true);
    }

    [ContextMenu("Cerrarmolinete")]
    public void Closedoor()
    {
        molinetedoor.SetBool("Abrir", false);
    }

    private void OnTriggerEnter(Collider other)
    {
        ApoyaSube(other.gameObject);
    }

    private void ApoyaSube(GameObject target)
    {
        MainCharacter mainCharacter = target.GetComponent<MainCharacter>();
        if (mainCharacter != null)
        {
            if(mainCharacter.saldoSube > 300)
            {
                mainCharacter.ApoyaSube(restaSaldo * Time.fixedDeltaTime);
                Opendoor();
                Abresonido();
                Destroy(gameObject);
            }
        }
    }
    public void Abresonido()
    {
        audioSource.PlayOneShot(sonidopuerta);
    }

    private float currentTime;

    private void Awake()
    {
        currentTime = initialTemp;
    }

    private void Update()
    {
        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        Closedoor();
    }
}