using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerUp : SpawnObject
{
    private PlayerHealth playerHealth;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == this.player && !this.playerHealth.IsFullHealth())
        {
            this.playerHealth.PowerUpHeath();
            GameManager.instance.UnRegisterPowerUp();
            Destroy(gameObject);
        }
    }

    protected override void OnStart()
    {
        base.OnStart();
        this.playerHealth = this.player.GetComponent<PlayerHealth>();
        this.RegisterObject();
    }

    protected override void RegisterObject()
    {
        GameManager.instance.RegisterPowerUp();
    }

    protected override void UnRegisterObject()
    {
        GameManager.instance.UnRegisterPowerUp();
    }
}
