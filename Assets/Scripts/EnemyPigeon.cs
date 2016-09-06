using UnityEngine;
using System.Collections;

public class EnemyPigeon : Enemy
{
	// Local Variables
	GameObject m_pTarget;
	
	// Use this for initialization
	public override void Start()
	{
		// Call the base class
		base.Start();
		
		// What do we look like?
		GetComponent<Renderer>().material = (Material)Resources.Load("Materials/SpacePigeon") as Material;
		gameObject.name = " EnemyPigeon";
		
		// Play our sound
		m_pAudioSource.clip = (AudioClip)Resources.Load("Sounds/SpacePigeon") as AudioClip;
		m_pAudioSource.Play();
	}
	
	// The constructor for this class
	public override void Init(int nEnemyHealth, float fMoveSpeed, float fScale)
	{
		// Call the base class
		base.Init(nEnemyHealth, fMoveSpeed, fScale);
		
		// Do this class's init
		m_pTarget = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	public override void Update()
	{
		// Make sure we have a target
		if(m_pTarget == null)
		{
			DestroyObject(gameObject);
			return;
		}
		
		// First find our direction towards the target
		Vector3 vDirToTarget = m_pTarget.transform.position - transform.position;
		vDirToTarget.Normalize();

        // Move towards the player, try to crash into them
        transform.Translate(m_fMoveSpeed * vDirToTarget, Space.World);
        //transform.RotateAround(Vector3.forward, 30.0f);

        // Rotate to face our target
        float fHeading = Vector3.Dot(Vector3.right, vDirToTarget);
		Vector3 vNewRotation = transform.rotation.eulerAngles;
		vNewRotation.z = fHeading * 90.0f;
		
		// Adjust our rotation if we're on the bottom half of the input circle
		if(vDirToTarget.y > 0.0f)
		{
			vNewRotation.z = 180.0f - vNewRotation.z;	
		}
		
		// Apply the transform to the object		
		transform.rotation = Quaternion.Euler(vNewRotation);
	}
	
	// Collision reaction
	void OnTriggerEnter(Collider pOther)
	{
		// We only hit other players
		if(pOther.gameObject.GetComponent<PlayerMovement>() == null)
			return;

        // Do Damage then explode!
        pOther.gameObject.GetComponent<Blink>().StartBlinking(1f, .05f);
		pOther.BroadcastMessage("TakeDamage");
		Destroy(gameObject);
	}
}
