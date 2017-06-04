using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    [SerializeField]
    private float range = 3f;

    [SerializeField]
    private float timeBetweenAttacks = 1f;

    private Animator anim;
    private GameObject player;
    private bool playerInRange;
    private BoxCollider[] weaponColliders;
    private EnemyHealth enemyHealth;

	// Use this for initialization
	void Start () {
        this.weaponColliders = GetComponentsInChildren<BoxCollider>();
        this.player = GameManager.instance.Player;
        this.anim = GetComponent<Animator>();
        this.enemyHealth = GetComponent<EnemyHealth>();
        StartCoroutine(this.Attack());
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance(this.transform.position, this.player.transform.position) < range && this.enemyHealth.IsAlive)
        {
            this.playerInRange = true;
        }
        else
        {
            this.playerInRange = false;
        }
	}

    IEnumerator Attack()
    {
        if (this.playerInRange && !GameManager.instance.IsGameOver)
        {
            this.anim.Play("Attack");
            yield return new WaitForSeconds(this.timeBetweenAttacks);
        }

        yield return null;

        StartCoroutine(this.Attack());
    }

    public void EnemyBeginAttack()
    {
        foreach (var weapon in weaponColliders)
        {
            weapon.enabled = true;
        }
    }

    public void EnemyEndAttack()
    {
        foreach (var weapon in weaponColliders)
        {
            weapon.enabled = false;
        }
    }
}
