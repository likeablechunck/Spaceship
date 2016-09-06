using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
	// Local Variables
	public int m_nEnemyHealth = 5;
	public float m_fEnemySpeed = 0.1f;
	public float m_fSpawnRadius = 20.0f;
	public float m_fEnemyScale = 2.0f;
	public float m_fSpawnDelay = 3.0f;
	float m_fAccumulatedSpawnTime = 0.0f;
	
	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
		// Keep track of how much time has passed
		m_fAccumulatedSpawnTime += Time.deltaTime;
		while(m_fAccumulatedSpawnTime > m_fSpawnDelay)
		{
			// Decrement the time
			m_fAccumulatedSpawnTime -= m_fSpawnDelay;
			
			// Create a new enemy
			GameObject pNewObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
			
			// Match the spawner's rotation and position
			pNewObject.transform.rotation = transform.rotation;
			pNewObject.transform.position = transform.position;
			// Rotate the new object randomly
			pNewObject.transform.RotateAround(Vector3.forward, Random.Range(0.0f, 360.0f));
			// Then push the object back the spawn offset distance
			pNewObject.transform.Translate(m_fSpawnRadius, 0.0f, 0.0f);
			
			// Set the behaviour on this new enemy
			Enemy pEnemy;
			if(Random.value > 0.5f)
			{
				pEnemy = (Enemy)pNewObject.AddComponent<EnemyPigeon>();
			}
			else
			{
				pEnemy = (Enemy)pNewObject.AddComponent<EnemySaw>();
			}
			pEnemy.Init(m_nEnemyHealth, m_fEnemySpeed, m_fEnemyScale);
		}
	}
}
