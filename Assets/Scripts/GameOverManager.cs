using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverUI;

    public static GameOverManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de GameOverManager dans la scène");
            return;
        }

        instance = this;
    }

    public void OnPlayerDeath() 
    {
        if (CurrentSceneManager.instance.isPlayerPresentByDefault)
        {
            DontDestroyOnLoadScene.instance.RemoveFromDontDestroyOnLoad();
        }

        gameOverUI.SetActive(true);
    }

    // Il faut 1 méthode par bouton
    // Les méthodes doivent être en public afin de pouvoir être appelé depuis un bouton UI
    public void RetryButton() 
    {
        // Supprime le nombre de pièces qui ont été récupérés dans cette scène
        Inventory.instance.RemoveCoins(CurrentSceneManager.instance.coinsPickedUpInThisSceneCount);
        // Recharge la scène 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        // Replace le joueur au spawn
        PlayerHealth.instance.Respawn();
        // Réactive les mouvements du joueur + qu'on lui rende sa vie
        gameOverUI.SetActive(false); // Désactive le menu Game Over
    }

    public void MainMenuButton() 
    {
        // Retour au menu principal
    }

    public void QuitButton() 
    {
        // Fermer le jeu
        Application.Quit();
    }
}
