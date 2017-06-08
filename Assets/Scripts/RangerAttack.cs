using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerAttack : MonoBehaviour
{
    [SerializeField]
    private float range = 3f;

    [SerializeField]
    private float timeBetweenAttacks = 1f;

    [SerializeField]
    private Transform fireSpot;

    [SerializeField]
    private float arrowSpeed = 25f;

    private Animator anim;
    private Transform player;
    private bool playerInRange;
    private EnemyHealth enemyHealth;
    private GameObject arrow;

    // Use this for initialization
    void Start()
    {
        this.arrow = GameManager.instance.Arrow;
        this.player = GameManager.instance.Player.transform;
        this.anim = GetComponent<Animator>();
        this.enemyHealth = GetComponent<EnemyHealth>();
        StartCoroutine(this.Attack());
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(this.transform.position, this.player.position) < range && this.enemyHealth.IsAlive)
        {
            this.playerInRange = true;
            this.RotateTowards(this.player);
        }
        else
        {
            this.playerInRange = false;
        }

        this.anim.SetBool("PlayerInRange", this.playerInRange);
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

    private void RotateTowards(Transform _player)
    {
        var direction = (_player.position - this.transform.position).normalized;
        var lookRotation = Quaternion.LookRotation(direction);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, lookRotation, Time.deltaTime * 10f);
    }

    public void EnemyEndAttack()
    {

    }

    public void EnemyBeginAttack()
    {

    }

    public void FireArrow()
    {
        var newArrow = Instantiate(arrow) as GameObject;
        newArrow.transform.position = this.fireSpot.position;
        newArrow.transform.rotation = transform.rotation;
        newArrow.GetComponent<Rigidbody>().velocity = transform.forward * this.arrowSpeed;
    }
}
