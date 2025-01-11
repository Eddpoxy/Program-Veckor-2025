using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 

{
    public static GameManager Instance;
    [Header("Food Spawn")]
    public int Food;
    public List<GameObject> Foodprefab;
    public Transform FoodSpawnPosition;
    public float FoodSpawnSpeed;
    private List<GameObject> spawnedFoodObjects = new List<GameObject>();
    [Header("NPC Spawn & Days")]
    public Transform SpawnPosition;
    public List<GameObject> NPCPrefabs;
    private Queue<int> currentDayQueue = new Queue<int>();
    private Dictionary<int, List<int>> dayNPCs = new Dictionary<int, List<int>>();
    private int currentDay = 0;
    



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // Ensure only one instance of GameManager exists
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AddFood(15));
        // Detta bestämmer att dag 1 så kommer vi spawna npc 0, 1, 2 som ligger i listan
        dayNPCs[0] = new List<int> { 0, 1, 2 };
        dayNPCs[1] = new List<int> { 0, 1, 2 };
        StartDay(0);
        Debug.Log($"Day {currentDay} started.");
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

            yield return new WaitForSeconds(FoodSpawnSpeed);
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

                yield return new WaitForSeconds(FoodSpawnSpeed); // Delay between removing each food
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
            int npcIndex = currentDayQueue.Dequeue();
            if (npcIndex >= 0 && npcIndex < NPCPrefabs.Count && NPCPrefabs[npcIndex] != null)
            {
                // Instantiate the NPC
                GameObject spawnedNPC = Instantiate(NPCPrefabs[npcIndex], SpawnPosition.position, Quaternion.identity);
                yield return WaitForNPCDestruction(spawnedNPC);
            }
            else
            {
                Debug.LogWarning($"Invalid or null NPC prefab for index: {npcIndex}");
            }

        }

        Debug.Log("All NPCs for the day have been spawned and destroyed.");
        yield return new WaitForSeconds(2f);
        NextDay();
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

        if (dayNPCs.ContainsKey(currentDay))
        {
            StartDay(currentDay); // Start the next day
            Debug.Log($"Day {currentDay} started.");
        }
        else
        {
            Debug.Log($"No NPCs assigned for Day {currentDay}. End of the game or no data available.");
           
        }
    } 
    private void GameOver()
    {
        Debug.Log("GameOver"); 
        //bytt till en scen för GameOver och sen gå tillbaka till menyn
    }
   

}
