using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    public class PlayerStats : MonoBehaviour {
        public static PlayerStats instance;
        public int maxHealth = 100;
        public float healthRegenRate = 2f;

        private int _currentHealth;
        public int currentHealth
        {
            get { return _currentHealth; }
            set { _currentHealth = Mathf.Clamp(value, 0, maxHealth); }
        }
        public float movementSpeed = 10f;                    // The fastest the player can travel in the x axis.
        public void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }
    }

