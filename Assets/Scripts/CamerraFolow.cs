using UnityEngine;

public class CamerraFolow : MonoBehaviour
{
    public GameObject player; // Fait référence au joueur
    public float timeOffset; // Permet de déplcacer à une certaine vitesse
    public Vector3 posOffset;

    private Vector3 velocity;

    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + posOffset, ref velocity, timeOffset);
        // Permet de déplacer un objet d'un endroit à un autre
    }
}
