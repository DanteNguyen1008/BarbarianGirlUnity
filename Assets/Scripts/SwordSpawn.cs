using UnityEngine;

public class SwordSpawn : SpawnObject {

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == this.player)
        {
            var playerController = this.player.GetComponent<PlayerController>();

            if(playerController != null)
            {
                playerController.ReplaceWeapon(this.tag);
            }

            this.UnRegisterObject();
            Destroy(gameObject);
        }
    }

    protected override void OnStart()
    {
        base.OnStart();
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
