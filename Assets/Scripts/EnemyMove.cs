using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    [SerializeField]
    Transform player;

    private NavMeshAgent nav;
    private Animator anim;

    void Awake()
    {
        Assert.IsNotNull(this.player);
    }

    // Use this for initialization
    void Start () {
        this.anim = GetComponent<Animator>();
        this.nav = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        this.nav.SetDestination(this.player.position);
	}
}
