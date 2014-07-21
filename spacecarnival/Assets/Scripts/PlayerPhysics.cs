using UnityEngine;
using System.Collections;


[RequireComponent (typeof(BoxCollider))]
public class PlayerPhysics : MonoBehaviour {
	
	public LayerMask collisionMask;
	
	private BoxCollider collider;
	private Vector3 size;
	private Vector3 center;
	
	private float skin = .005f;
	
	[HideInInspector]
	public bool grounded;
	[HideInInspector]
	public bool movementStopped;
	
	Ray ray;
	RaycastHit hit;
	
	void Start() {
		collider = GetComponent<BoxCollider>();
		size = collider.size;
		center = collider.center;
	}
	
	public void Move(Vector2 moveAmount) {
		
		float deltaY = moveAmount.y;
		float deltaX = moveAmount.x;
		Vector2 p = transform.position;
		
		// Check vertical collisions
		grounded = false;
		
		for (int i = 0; i<3; i ++) {
			float dir = Mathf.Sign(deltaY);
			float x = (p.x + center.x - size.x/2) + size.x/2 * i; // Left, center, and right edge of the collider
			float y = p.y + center.y + size.y/2 * dir; // Bottom of collider
			
			ray = new Ray(new Vector2(x,y), new Vector2(0,dir));
			Debug.DrawRay(ray.origin,ray.direction);
			
			if (Physics.Raycast(ray,out hit,Mathf.Abs(deltaY) + skin,collisionMask)) {
				// Distance between the player and the ground
				float dst = Vector3.Distance (ray.origin, hit.point);
				
				// Stop player's downwards movement after coming within skin width of a collider
				if (dst > skin) {
					deltaY = dst * dir - skin * dir;
				}
				else {
					deltaY = 0;
				}
				
				grounded = true;
				
				break;
				
			}
		}
		
		
		// Check collisions left and right
		movementStopped = false;
		for (int i = 0; i<3; i ++) {
			float dir = Mathf.Sign(deltaX);
			float x = p.x + center.x + size.x/2 * dir;
			float y = p.y + center.y - size.y/2 + size.y/2 * i;
			
			ray = new Ray(new Vector2(x,y), new Vector2(dir,0));
			Debug.DrawRay(ray.origin,ray.direction);
			
			if (Physics.Raycast(ray,out hit,Mathf.Abs(deltaX) + skin,collisionMask)) {
				// Distance between player and ground
				float dst = Vector3.Distance (ray.origin, hit.point);
				
				// Stop player's downwards movement after coming within skin width of a collider
				if (dst > skin) {
					deltaX = dst * dir - skin * dir;
				}
				else {
					deltaX = 0;
				}
				
				movementStopped = true;
				break;
				
			}
		}
		
		
		
		
		Vector2 finalTransform = new Vector2(deltaX,deltaY);
		
		transform.Translate(finalTransform);
	}
	
}
