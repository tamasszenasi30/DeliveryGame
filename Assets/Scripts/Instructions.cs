using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instructions : MonoBehaviour
{
    // GameObject variable for a copy of prefab
    public GameObject spawnObject;

    // gameObject reference for the iconPrefab
    public GameObject iconPrefab;
    // array with gameObjects for the restaurant zones
    public GameObject[] restaurantArray;

    // array with gameObjects for the customer zones
    public GameObject[] customerArray;

    // index number for the random restaurant zone
    public int randomRestaurantIndex;

    // index number for the random customer zone
    public int randomCustomerIndex;

    // giving an offset for the instantiated object in space, 5 meters up
    Vector3 arrowOffset = new Vector3(0, 5f, 0);

    Vector3 distance;

    

    // Start is called before the first frame update
    void Start()
    {
        ShowRestaurantIndicator();
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    // method to choose a random customer zone, returning an integer
    public int ChooseRandomCustomer()
    {
        // setting randomCustomerIndex to a randomly generated index number
        // between 0 and the length of the array
        randomCustomerIndex = Random.Range(0, customerArray.Length);
        // returning the index number
        return randomCustomerIndex;

    }

    // method to show an indicator at a randomly chosen customer zone
    public void ShowCustomerIndicator()
    {
        
        // spawning a copy of game object prefab (iconPrefab, position, rotation)
        spawnObject = Instantiate(iconPrefab, customerArray[randomCustomerIndex].transform.position + arrowOffset, Quaternion.identity);

    }

    // method to choose a random restaurant zone, returning and integer
    public int ChooseRandomRestaurant()
    {
        // setting randomRestaurantIndex to a randomly generated index number
        // between 0 and the length of the array
        // returning the index number
        return Random.Range(0, restaurantArray.Length);
    }


    // method to show an indicator at the randomly chosen restaurant zone
    public void ShowRestaurantIndicator()
    {
        ChooseRandomRestaurant();
        // spawning a copy of gameObject(object name, position + offset, rotation)
        spawnObject = Instantiate(iconPrefab, restaurantArray[randomRestaurantIndex].transform.position + arrowOffset, Quaternion.identity);

        // setting currentIndex to 0
        int currentIndex = 0;

        //going through all items (r) in the restaurant array
        foreach (GameObject r in restaurantArray)
        {
            // if currentIndex equals randomRestaurantIndex
            if(currentIndex == randomRestaurantIndex) //3
            {
                // switch on the boxcollider on the item
                r.GetComponent<BoxCollider>().enabled = true;
            }

            // otherwise
            else
            {
                // switch off the boxcollider on the item
                r.GetComponent<BoxCollider>().enabled = false;
                // go to the next item
                
            }
            currentIndex++;
        }

    }

   

    

}
