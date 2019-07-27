using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
    [SerializeField]
    private Text healthText;
    [SerializeField]
    private Text speedText;

    private PlayerStats stats;

    [SerializeField]
    private float healthMultiplier = 1.3f;
    private int upgradeCost = 50;

    string pressedButtonSound = "ButtonPress";

    Bardo.AudioManager audioManager;

    private void Start()
    {
        audioManager = Bardo.AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("No AudioManager found");
        }
    }

    private void OnEnable() {
        stats = PlayerStats.instance;
        UpdateValues();
    }
    void UpdateValues(){
        healthText.text = "HEALTH: " + stats.maxHealth.ToString();
        speedText.text = "SPEED: " + stats.movementSpeed.ToString();
    }

    public void UpgradeHealth(){
        if (GameMaster.Money < 0)
        {
            audioManager.PlaySound("No Money");
            return;
        }
        audioManager.PlaySound(pressedButtonSound);
        stats.maxHealth =(int)(stats.maxHealth * healthMultiplier);
        GameMaster.Money -= upgradeCost;
        Debug.Log("Upgrading to " + stats.maxHealth.ToString());
        audioManager.PlaySound("Money");
        UpdateValues();
    }
    public void UpgradeSpeed(){
        if (GameMaster.Money < upgradeCost)
        {
            audioManager.PlaySound("No Money");
            return;
        }
        audioManager.PlaySound(pressedButtonSound);
        stats.movementSpeed =(int)(stats.movementSpeed * healthMultiplier);
        GameMaster.Money -= upgradeCost;
        Debug.Log("Upgrading to " + stats.movementSpeed.ToString());
        audioManager.PlaySound("Money");
        UpdateValues();
    }
}
