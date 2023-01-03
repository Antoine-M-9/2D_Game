using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public float invincibilityTimeAfterHit = 2f;
    public float invincibilityFlashDelay = 0.15f; 
    public bool isInvincible = false;

    public SpriteRenderer graphics;
    public HealthBar healthBar;

    public static PlayerHealth instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de Inventory dans la scène");
            return;
        }

        instance = this;
    }

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H)) {
            TakeDamage(60);
        }
    }

    public void HealPlayer(int amount)
    {
        if ((currentHealth + amount) > maxHealth) 
        {
            currentHealth = maxHealth;
        }
        else 
        {
        currentHealth = maxHealth;
        }

        healthBar.SetMaxHealth(maxHealth);

    }

    public void TakeDamage(int damage) 
    {
        if (!isInvincible)
        {
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);

            //  Vérifier que le joueur est toujours vivant
            if (currentHealth <= 0) 
            {
                Die();
                return;
            }

            isInvincible = true;
            StartCoroutine(InvicibilityFlash());
            StartCoroutine(HandleInvincibilityDelay());
        }
    }

    public void Die() 
    {
        // On veut bloquer les mouvement du personnage
        PlayerMovement.instance.enabled = false;

        // Jouer l'animation d'élimination
        PlayerMovement.instance.animator.SetTrigger("Die");

        // empêcher les intéractions physique avec les intéractions de la scène
        PlayerMovement.instance.rb.bodyType = RigidbodyType2D.Kinematic;
        PlayerMovement.instance.playerCollider.enabled = false;
    }

    public IEnumerator InvicibilityFlash() 
    {
        while(isInvincible) 
        {
            graphics.color = new Color(1f, 1f, 1f, 0f); // on vient changer la couleur de notre personnage
            yield return new WaitForSeconds(invincibilityFlashDelay); // Permet d'ajouter un temps d'attente
            graphics.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(invincibilityFlashDelay);
        }
    }

    public IEnumerator HandleInvincibilityDelay()
    {
        yield return new WaitForSeconds(invincibilityTimeAfterHit);
        isInvincible = false;
    }
}
