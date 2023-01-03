using UnityEngine;
using UnityEngine.UI;

public class Ladder : MonoBehaviour
{
    private bool isInRange; // est à porté
    // La variable a été mise en public afin de garder un oeil dessus dans l'inspector de Unity
    private PlayerMovement playerMovement;
    public BoxCollider2D topCollider;
    public Text interactUI;

    void Awake() 
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        interactUI = GameObject.FindGameObjectWithTag("InteractUI").GetComponent<Text>();
    }

    void Update() 
    {
        if (isInRange && playerMovement.isClimbing && Input.GetKeyDown(KeyCode.E))
        {
            playerMovement.isClimbing = false;
            topCollider.isTrigger = false; // Nous ne sommes plus en train de monter allors il faut remettre le mur
            return;
        }

        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            playerMovement.isClimbing = true;
            topCollider.isTrigger = true; // Quand le joueur monte l'échelle on active le déclancheur afin de traverser le mur 
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.CompareTag("Player"))
        {
            interactUI.enabled = true;
            isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) 
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = false; // nous somme plus dans la portée de l'échelle 
            playerMovement.isClimbing = false; // on ne peut plus monter
            topCollider.isTrigger = false; // On ne peut plus franchir le mur
            interactUI.enabled = false;
        }
    }


}
