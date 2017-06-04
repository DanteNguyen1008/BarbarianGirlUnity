using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float moveSpeed = 10.0f;

    [SerializeField]
    private LayerMask layerMask;

    private CharacterController characterController;
    private Vector3 currentLookTarget = Vector3.zero;
    private Animator anim;
    private bool isAttacking = false;
    private BoxCollider[] weaponColliders;

	// Use this for initialization
	void Start ()
    {
        this.characterController = this.GetComponent<CharacterController>();
        this.anim = this.GetComponent<Animator>();
        this.weaponColliders = GetComponentsInChildren<BoxCollider>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (GameManager.instance.IsGameOver)
        {
            this.anim.SetBool("IsWalking", false);
            this.anim.SetBool("IsRunning", false);
            this.anim.SetBool("IsJump", false);
            this.characterController.enabled = false;
            return;
        }

        var moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        this.characterController.SimpleMove(moveDirection * this.moveSpeed);

        // Walking
        this.anim.SetBool("IsWalking", moveDirection != Vector3.zero);

        // Running
        if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            this.anim.SetBool("IsRunning", true);
            this.moveSpeed = 6.5f;
        }
        else
        {
            this.anim.SetBool("IsRunning", false);
            this.moveSpeed = 4f;
        }

        // Space
        this.anim.SetBool("IsJump", Input.GetKey(KeyCode.Space));

        // Attacks
        if (Input.GetKeyDown(KeyCode.I))
        {
            this.anim.SetInteger("Attack", 1);
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            this.anim.SetInteger("Attack", 2);
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            this.anim.SetInteger("Attack", 3);
        }
        else if (Input.GetKeyDown(KeyCode.J) || Input.GetMouseButtonDown(0))
        {
            this.anim.SetInteger("Attack", 4);
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            this.anim.SetInteger("Attack", 5);
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            this.anim.SetInteger("Attack", 6);
        }
        else
        {
            this.anim.SetInteger("Attack", 0);
        }
    }

    void FixedUpdate()
    {
        if(GameManager.instance.IsGameOver)
        {
            return;
        }

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 500, this.layerMask, QueryTriggerInteraction.Ignore))
        {
            Debug.DrawRay(ray.origin, ray.direction * 500, Color.blue);
            if (hit.point != this.currentLookTarget)
            {
                this.currentLookTarget = hit.point;
            }

            var targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            var rotation = Quaternion.LookRotation(targetPosition - transform.position);

            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 10f);
        }
    }

    void OnStartWeaponAttack()
    {
        foreach (var weapon in this.weaponColliders)
        {
            weapon.enabled = true;
        }
    }

    void OnEndWeaponAttack()
    {
        foreach (var weapon in this.weaponColliders)
        {
            weapon.enabled = false;
        }
    }
}
