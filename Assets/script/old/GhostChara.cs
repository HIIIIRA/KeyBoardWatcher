
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostChara : MonoBehaviour {

	private CharacterController characterController;
	private Animator animator;
	private Vector3 velocity;
	[SerializeField]
	private float walkSpeed = 1.5f;
	[SerializeField]
	private float jumpPower = 5f;

	// Use this for initialization
	void Start () {
		characterController = GetComponent<CharacterController> ();
		animator = GetComponent<Animator> ();
		velocity = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		if (characterController.isGrounded) {
			velocity = Vector3.zero;
			var input = new Vector3 (Input.GetAxis ("Horizontal"), 0f, Input.GetAxis ("Vertical"));

			if (input.magnitude > 0f) {
				transform.LookAt (transform.position + input);
				animator.SetFloat ("Speed", input.magnitude);
				velocity = input * walkSpeed;
			} else {
				animator.SetFloat ("Speed", 0f);
			}

			if (Input.GetButtonDown ("Jump") 
				&& !animator.GetCurrentAnimatorStateInfo(0).IsName("Jump")
			) {
				animator.SetTrigger ("Jump");
				velocity.y += jumpPower;
			}
		}

		velocity.y += Physics.gravity.y * Time.deltaTime;
		characterController.Move (velocity * Time.deltaTime);
	}
}

 
 