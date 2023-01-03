using UnityEngine;

public class DontDestroyOnLoadScene : MonoBehaviour
{
    public GameObject[] objets; // Nos différents objets sélectionnés seront stockés dans ce tableau
    void Start()
    {
        foreach (var element in objets)
        {
            DontDestroyOnLoad(element);
        } // Permet de ne pas détruire certains objets après chargement d'une nouvelle scène
          // Ces objets seront tagués DontDestroyOnLoad
    }
}
