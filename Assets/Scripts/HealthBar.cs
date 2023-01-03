using UnityEngine;
using UnityEngine.UI; // Packages - Plugins me permettant d'accéder au slider, img etc ...

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    public Gradient gradient;
    public Image fill;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(int health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue); // récupère la bonne couleur de notre grandient 
    }
}
