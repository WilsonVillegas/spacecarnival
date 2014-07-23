using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerPhysics))]
public class PlayerController : MonoBehaviour {
	
	// Player Handling
	public float gravity = 20;
	public float walkSpeed = 8;
	public float runSpeed = 12;
	public float acceleration = 30;
	public float jumpHeight = 12;
	
	private float currentSpeed;
	private float targetSpeed;
	private Vector2 amountToMove;

	private PlayerPhysics playerPhysics;

	// Animation shenanigans
	private Animator animator;
	private float animationSpeed;
	private float vertical;
	private float horizontal;
	private Vector3 pScale;

	void Start () {
		playerPhysics = GetComponent<PlayerPhysics>();
		animator = GetComponent<Animator>();
		pScale = transform.localScale;
	}
	
	void Update () {
		// Reset acceleration upon collision
		if (playerPhysics.movementStopped) {
			targetSpeed = 0;
			currentSpeed = 0;
		}
		
		// If player is touching the ground
		if (playerPhysics.grounded) {
			amountToMove.y = 0;
			
			// Jump
			if (Input.GetButtonDown("Jump")) {
				amountToMove.y = jumpHeight;	
			}
		}
		
		// Input
		float speed = (Input.GetButton("Run"))?runSpeed:walkSpeed;
		vertical = Input.GetAxis ("Vertical");
		horizontal = Input.GetAxis ("Horizontal");
		targetSpeed = Input.GetAxisRaw("Horizontal") * speed;
		currentSpeed = MoveTo(currentSpeed, targetSpeed,acceleration);

		if(horizontal > 0)
		{
			animator.SetInteger("State", 1);
			//pScale.x = Mathf.Abs(pScale.x);
			//transform.localScale = pScale;
		}
		else if(horizontal < 0)
		{
			animator.SetInteger("State", 1);
			//pScale.x = Mathf.Abs(pScale.x)*-1;
			//transform.localScale = pScale;
		}
		else
		{
			animator.SetInteger("State", 0);
		}
		
		// Set amount to move
		amountToMove.x = currentSpeed;
		amountToMove.y -= gravity * Time.deltaTime;
		playerPhysics.Move(amountToMove * Time.deltaTime);

		//Face
		if(horizontal != 0)
		{
			transform.eulerAngles = (horizontal>0)?Vector3.zero:Vector3.up * 180;
		}
	}
	
	// Increase n towards target by speed
	private float MoveTo(float n, float target, float a) {
		if (n == target) {
			return n;	
		}
		else {
			float dir = Mathf.Sign(target - n); // must n be increased or decreased to get closer to target
			n += a * Time.deltaTime * dir;
			return (dir == Mathf.Sign(target-n))? n: target; // if n has now passed target then return target, otherwise return n
		}
	}


}
