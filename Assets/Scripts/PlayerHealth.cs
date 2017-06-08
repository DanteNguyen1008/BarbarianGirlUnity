using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    [SerializeField]
    int startingHealth = 100;

    [SerializeField]
    float timeSinceLastHit = 2f;

    [SerializeField]
    Slider healthSlider;

    private float timer = 0f;
    private CharacterController chacterController;
    private Animator anim;
    private int currentHealth;
    private AudioSource _audio;
    private ParticleSystem blood;

    /// <summary>
    /// Gets or sets the current health
    /// </summary>
    public int CurrentHealth
    {
        get { return this.currentHealth; }
        set
        {
            if (value < 0)
            {
                this.currentHealth = 0;
            }

            this.currentHealth = value;
        }
    }

    void Awake()
    {
        Assert.IsNotNull(this.healthSlider);
    }

    // Use this for initialization
    void Start () {
        this.anim = GetComponent<Animator>();
        this.chacterController = GetComponent<CharacterController>();
        this.currentHealth = startingHealth;
        this._audio = GetComponent<AudioSource>();
        this.blood = GetComponentInChildren<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
        this.timer += Time.deltaTime;
	}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter " + other.tag);
        if (this.timer >= timeSinceLastHit && !GameManager.instance.IsGameOver)
        {
            if(other.tag.Equals("Weapon"))
            {
                this.takeHit();
                this.timer = 0;
            }
        }
    }

    public void PowerUpHeath()
    {
        this.CurrentHealth += 40;

        if (this.CurrentHealth > this.startingHealth)
        {
            this.CurrentHealth = this.startingHealth;
        }

        this.healthSlider.value = this.CurrentHealth;
    }

    public bool IsFullHealth ()
    {
        // the bigger condition is just sanity
        return this.CurrentHealth >= this.startingHealth;
    }

    private void takeHit()
    {
        if(this.currentHealth > 0)
        {
            // Alive
            GameManager.instance.PlayerHit(this.currentHealth);
            this.anim.Play("HitBack");
            currentHealth -= 10;
            this.healthSlider.value = this.currentHealth;
            this._audio.PlayOneShot(this._audio.clip);
            this.blood.Play();
        }
        
        if(this.currentHealth <= 0)
        {
            // death
            this.killPlayer();
        }
    }

    private void killPlayer()
    {
        // Game over
        GameManager.instance.PlayerHit(this.currentHealth);
        this.anim.SetTrigger("HeroDie");
        chacterController.enabled = false;
        this.blood.Play();
    }
}
