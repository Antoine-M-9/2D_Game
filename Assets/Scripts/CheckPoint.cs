using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private Transform playerSpawn;

    private void Awake() 
    {
        playerSpawn = GameObject.FindGameObjectWithTag("PlayerSpawn").transform;
        // Cette ligne permet d'obtenir la position du playerSpawn;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerSpawn.position = transform.position;
            // Nous changeons l'emplacement du playerSpawn à la position de notre checkpoint
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            // désactive le boxCollider afin de ne plus accéder à ce checkpoint
            gameObject.GetComponentInChildren<Animator>().enabled = false;
        }
    }
}
