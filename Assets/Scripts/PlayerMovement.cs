using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent (typeof (Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
	// Local Variables
	Vector3 m_vMinBounds = new Vector3(-14.0f, 2.0f, 0.0f);
	Vector3 m_vMaxBounds = new Vector3(14.0f, 18.0f, 0.0f);
	public float m_fPlayerSpeed = 0.1f;
    public bool readyToInstantiate = false;
    public Text winText;
    public Text treasureText;
    int treasureCounter = 0;
    


    // Use this for initialization
    void Start ()
	{
		GetComponent<Rigidbody>().useGravity = false;
		GetComponent<Collider>().isTrigger = true;
        winText.text = "";
        treasureText.text = "";
    }
	
	// Update is called once per frame
	void Update ()
	{
		// We are going to read the input every frame
		Vector3 vNewInput = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
		
		// Only do any work if meaningful
		if(vNewInput.sqrMagnitude < 0.1f)
			return;
        vNewInput.x *= -1;	
		// Turn the input in a normalized, or 'Unit' vector
		vNewInput.Normalize();
		// Add our input to our position
		transform.position += vNewInput * m_fPlayerSpeed;
		
		// Limit the position to our play area
		Vector3 vNewPosition = transform.position;
		vNewPosition = Vector3.Max(m_vMinBounds, vNewPosition);
		vNewPosition = Vector3.Min(m_vMaxBounds, vNewPosition);
		transform.position = vNewPosition;
		
		// Set our rotation to represent the input
		float fHeading = Vector3.Dot(Vector3.right, vNewInput);
		Vector3 vNewRotation = transform.rotation.eulerAngles;
		vNewRotation.z = fHeading * 90.0f;
		
		// Adjust our rotation if we're on the bottom half of the input circle
		if(vNewInput.y < 0.0f) // Input is inverted
		{
			vNewRotation.z = 180.0f - vNewRotation.z;	
		}
		
		// Apply the transform to the object		
		transform.rotation = Quaternion.Euler(vNewRotation);
	}
	
	// React to taking damage
	public virtual void TakeDamage()
    {
    }

    //picking up all pick up items, using "pickupSpawner" script
    void OnTriggerEnter(Collider other)
    {
        pickupSpawner ps = GameObject.Find("pickupSpawner").GetComponent<pickupSpawner>();

        if (other.tag == "Pick Up")
        {
            print("I collected a pick up item");
            //ps.pickedupItems += other.name;
            //ps.pickedupItems.Add(other.name);
            ps.pickedupItemsCounter++;
            print("Things I already picked up are: " + ps.pickedupItems);
            Destroy(other.gameObject);
            //other.gameObject.SetActive(false);
            readyToInstantiate = true;
            if(ps.pickedupItemsCounter == ps.availableItemCounter)
            {
                SceneManager.LoadScene("End_Animation");
                //winText.text = "Congratulations!\n Now you can water your plants :D";

            }
           
        }
        else if (other.tag == "Treasures")
        {
            //treasureCounter++;
            //treasureText.text= (treasureCounter.ToString);
            Destroy(other.gameObject);
        }


    }


}