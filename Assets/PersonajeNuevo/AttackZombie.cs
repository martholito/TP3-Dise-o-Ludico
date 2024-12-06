using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackZombie : MonoBehaviour
{
    public float damage;

    private void OnTriggerEnter(Collider other)
    {
        MainCharacter player = other.GetComponent<MainCharacter>();
        if (player != null)
        {
            player.TakeDamage(damage);
        }
    }
}
