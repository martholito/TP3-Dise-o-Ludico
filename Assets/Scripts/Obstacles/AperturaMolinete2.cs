using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Molinete2 : MonoBehaviour
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
            if (mainCharacter.saldoSube > 300)  // Asegúrate de que la variable 'saldo' es la correcta
            {
                mainCharacter.ApoyaSube(restaSaldo * Time.fixedDeltaTime);
                OpenAndCloseDoor();
            }
        }
    }

    public void Abresonido()
    {
        audioSource.PlayOneShot(sonidopuerta);
    }

    private void OpenAndCloseDoor()
    {
        Opendoor();
        Abresonido();
        StartCoroutine(CloseDoorAfterDelay(5f));  // Tiempo de espera antes de cerrar la puerta
    }

    private IEnumerator CloseDoorAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Closedoor();
        Destroy(gameObject);  // Destruir el objeto después de cerrarlo
    }
}
