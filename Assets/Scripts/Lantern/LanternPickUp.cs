using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternPickUp : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject linternaPlayer;
    [SerializeField] private SoundController sonidoLinterna;
    [SerializeField] private GameObject visualPilas;
    [SerializeField] private MainCharacter player;
    public void PickUpLantern()
    {
        player.cantLinterna += 1;
        sonidoLinterna.PlaySound1();
        linternaPlayer.SetActive(true);
        visualPilas.SetActive(true);
        linternaPlayer.GetComponent<Linterna>().linternaEnMano = true;
        Destroy(gameObject);
    }

}
