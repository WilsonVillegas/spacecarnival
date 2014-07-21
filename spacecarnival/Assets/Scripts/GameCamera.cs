using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour 
{
	private Transform target;
	private float trackingSpeed = 4;

	public void SetTarget(Transform t)
	{
		target = t;
	}

	void LateUpdate()
	{
		if (target)
		{
			float x = Accelerate (transform.position.x, target.position.x, trackingSpeed);
			float y = Accelerate (transform.position.y, target.position.y, trackingSpeed);
			transform.position = new Vector3(x, y, transform.position.z);
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
			//n gets faster or slower, whichver goes closer to target speed
			float dir = Mathf.Sign(target - n);
			n += a * Time.deltaTime * dir;
			//If n has passed the target, return the target speed, otherwise return n
			return (dir == Mathf.Sign (target-n))? n: target;
		}
	}


}
