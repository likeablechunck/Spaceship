using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
public class Projectile : MonoBehaviour
{
	// Local variables
	float m_fProjectileSpeed = 0.5f;
	float m_fProjectileLifeTime = 2.0f;
	
	// The constructor for this class
	public void Init(float fProjectileSpeed)
	{
		// How fast are we going to move?
		m_fProjectileSpeed = fProjectileSpeed;
	}
	
	// Use this for initialization
	void Start ()
	{
		GetComponent<Rigidbody>().useGravity = false;
		GetComponent<Collider>().isTrigger = true;
	}
	
	// Update is called once per frame
	void Update ()
	{
		// Our project moves on it's own without needing player input
		transform.Translate(0.0f, m_fProjectileSpeed, 0.0f);
		
		m_fProjectileLifeTime -= Time.deltaTime;
		if(m_fProjectileLifeTime < 0.0f)
		{
			// Destroy this projectile after it's life time runs out
			DestroyObject(gameObject);
		}
	}
	
	// Collision handling
	void OnTriggerEnter(Collider pOther)
	{
		// We don't hit players
		if(pOther.gameObject.GetComponent<PlayerMovement>() != null)
			return;

		// Tell enemies they were hit							
		if(pOther.gameObject.GetComponent<Enemy>() != null)		
		{														
			// Tell whatever we hit to take damage				
			pOther.gameObject.BroadcastMessage("TakeDamage");	
		}														
		
		// Destroy this projectile after collision
		DestroyObject(gameObject);
	}
}
