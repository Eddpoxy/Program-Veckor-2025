using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 

{
    public static GameManager Instance;
    [Header("Food Spawn")]
    public int Food;
    public List<GameObject> Foodprefab;
    public Transform FoodSpawnPosition;
    public float FoodSpawnSpeed;
    private List<GameObject> spawnedFoodObjects = new List<GameObject>();
    public TextMeshProUGUI FoodCounterText;
    public int DailyFoodLoss;
    [Header("NPC Spawn & Days")]
    public Transform SpawnPosition;
    public List<GameObject> NPCPrefabs;
    private Queue<int> currentDayQueue = new Queue<int>();
    private Dictionary<int, List<int>> dayNPCs = new Dictionary<int, List<int>>();
    private int currentDay = 0;
    private Dictionary<int, string> npcChoices = new Dictionary<int, string>();
    public TextMeshProUGUI DayCounterText;
    





    private void Awake()
    {
        DOTween.SetTweensCapacity(500, 50);
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
        dayNPCs[0] = new List<int> { 3, 1, 4};
        dayNPCs[1] = new List<int> { 0, 3, 2};
        dayNPCs[3] = new List<int> { 4, 0, 2 };
        StartDay(0);
        Debug.Log($"Day {currentDay} started.");
    } 


    // Update is called once per frame
    void Update()
    {
        DayCounterText.text = ("Day: " + currentDay);
        FoodCounterText.text = ("Food: " + Food);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(RemoveFood(10));
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(AddFood(10));
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }

    public IEnumerator AddFood(int Amount)
    {
        Food += Amount;
        for (int i = 0; i < Amount; i++) // Change from i <= Amount to i < Amount
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
            if (spawnedFoodObjects.Count > 0 && Food > 0) // Ensure food is available
            {
                // Remove the oldest (first) food object in the list
                GameObject foodToRemove = spawnedFoodObjects[0];
                spawnedFoodObjects.RemoveAt(0);

                // Destroy the food GameObject
                Destroy(foodToRemove);

                // Decrease the food counter, but ensure it doesn't go below 0
                Food = Mathf.Max(0, Food - 1); // This ensures Food stays at 0 if it tries to go negative

                yield return new WaitForSeconds(FoodSpawnSpeed); // Delay between removing each food
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

                // Set the npcID for the spawned NPC
                NPCS npcScript = spawnedNPC.GetComponent<NPCS>();
                if (npcScript != null)
                {
                    npcScript.npcID = npcIndex; // Use the index as a consistent ID
                }

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
        StartCoroutine(RemoveFood(DailyFoodLoss));
        if (Food <= 0)
        {
            GameOver();
        }

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
        SceneManager.LoadScene("GameOverScene"); // Byt till Game Over-scenen
    }

    public void RecordChoice(int npcID, string choice)
    {
        npcChoices[npcID] = choice; // Store the choice
    }

    // Method to retrieve the player's previous choice for an NPC
    public string GetChoice(int npcID)
    {
        if (npcChoices.ContainsKey(npcID))
        {
            return npcChoices[npcID]; // Return the stored choice
        }
        return null; // Return null if no choice exists for this NPC
    }

    



}
