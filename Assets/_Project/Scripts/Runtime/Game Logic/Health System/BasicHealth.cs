using UnityEngine;
using WizardGame.Combat_System.Element_System;
using WizardGame.Health_System;
using WizardGame.ObjectRemovalHandling;

[RequireComponent(typeof(IRemovalProcessor))]
public class BasicHealth : MonoBehaviour, IDamageable, IHealable
{
    [SerializeField] private int maxHealth;

    private IRemovalProcessor removalProcessor;
    
    private int currentHealth;

    private void Awake()
    {
        removalProcessor = GetComponent<IRemovalProcessor>();

        ResetHealth();
    }

    private void ResetHealth()
    {
        currentHealth = maxHealth;
    }
    
    public void TakeDamage(int dmg, Element damageElement, GameObject damageSource = null)
    {
        currentHealth = Mathf.Clamp(currentHealth - dmg, 0, maxHealth);

        if (currentHealth == 0)
        {
            removalProcessor.Remove();
        }
    }

    public void Heal(int hp, object source)
    {
        if (currentHealth == 0)
        {
            return;
        }

        currentHealth = Mathf.Clamp(currentHealth + hp, 0, maxHealth);
    }
}