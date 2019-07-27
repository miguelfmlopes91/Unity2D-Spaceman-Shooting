using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {

    public static GameMaster gm;
    [SerializeField]
    private int maxLives = 3;
    private static int _remainingLives = 3;
    [SerializeField]
    private static int startingMoney = 100;
    public static int Money;

    public static int RemainingLives
    {
        get { return _remainingLives; }
        set {_remainingLives = value;}
    }

    public Transform playerPrefab;
    public Transform spawnPoint;
    public float spawnDelay = 2;
    public Transform spawnPrefab;
    public string respawnSound = "RespawnCountdown";
    public string spawnSound = "Spawn";
    public CameraShake cameraShake;
    [SerializeField]
    private GameObject gameOverUI;
    [SerializeField]
    private GameObject upgradeMenu;

    public delegate void UpgradeMenuCallback(bool active);
    public UpgradeMenuCallback onToggleUpgradeMenu;

    public string gameOverSound = "GameOver";
    
    //cache
    private Bardo.AudioManager audioManager;

    private void Awake()
    {
        if (gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        }
    }

    private void Start()
    {
        if (cameraShake == null)
        {
            Debug.LogError("No camera shake reference in Game Master");
        }
        RemainingLives = maxLives;
        Money = startingMoney;

        //caching 
        audioManager = Bardo.AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("No audiomanager reference in Game Master");
        }
    }

    public void EndGame() {
        Debug.Log("End Game");
        audioManager.PlaySound(gameOverSound);
        gameOverUI.SetActive(true);
    }
    public IEnumerator _RespawnPlayer()
    {
        audioManager.PlaySound(respawnSound);
        yield return new WaitForSeconds(spawnDelay);
        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        audioManager.PlaySound(spawnSound);
        Transform clone = (Transform)Instantiate(spawnPrefab, spawnPoint.position, spawnPoint.rotation);
        Destroy(clone.gameObject, 3f);
    }

    public static void KillEnemy(Enemy enemy){
        gm._killEnemy(enemy);
    }

    public static void KillPlayer(Player player) {
        Destroy(player.gameObject);
        _remainingLives--;

        if (_remainingLives <= 0)
        {
            gm.EndGame();
        }
        else
        {
            gm.StartCoroutine(gm._RespawnPlayer());
        }
    }

    public void _killEnemy(Enemy _enemy)
    {
        //add particles
        Transform clone =  Instantiate(_enemy.deathParticles, _enemy.transform.position, Quaternion.identity);
        Destroy(clone.gameObject, .5f);

        //do camera shake
        cameraShake.Shake(_enemy.shakeAmount, _enemy.shakeLength);
        Destroy(_enemy.gameObject);

        //play sounds
        audioManager.PlaySound(_enemy.deathSoundName);
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.U))
        {
            ToggleUpgradeMenu();
        }
    }

    private void ToggleUpgradeMenu(){
        upgradeMenu.SetActive(!upgradeMenu.activeSelf);
        onToggleUpgradeMenu.Invoke(upgradeMenu.activeSelf);
    }

}
