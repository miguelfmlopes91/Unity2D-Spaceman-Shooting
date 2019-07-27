using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

[RequireComponent(typeof(Platformer2DUserControl))]
public class Player : MonoBehaviour
{
    public int fallBoundary = -20;

    public string deathSoundName = "DeathVoice";
    public string gruntSoundName = "Grunt";

    private Bardo.AudioManager audioManager;

    [Header("Optional: ")]
    [SerializeField]
    private StatusIndicator statusIndicator;

    private PlayerStats stats;

    private void Start()
    {
        stats = PlayerStats.instance;
        stats.currentHealth = stats.maxHealth;
        if (statusIndicator == null)
        {
            Debug.LogError("Missing status indicator object reference");
        }
        else
        {
            statusIndicator.SetHealth(stats.currentHealth, stats.maxHealth);

        }
        GameMaster.gm.onToggleUpgradeMenu += OnUpgradeMenuToggle;

         audioManager = Bardo.AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("No AudioManager found");
        }

        InvokeRepeating("RegenHealth", 1f/stats.healthRegenRate, 1f/stats.healthRegenRate);
    }

    void Update(){
        if (transform.position.y <= fallBoundary)
        {
            DamagePlayer(10000000);
        }
    }

    void OnUpgradeMenuToggle(bool active){
        //handle what happens when the upgrade menu is toggled
        GetComponent<Platformer2DUserControl>().enabled = !active;
        Weapon weapon = GetComponentInChildren<Weapon>();
        if (weapon != null)
        {
            weapon.enabled = !active;
        }
    }
    public void DamagePlayer(int damage)
    {
        stats.currentHealth -= damage;
        if (stats.currentHealth <= 0)
        {
            audioManager.PlaySound(deathSoundName);
            GameMaster.KillPlayer(this);
        }else
        {
            audioManager.PlaySound(gruntSoundName);
        }
        statusIndicator.SetHealth(stats.currentHealth, stats.maxHealth);
    }
    private void OnDestroy() {
        GameMaster.gm.onToggleUpgradeMenu -= OnUpgradeMenuToggle;
    }

    private void RegenHealth(){
        stats.currentHealth += 1;
        statusIndicator.SetHealth(stats.currentHealth, stats.maxHealth);
    }
}
