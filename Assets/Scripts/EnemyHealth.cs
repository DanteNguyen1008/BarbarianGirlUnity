using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour {

    [SerializeField]
    private int startingHealth = 20;

    [SerializeField]
    private float timeSinceLastHit = 0.5f;

    [SerializeField]
    private float dissapearSpeed = 2f;

    private AudioSource _audio;
    private float timer = 0f;
    private Animator anim;
    private NavMeshAgent nav;
    private bool isAlive;
    private Rigidbody _rigidBody;
    private CapsuleCollider capsuleCollider;
    private bool dissappearEnemy = false;
    private int currentHealth;
    private ParticleSystem blood;

	// Use this for initialization
	void Start () {
        GameManager.instance.RegisterEnemy(this);
        this._audio = GetComponent<AudioSource>();
        this._rigidBody = GetComponent<Rigidbody>();
        this.capsuleCollider = GetComponent<CapsuleCollider>();
        this.nav = GetComponent<NavMeshAgent>();
        this.anim = GetComponent<Animator>();
        this.isAlive = true;
        this.currentHealth = this.startingHealth;
        this.blood = GetComponentInChildren<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
        this.timer += Time.deltaTime;

        if(this.dissappearEnemy)
        {
            this.transform.Translate(-Vector3.up * this.dissapearSpeed * Time.deltaTime);
        }
	}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter");
        if (timer >= timeSinceLastHit && !GameManager.instance.IsGameOver)
        {
            if (other.tag.Equals("PlayerWeapon"))
            {
                Debug.Log("Player weapon hit");
                this.blood.Play();
                this.takeHit();
                this.timer = 0f;
            }
        }
    }

    private void takeHit()
    {
        if (this.currentHealth > 0)
        {
            this._audio.PlayOneShot(this._audio.clip);
            this.anim.Play("LeftHurt");
            this.currentHealth -= 10;
            this.blood.Play();
        }

        if(this.currentHealth <= 0)
        {
            this.isAlive = false;
            this.KillEnemy();
        }
    }

    private void KillEnemy()
    {
        GameManager.instance.KilledEnemy(this);
        this.capsuleCollider.enabled = false;
        this.nav.enabled = false;
        this.anim.SetTrigger("EnemyDie");
        this._rigidBody.isKinematic = true;

        StartCoroutine(this.removeEnemy());
    }

    IEnumerator removeEnemy()
    {
        yield return new WaitForSeconds(4f);
        this.dissappearEnemy = true;
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    public bool IsAlive
    {
        get { return this.isAlive; }
    }
}
