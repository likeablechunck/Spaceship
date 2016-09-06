using UnityEngine;
using System.Collections;

public class EnemySaw : Enemy
{
	// Local Variables
	Vector3 m_vDirection;
	Vector3 m_vMinBounds = new Vector3(-14.0f, 2.0f, 0.0f);
	Vector3 m_vMaxBounds = new Vector3(14.0f, 18.0f, 0.0f);
		
	// Use this for initialization
	public override void Start()
	{
		// Call the base class
		base.Start();
		
		// What do we look like?
		GetComponent<Renderer>().material = (Material)Resources.Load("Materials/SpaceSaw") as Material;
		gameObject.name = " EnemySaw";
		
		// Play our sound
		m_pAudioSource.clip = (AudioClip)Resources.Load("Sounds/SpaceSaw") as AudioClip;
		m_pAudioSource.loop = true;
		m_pAudioSource.Play();
	}
	
	// The constructor for this class
	public override void Init(int nEnemyHealth, float fMoveSpeed, float fScale)
	{
		// Call the base class
		base.Init(nEnemyHealth, fMoveSpeed, fScale);
		
		// Do this class's init
		// First find our direction towards the target
		GameObject pTarget = GameObject.Find("Player");
		m_vDirection = pTarget.transform.position - transform.position;
		m_vDirection.Normalize();
	}
	
	// Update is called once per frame
	public override void Update()
	{
		// Move towards the player, try to crash into them
		transform.Translate(m_fMoveSpeed * m_vDirection, Space.World);
		// This saw is always spinning
		transform.RotateAround(Vector3.forward, 30.0f);
		
		// Adjust our direction
		if((transform.position.x < m_vMinBounds.x && m_vDirection.x < 0) ||
		   (transform.position.x > m_vMaxBounds.x && m_vDirection.x > 0))
		{
			m_vDirection.x *= -1;
		}
		if((transform.position.y < m_vMinBounds.y && m_vDirection.y < 0) ||
		   (transform.position.y > m_vMaxBounds.y && m_vDirection.y > 0))
		{
			m_vDirection.y *= -1;
		}
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
