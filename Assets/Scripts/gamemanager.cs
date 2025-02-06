using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public Image DayTransition; 

    





    private void Awake()
    {
        DOTween.SetTweensCapacity(500, 50);
        if (Instance == null)
        {
            Instance = this; 
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    void Start()
    {
        StartCoroutine(AddFood(15));
        dayNPCs[0] = new List<int> { 1, 4, 3}; //{ 1, 4, 3};
        dayNPCs[1] = new List<int> { 0, 3, 2};
        dayNPCs[2] = new List<int> { 4, 0, 2};
        dayNPCs[3] = new List<int> {5};
        StartDay(0);
        Debug.Log($"Day {currentDay} started.");
    } 


    void Update()
    { 
        if (Input.GetKeyDown(KeyCode.X))
        {
            SceneManager.LoadScene("SideGame");
        }
        if (Food <= 0)
        {
            GameOver();
        }
        DayCounterText.text = ("Day: " + currentDay);
        FoodCounterText.text = ("Food: " + Food);
      
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }

    public IEnumerator AddFood(int Amount)
    {
        Food += Amount;
        for (int i = 0; i < Amount; i++) 
        {
            int randomIndex = Random.Range(0, Foodprefab.Count);
            GameObject randomFood = Foodprefab[randomIndex];

            GameObject spawnedFood = Instantiate(randomFood, FoodSpawnPosition.transform.position, Quaternion.identity);
            spawnedFoodObjects.Add(spawnedFood);

            yield return new WaitForSeconds(FoodSpawnSpeed);
        }

    }

    public IEnumerator RemoveFood(int Amount)
    {
        for (int i = 0; i < Amount; i++)
        {
            if (spawnedFoodObjects.Count > 0 && Food > 0) 
            {
                GameObject foodToRemove = spawnedFoodObjects[0];
                spawnedFoodObjects.RemoveAt(0);

                Destroy(foodToRemove);

                Food = Mathf.Max(0, Food - 1); 

                yield return new WaitForSeconds(FoodSpawnSpeed); 
            }
        }
    }

    public void StartDay(int day)
    {
        if (dayNPCs.ContainsKey(day)) 
        {
            
            currentDayQueue = new Queue<int>(dayNPCs[day]);

            StartCoroutine(SpawnNPCsSequentially());
        }
        else
        {
            SceneManager.LoadScene("Win Scene");
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
                GameObject spawnedNPC = Instantiate(NPCPrefabs[npcIndex], SpawnPosition.position, Quaternion.identity);

                NPCS npcScript = spawnedNPC.GetComponent<NPCS>();
                if (npcScript != null)
                {
                    npcScript.npcID = npcIndex;
                }

                yield return WaitForNPCDestruction(spawnedNPC);
            }
            else
            {
                Debug.LogWarning($"Invalid or null NPC prefab for index: {npcIndex}");
            }
        }

        Debug.Log("All NPCs for the day have been spawned and destroyed.");
        Transition();
        yield return new WaitForSeconds(2f);
        NextDay();
    }

    private IEnumerator WaitForNPCDestruction(GameObject npc)
    {
        while (npc != null)
        {
            yield return null; 
        }
    }

    public void NextDay()
    {
       
        currentDay++;
        StartCoroutine(RemoveFood(DailyFoodLoss));
        

        if (dayNPCs.ContainsKey(currentDay))
        {
            StartDay(currentDay); 
            Debug.Log($"Day {currentDay} started.");
        }
        else
        {
            Debug.Log($"No NPCs assigned for Day {currentDay}. End of the game or no data available.");
           
        }
    }
    public void GameOver()
    {
        Debug.Log("GameOver");
        SceneManager.LoadScene("GameOverScene"); 
    }

    public void RecordChoice(int npcID, string choice)
    {
        npcChoices[npcID] = choice; 
    }

    public string GetChoice(int npcID)
    {
        if (npcChoices.ContainsKey(npcID))
        {
            return npcChoices[npcID]; 
        }
        return null; 
    }
    private void Transition()
    {
        DayTransition.gameObject.SetActive(true);

        Color initialColor = DayTransition.color;
        initialColor.a = 0f;
        DayTransition.color = initialColor;

        Sequence transitionSequence = DOTween.Sequence();
        transitionSequence.Append(DayTransition.DOFade(1f, 1f)) 
                         .AppendInterval(1f) 
                         .Append(DayTransition.DOFade(0f, 1f)) 
                         .OnComplete(() => DayTransition.gameObject.SetActive(false)); 

    }





}
