using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Food Spawn")]
    public int Food;
    public List<GameObject> Foodprefab;
    public Transform FoodSpawnPosition;
    private List<GameObject> spawnedFoodObjects = new List<GameObject>();
    [Header("NPC Spawn & Days")]
    public Transform SpawnPosition;
    public List<GameObject> NPCPrefabs;
    private Queue<int> currentDayQueue = new Queue<int>();
    private Dictionary<int, List<int>> dayNPCs = new Dictionary<int, List<int>>();
    private int currentDay = 0;



    // Start is called before the first frame update
    void Start()
    {
        // Detta bestämmer att dag 1 så kommer vi spawna npc 0, 1, 2 som ligger i listan
        dayNPCs[1] = new List<int> { 0, 1, 2 };
        StartDay(1);
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
            int randomIndex = Random.Range(0, Foodprefab.Count);
            GameObject randomFood = Foodprefab[randomIndex];

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
                GameOver();
                break;
            }
        }
    }

    public void StartDay(int day)
    {
        if (dayNPCs.ContainsKey(day)) // Check if the day exists
        {
            // Populate the queue with NPCs for the current day
            currentDayQueue = new Queue<int>(dayNPCs[day]);

            // Start spawning NPCs one by one
            StartCoroutine(SpawnNPCsSequentially());
        }
        else
        {
            Debug.Log($"No NPCs assigned for Day {day}.");
        }
    }
    private IEnumerator SpawnNPCsSequentially()
    {
        while (currentDayQueue.Count > 0)
        {
            int npcIndex = currentDayQueue.Dequeue(); // Get the next NPC from the queue
            if (npcIndex >= 0 && npcIndex < NPCPrefabs.Count) // Ensure index is valid
            {
                // Spawn the NPC and wait for it to be destroyed
                GameObject spawnedNPC = Instantiate(NPCPrefabs[npcIndex], SpawnPosition.position, Quaternion.identity);
                yield return WaitForNPCDestruction(spawnedNPC);
            }
            else
            {
                Debug.LogWarning($"Invalid NPC index: {npcIndex}");
            }
        }

        Debug.Log("All NPCs for the day have been spawned and destroyed.");
    }

    private IEnumerator WaitForNPCDestruction(GameObject npc)
    {
        // Wait until the NPC is destroyed
        while (npc != null)
        {
            yield return null; // Wait for the next frame
        }
    }

    public void NextDay()
    {
        currentDay++;
        StartDay(currentDay); // Start the next day
    } 
    private void GameOver()
    {
        Debug.Log("GameOver"); 
        //bytt till en scen för GameOver och sen gå tillbaka till menyn
    }
}
