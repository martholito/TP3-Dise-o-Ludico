using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LlavePickup : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private SoundController sonidoLlave;

    [SerializeField] private float pickUpRadius;
    [SerializeField] private LayerMask collisionLayer;
    [SerializeField] private MainCharacter player;

    private void Update()
    {
        DestroyKeys();
    }

    private void DestroyKeys()
    {
        Collider[] collisions = UnityEngine.Physics.OverlapSphere(transform.position, pickUpRadius, collisionLayer);

        if (collisions.Length > 0)
        {
            foreach (Collider coll in collisions)
            {
                var collidedRigidBody = coll.GetComponent<Rigidbody>();
                if (Input.GetKeyDown(KeyCode.F) && collidedRigidBody != null)
                {
                    sonidoLlave.PlaySound3();
                    Destroy(gameObject);
                    player.cantLlaves += 1;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, pickUpRadius);
    }
}
