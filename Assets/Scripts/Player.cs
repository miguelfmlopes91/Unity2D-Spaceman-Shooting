using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

[RequireComponent(typeof(Platformer2DUserControl))]
public class Player : MonoBehaviour
{
    [System.Serializable]
    public class PlayerStats {
        public int maxHealth = 100;

        private int _currentHealth;
        public int currentHealth
        {
            get { return _currentHealth; }
            set { _currentHealth = Mathf.Clamp(value, 0, maxHealth); }
        }

        public void Init()
        {
            currentHealth = maxHealth;
        }
    }

    public PlayerStats stats = new PlayerStats();
    public int fallBoundary = -20;

    public string deathSoundName = "DeathVoice";
    public string gruntSoundName = "Grunt";

    private Bardo.AudioManager audioManager;

    [Header("Optional: ")]
    [SerializeField]
    private StatusIndicator statusIndicator;

    private void Start()
    {
        stats.Init();
        if (statusIndicator == null)
        {
            Debug.LogError("Missing status indicator object reference");
        }
        else
        {
            statusIndicator.SetHealth(stats.currentHealth, stats.maxHealth);

        }
         audioManager = Bardo.AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("No AudioManager found");
        }

        GameMaster.gm.onToggleUpgradeMenu += OnUpgradeMenuToggle;
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
}
