using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AperturaMolinete : MonoBehaviour
{
    [SerializeField] private Animator molinetedoor; // Animador del molinete
    [SerializeField] private float restaSaldo; // Cantidad de saldo que se resta al pasar
    [SerializeField] private AudioClip sonidoAbrir; // Sonido al abrir
    [SerializeField] private AudioClip sonidoCerrar; // Sonido al cerrar
    [SerializeField] private AudioSource audioSource; // Fuente de audio
    private bool haPasado = false; // Controla si el jugador ya pasó

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !haPasado)
        {
            MainCharacter mainCharacter = other.GetComponent<MainCharacter>();

            if (mainCharacter != null && mainCharacter.saldoSube > 300)
            {
                haPasado = true; // Marca que el jugador ha pasado
                mainCharacter.saldoSube -= restaSaldo; // Resta el saldo
                Opendoor();
            }
            else
            {
                DenegarAcceso(); // No permite el paso si no tiene suficiente saldo
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && haPasado)
        {
            Closedoor(); // Cierra el molinete al salir
        }
    }

    private void Opendoor()
    {
        molinetedoor.SetBool("Abrir", true); // Abre el molinete
        if (audioSource != null && sonidoAbrir != null)
        {
            audioSource.PlayOneShot(sonidoAbrir); // Reproduce el sonido de apertura
        }
    }

    private void Closedoor()
    {
        molinetedoor.SetBool("Abrir", false); // Cierra el molinete
        if (audioSource != null && sonidoCerrar != null)
        {
            audioSource.PlayOneShot(sonidoCerrar); // Reproduce el sonido de cierre
        }
    }

    private void DenegarAcceso()
    {
        if (audioSource != null)
        {
            // Opcional: puedes agregar un sonido para denegar el acceso si lo tienes
            Debug.Log("Acceso denegado: saldo insuficiente");
        }
    }
}