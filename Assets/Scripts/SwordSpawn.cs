using UnityEngine;

public class SwordSpawn : SpawnObject {

    [SerializeField]
    GameObject leftWeapon;

    [SerializeField]
    GameObject rightWeapon;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == this.player)
        {
            var playerController = this.player.GetComponent<PlayerController>();

            if(playerController != null)
            {
                playerController.ReplaceWeapon(leftWeapon, rightWeapon);
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
