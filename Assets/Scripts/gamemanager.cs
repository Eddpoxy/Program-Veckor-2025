using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int Food;
    public List<GameObject> FoodSprites;
    public Transform FoodSpawnPosition;

    // List to keep track of all instantiated food objects
    private List<GameObject> spawnedFoodObjects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
      
      

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(RemoveFood(10));
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(AddFood(10));
        }
    }

    public IEnumerator AddFood(int Amount)
    {
        Food += Amount;
        for (int i = 0; i <= Amount; i++)
        {
            // Select a random GameObject from the FoodSprites list
            int randomIndex = Random.Range(0, FoodSprites.Count);
            GameObject randomFood = FoodSprites[randomIndex];

            // Instantiate the food and add it to the spawnedFoodObjects list
            GameObject spawnedFood = Instantiate(randomFood, FoodSpawnPosition.transform.position, Quaternion.identity);
            spawnedFoodObjects.Add(spawnedFood);

            yield return new WaitForSeconds(0.25f);
        }
    }

    public IEnumerator RemoveFood(int Amount)
    {
        for (int i = 0; i < Amount; i++)
        {
            if (spawnedFoodObjects.Count > 0)
            {
                // Remove the oldest (first) food object in the list
                GameObject foodToRemove = spawnedFoodObjects[0];
                spawnedFoodObjects.RemoveAt(0);

                // Destroy the food GameObject
                Destroy(foodToRemove);

                // Decrease the food counter
                Food--;

                yield return new WaitForSeconds(0.25f); // Delay between removing each food
            }
            else
            {
                // If no food is left, break out of the loop
                break;
            }
        }
    }
}
