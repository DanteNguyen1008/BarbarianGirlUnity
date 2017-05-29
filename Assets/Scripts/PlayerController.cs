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

	// Use this for initialization
	void Start ()
    {
        this.characterController = this.GetComponent<CharacterController>();
        this.anim = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
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

        this.anim.SetBool("SpinAttack", Input.GetMouseButton(0));
    }

    void FixedUpdate()
    {
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
}
