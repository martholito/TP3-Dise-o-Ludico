using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubePickup : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private SoundController sonidoSube;

    [SerializeField] private float pickUpRadius;
    [SerializeField] private LayerMask collisionLayer;
    [SerializeField] private MainCharacter player;
    [SerializeField] private GameObject pressF;

    private bool isInRange = false; // Controla si el jugador está en el rango de interacción.

    private void Update()
    {
        CheckLanternProximity();
        if (isInRange && Input.GetKeyDown(KeyCode.F))
        {
            PickUpSube();
        }
    }

    private void CheckLanternProximity()
    {
        Collider[] collisions = UnityEngine.Physics.OverlapSphere(transform.position, pickUpRadius, collisionLayer);

        if (collisions.Length > 0)
        {
            if (!isInRange)
            {
                isInRange = true; // Marca que el jugador está en el rango.
                pressF.SetActive(true); // Muestra "Press F".
            }
        }
        else if (isInRange)
        {
            isInRange = false; // Marca que el jugador salió del rango.
            pressF.SetActive(false); // Oculta "Press F".
        }
    }
    private void PickUpSube()
    {
        pressF.SetActive(false);
        sonidoSube.PlaySound4();
        player.cantSube += 1;
        Destroy(gameObject);        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, pickUpRadius);
    }
}
