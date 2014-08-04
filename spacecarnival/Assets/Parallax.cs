using UnityEngine;
using System.Collections;

public class Parallax : MonoBehaviour {

	public Transform target;
	public float trackingSpeed;
	[HideInInspector]
	public bool moving;

	void Start()
	{
		moving = target.transform.GetComponent<PlayerController>().playerMoving;
	}
	
	void Update()
	{
		moving = target.transform.GetComponent<PlayerController>().playerMoving;
		if (target && moving)
		{
			float x = Accelerate (transform.position.x, -1 * target.position.x, trackingSpeed);
			transform.position = new Vector3(x, transform.position.y, transform.position.z);
		}
	}
	
	private float Accelerate(float n, float target, float a)
	{
		if (n == target)
		{
			return n;
		}
		else
		{
			//n gets faster or slower, whichever goes closer to target speed
			float dir = Mathf.Sign(target - n);
			n += a * Time.deltaTime * dir;
			//If n has passed the target, return the target speed, otherwise return n
			return (dir == Mathf.Sign (target-n))? n: target;
		}
	}

}
