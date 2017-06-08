using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    private GameObject player;
    private NavMeshAgent nav;
    private Animator anim;
    private EnemyHealth enemyhealth;

    // Use this for initialization
    void Start () {
        this.player = GameManager.instance.Player;
        this.anim = GetComponent<Animator>();
        this.nav = GetComponent<NavMeshAgent>();
        this.enemyhealth = GetComponent<EnemyHealth>();
	}
	
	// Update is called once per frame
	void Update () {
        if(!GameManager.instance.IsGameOver && this.enemyhealth.IsAlive)
        {
            this.nav.SetDestination(this.player.transform.position);
        }
        else if ((!GameManager.instance.IsGameOver || GameManager.instance.IsGameOver) && !this.enemyhealth.IsAlive)
        {
            this.nav.enabled = false;
        }
        else 
        {
            this.nav.enabled = false;
            this.anim.Play("Idle");
        }
    }
}
