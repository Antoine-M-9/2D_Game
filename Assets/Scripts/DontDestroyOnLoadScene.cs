using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyOnLoadScene : MonoBehaviour
{
    public GameObject[] objets; // Nos différents objets sélectionnés seront stockés dans ce tableau

    public static DontDestroyOnLoadScene instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de DontDestroyOnLoadScene dans la scène");
            return;
        }

        instance = this;
         
        foreach (var element in objets)
        {
            DontDestroyOnLoad(element);
            // Permet de ne pas détruire les éléments sélectionné après le chargement d'une nouvelle scène
            // Ces objets seront tagués DontDestroyOnLoad
        }
        
    }

    public void RemoveFromDontDestroyOnLoad()
    {
        foreach (var element in objets)
        {
            SceneManager.MoveGameObjectToScene(element, SceneManager.GetActiveScene());
            // Permet de déplacer les element de DontDestroyOnLoad vers la scène qui est active;
        }
    }
}
