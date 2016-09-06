using UnityEngine;
using System.Collections;

public class PlayerAiming : MonoBehaviour
{
	// Update is called once per frame
	void Update ()
	{
		// We are going to read the input every frame
		Vector3 vNewInput = new Vector3(Input.GetAxis("Horizontal_R"), Input.GetAxis("Vertical_R"), 0.0f);
		// Only do work if meaningful
		if(vNewInput.sqrMagnitude < 0.1f)
		{
			return;
		}
		
		// Set our rotation to represent the input
		vNewInput.Normalize();
		float fHeading = Vector3.Dot(Vector3.right, vNewInput);
		Vector3 vNewRotation = transform.rotation.eulerAngles;
		vNewRotation.z = fHeading * 90.0f;
		
		// Adjust our rotation if we're on the bottom half of the input circle
		if(vNewInput.y > 0.0f)
		{
			vNewRotation.z = 180.0f - vNewRotation.z;	
		}
		
		// Apply the transform to the object		
		transform.rotation = Quaternion.Euler(vNewRotation);
	}
}

