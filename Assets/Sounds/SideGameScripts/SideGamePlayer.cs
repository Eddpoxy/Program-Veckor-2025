
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SideGamePlayer : MonoBehaviour
{
    private Rigidbody2D rB_Player;
    public float moveSpeed = 5f;
    private Vector2 movement;
    public int scrapAmmount;
    public bool isBody = false;

    [SerializeField] private GameObject body;
    private GameObject currentTrashcan;
    private Dictionary<GameObject, bool> searchedTrashcans = new Dictionary<GameObject, bool>();

    private SpriteRenderer spriterenderer;

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
        spriterenderer = GetComponent<SpriteRenderer>();

    }

    void Update()
    {
        movement = Vector2.zero;

        if (Input.GetKey(KeyCode.W)) movement.y = 1f;
        if (Input.GetKey(KeyCode.D))
        {
            movement.x = 1f;
            spriterenderer.flipX = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            movement.x = -1f;
            spriterenderer.flipX = false;
        }
        if (Input.GetKey(KeyCode.S)) movement.y = -1f;    
        
        if(currentTrashcan != null && Input.GetKeyDown(KeyCode.E))
        {
            if (!IsTrashcanSearched(currentTrashcan))
            {
                Search();
                MarkTrashcanAsSearched(currentTrashcan);
                
            }
            else if(isBody == true && Input.GetKey(KeyCode.E))
            {
                searchPrompt.text = "Better leave it";
                isBody = false;
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
            else if (!IsTrashcanSearched(currentTrashcan) && isBody == true)
            {
                searchPrompt.text = "Poor cat";
                isBody = false;
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
        else if (collision.gameObject.CompareTag("Devil"))
        {
            SceneManager.LoadScene("GameOverScene");
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
            isBody = true;
            Instantiate<GameObject>(body, currentTrashcan.transform.position, Quaternion.identity);
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
