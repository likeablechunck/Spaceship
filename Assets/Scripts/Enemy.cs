using UnityEngine;
using System.Collections;

[RequireComponent (typeof (AudioSource))]
[RequireComponent (typeof (Rigidbody))]
public abstract class Enemy : MonoBehaviour
{
	// Local variables
	protected int m_nHitPoints = 5;
	protected float m_fMoveSpeed = 0.08f;
	protected AudioSource m_pAudioSource;
    public LootTable lootTable;

    // Use this for initialization
    public virtual void Start()
	{
		m_pAudioSource = (AudioSource)gameObject.GetComponent("AudioSource");
		m_pAudioSource.playOnAwake = false;
		GetComponent<Rigidbody>().useGravity = false;
		GetComponent<Collider>().isTrigger = true;
        lootTable = LootTable.CreateLootTable();
    }

    // Update is called once per frame
    //public abstract void Update();
    public virtual void Update()
    {
       
    }

    // The constructor for this class
    public virtual void Init(int nEnemyHealth, float fMoveSpeed, float fScale)
	{
		// How much health do we have
		m_nHitPoints = nEnemyHealth;
		
		// How fast are we going to move?
		m_fMoveSpeed = fMoveSpeed;
		
		// How big is this enemy?
		transform.localScale = new Vector3(fScale, fScale, fScale);
	}
	
	// React to taking damage
	public virtual void TakeDamage()
	{
		// Take a hit, return if still alive
		if(--m_nHitPoints > 0)
			return;
		
		// Otherwise this enemy is dead
		DestroyObject(gameObject);
        if (lootTable != null)
        {
            if (lootTable.data != null)
            {
                if (lootTable.data.Count > 0)
                {
                    LootTableEntry entry = lootTable.data[Random.Range(0, lootTable.data.Count)];
                    float random = Random.Range(0.0f, 1.0f);

                    if (random < entry.chance)
                    {
                        //      Drop loot
                        print(this.gameObject.name + " dropped a " + entry.item.name + "!");
                        // instantiate an object using the prefab
                        GameObject droppedItem = Instantiate((GameObject)entry.item,
                            new Vector3(Random.Range(-14f, 14), Random.Range(2, 18), 0), Quaternion.identity) as GameObject;                
                    }
                } else
                {
                    print("loot table data is not null but is empty");
                }
            } else
            {
                print("loot table data is null");
            }
        } else
        {
            print("loot table is null");
        }
    }
}
