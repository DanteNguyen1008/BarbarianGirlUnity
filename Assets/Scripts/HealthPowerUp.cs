using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerUp : MonoBehaviour {

    private GameObject player;
    private PlayerHealth playerHealth;

	// Use this for initialization
	void Start () {
        this.player = GameManager.instance.Player;
        this.playerHealth = this.player.GetComponent<PlayerHealth>();
        GameManager.instance.RegisterPowerUp();
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == this.player && !this.playerHealth.IsFullHealth())
        {
            this.playerHealth.PowerUpHeath();
            GameManager.instance.UnRegisterPowerUp();
            Destroy(gameObject);
        }
    }

    void Update()
    {
        this.transform.Rotate(0, 100 * Time.deltaTime, 0);
    }
}
