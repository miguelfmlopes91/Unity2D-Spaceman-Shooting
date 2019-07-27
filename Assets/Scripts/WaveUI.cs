using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveUI : MonoBehaviour
{
    [SerializeField]
    WaveSpawner spawner;

    [SerializeField]
    Animator waveAnimator;

    [SerializeField]
    Text waveCountDownText;

    [SerializeField]
    Text waveCountText;

    private WaveSpawner.SpawnState previousState;
    // Start is called before the first frame update
    void Start()
    {
        if (spawner == null )
        {
            Debug.LogError("No spawner reference");
            this.enabled = false;
        }
        if (waveAnimator == null)
        {
            Debug.LogError("No waveAnimator reference");
            this.enabled = false;
        }
        if (waveCountDownText == null)
        {
            Debug.LogError("No waveCountDownText reference");
            this.enabled = false;
        }
        if (waveCountText == null)
        {
            Debug.LogError("No waveCountText reference");
            this.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (spawner.State)
        {
            case WaveSpawner.SpawnState.COUNTING:
                UpdateCountingUI();
                break;
            case WaveSpawner.SpawnState.WAITING:
                //UpdateIncomingUI();
                break;
            case WaveSpawner.SpawnState.SPAWNING:
                UpdateSpawningUI();
                break;
            default:
                break;
        }
        previousState = spawner.State;
    }
    private void UpdateCountingUI()
    {
        if (previousState != WaveSpawner.SpawnState.COUNTING)
        {
            waveAnimator.SetBool("WaveIncoming", false);
            waveAnimator.SetBool("WaveCountdown", true);
        }
        waveCountDownText.text = ((int)spawner.WaveCountdown).ToString();
    }
    private void UpdateSpawningUI()
    {
        if (previousState!= WaveSpawner.SpawnState.SPAWNING)
        {
            waveAnimator.SetBool("WaveCountdown", false);
            waveAnimator.SetBool("WaveIncoming", true);

            waveCountText.text = (spawner.NexWave + 1).ToString();
        }
    }


}
