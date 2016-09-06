using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class pickupSpawner : MonoBehaviour
{
    ArrayList itemPrefabs;
    public string[] pickedupItems;
    public int availableItemCounter;
    public int pickedupItemsCounter;
    //public ArrayList pickedupItems = new ArrayList();


    // Use this for initialization
    void Start ()
    {
        Object[] loadedItems = Resources.LoadAll("Items");
        itemPrefabs = new ArrayList(loadedItems);
        availableItemCounter = itemPrefabs.Count;
        print("Number of items in array is: " + availableItemCounter);
        //pickedupItemsCounter = pickedupItems.Length;

    }
	
	// Update is called once per frame
	void Update ()
    {
        //itemInstantiation();
        if (GameObject.FindGameObjectsWithTag("Pick Up").Length == 0)

        {
            print("Time to instantiate an item");
            PlayerMovement pm = GameObject.Find("Player").GetComponent<PlayerMovement>();
            
            itemInstantiation();


        }
    }

    public void itemInstantiation()
    {
        Vector3 playerPosition = GameObject.Find("Player").transform.position;
        if (itemPrefabs.Count > 0)
        {
            int itemIndex = Random.Range(0, itemPrefabs.Count);
            //Instantiating an item from the array randomely
            GameObject itemGo = Instantiate((GameObject)itemPrefabs[itemIndex],
                new Vector3(Random.Range(-14f, 14), Random.Range(2, 18), playerPosition.z), Quaternion.identity) as GameObject;
            print("Item that was Instantiates was : " + itemGo.name);
            //removing that object from the array
            itemPrefabs.RemoveAt(itemIndex);

        }
        else
            return;
               
    }
}
