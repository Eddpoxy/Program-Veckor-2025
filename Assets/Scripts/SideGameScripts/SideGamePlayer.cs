
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SideGamePlayer : MonoBehaviour
{
    private Rigidbody2D rB_Player;
    public float moveSpeed = 5f;
    private Vector2 movement;
    public int scrapAmmount;

    private GameObject currentTrashcan;
    private Dictionary<GameObject, bool> searchedTrashcans = new Dictionary<GameObject, bool>();

    [TextArea(2, 5)]
    public string prompt = "Press E to search";

    [SerializeField] private TextMeshProUGUI searchPrompt;
    [SerializeField] private TextMeshProUGUI scrapCount;
    void Start()
    {
        rB_Player = GetComponent<Rigidbody2D>();
        searchPrompt.text = "";
        scrapCount.text = "Scrap: " + scrapAmmount;
        scrapAmmount = 0;

    }

    void Update()
    {
        movement = Vector2.zero;

        if (Input.GetKey(KeyCode.W)) movement.y = 1f;
        if (Input.GetKey(KeyCode.D)) movement.x = 1f;
        if (Input.GetKey(KeyCode.A)) movement.x = -1f;
        if (Input.GetKey(KeyCode.S)) movement.y = -1f;    
        
        if(currentTrashcan != null && Input.GetKeyDown(KeyCode.E))
        {
            if (!IsTrashcanSearched(currentTrashcan))
            {
                Search();
                MarkTrashcanAsSearched(currentTrashcan);
                
            }
            else
            {
                searchPrompt.text = "It's empty";
            }

          
        }

        scrapCount.text = "Scrap: " + scrapAmmount;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Trashcan"))
        {
            Debug.Log("You suck ass");
            currentTrashcan = collision.gameObject;

            if (!IsTrashcanSearched(currentTrashcan))
            {
                searchPrompt.text = prompt;
            }
            else
            {
                searchPrompt.text = "Empty";
            }

                 
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Trashcan"))
        {
            currentTrashcan = null;
            searchPrompt.text = "";
        }
    }

    private void FixedUpdate()
    {
        rB_Player.velocity = movement.normalized * moveSpeed;
    }

    public void Search()
    {
        int randomNumber = Random.Range(1, 101);
        Debug.Log("Number generated" + randomNumber);

        if(randomNumber <= 70)
        {
            Debug.Log("You found 1 scrap");
            scrapAmmount++;
            searchPrompt.text = "You found 1 scrap";
        }
        else if(randomNumber <=100 && randomNumber >= 90)
        {
            Debug.Log("Oh Oh!");
            searchPrompt.text = "You found a body!";
        }
        else
        {
            Debug.Log("Nothing was found");
            searchPrompt.text = "Nothing was found";
        }
    }

    public bool IsTrashcanSearched(GameObject trashcan)
    {
        return searchedTrashcans.ContainsKey(trashcan) && searchedTrashcans[trashcan];
    }

    private void MarkTrashcanAsSearched(GameObject trashcan)
    {
        searchedTrashcans[trashcan] = true;
    }
}
