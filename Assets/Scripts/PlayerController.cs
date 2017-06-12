using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private const string SWORD1TAG = "Sword1";
    private const string SWORD2TAG = "Sword2";
    private const string SWORD3TAG = "Sword3";
    private const string SWORD4TAG = "Sword4";

    [SerializeField]
    private float moveSpeed = 10.0f;

    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private GameObject weaponLeftHolder;
        
    [SerializeField]
    private GameObject weaponRightHolder;

    [SerializeField]
    private GameObject sword1ModelLeft, sword1ModelRight;

    [SerializeField]
    private GameObject sword2ModelLeft, sword2ModelRight;

    [SerializeField]
    private GameObject sword3ModelLeft, sword3ModelRight;

    [SerializeField]
    private GameObject sword4ModelLeft, sword4ModelRight;

    private CharacterController characterController;
    private Vector3 currentLookTarget = Vector3.zero;
    private Animator anim;
    private bool isAttacking = false;
    private BoxCollider[] weaponColliders;
    private GameObject leftWeapon, rightWeapon;

	// Use this for initialization
	void Start ()
    {
        this.characterController = this.GetComponent<CharacterController>();
        this.anim = this.GetComponent<Animator>();
        this.weaponColliders = GetComponentsInChildren<BoxCollider>();

        //// Use first weapon by default
        this.ReplaceWeapon(this.sword1ModelLeft, this.sword1ModelRight);
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

    public void ReplaceWeapon(string swordTag)
    {
        if(swordTag.Equals(SWORD1TAG))
        {
            this.ReplaceWeapon(this.sword1ModelLeft, this.sword1ModelRight);
        }
        else if (swordTag.Equals(SWORD2TAG))
        {
            this.ReplaceWeapon(this.sword2ModelLeft, this.sword2ModelRight);
        }
        else if (swordTag.Equals(SWORD3TAG))
        {
            this.ReplaceWeapon(this.sword3ModelLeft, this.sword3ModelRight);
        }
        else if (swordTag.Equals(SWORD4TAG))
        {
            this.ReplaceWeapon(this.sword4ModelLeft, this.sword4ModelRight);
        }
    }

    public void ReplaceWeapon(GameObject _leftWeapon, GameObject _rightWeapon)
    {
        // Remove old weapons
        if(this.leftWeapon != null)
        {
            Destroy(this.leftWeapon);
        }

        if(this.rightWeapon != null)
        {
            Destroy(this.rightWeapon);
        }

        this.leftWeapon = Instantiate(_leftWeapon) as GameObject;
        this.rightWeapon = Instantiate(_rightWeapon) as GameObject;

        //// reset all local position and rotation to base and rotate the holder only
        this.leftWeapon.transform.parent = this.weaponLeftHolder.transform;
        this.leftWeapon.transform.localPosition = new Vector3(0, 0, 0);
        this.leftWeapon.transform.rotation = new Quaternion(0, 0, 0, 0);

        this.rightWeapon.transform.parent = this.weaponRightHolder.transform;
        this.rightWeapon.transform.localPosition = new Vector3(0, 0, 0);
        this.rightWeapon.transform.rotation = new Quaternion(0, 0, 0, 0);
    }
}
