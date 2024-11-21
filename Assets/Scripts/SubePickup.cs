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

    private void Update()
    {
        DestroySube();
    }

    private void DestroySube()
    {
        Collider[] collisions = UnityEngine.Physics.OverlapSphere(transform.position, pickUpRadius, collisionLayer);

        if (collisions.Length > 0)
        {
            foreach (Collider coll in collisions)
            {
                var collidedRigidBody = coll.GetComponent<Rigidbody>();
                if (Input.GetKeyDown(KeyCode.F) && collidedRigidBody != null)
                {
                    sonidoSube.PlaySound4();
                    Destroy(gameObject);
                    player.cantSube += 1;
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
