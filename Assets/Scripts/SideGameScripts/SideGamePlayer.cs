using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Security.Cryptography;

public class SideGamePlayer : MonoBehaviour
{
    private Rigidbody2D rB_Player;
    public bool isTrashcan;
    public float moveSpeed = 5f;
    private Vector2 movement;
    public int scrapAmmount;

    [TextArea(2, 5)]
    public string prompt = "Press E to search";

    [SerializeField] private TextMeshProUGUI searchPrompt;
    [SerializeField] private TextMeshProUGUI scrapCount;
    void Start()
    {
        rB_Player = GetComponent<Rigidbody2D>();
        isTrashcan = false;
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
        
        if(isTrashcan && Input.GetKeyDown(KeyCode.E))
        {
            Search();
        }

        scrapCount.text = "Scrap: " + scrapAmmount;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Trashcan"))
        {
            Debug.Log("You suck ass");
            isTrashcan = true;
            searchPrompt.text = prompt;        
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Trashcan"))
        {
            searchPrompt.text = "";
            isTrashcan = false;
        }
    }

    private void FixedUpdate()
    {
        rB_Player.velocity = movement.normalized * moveSpeed;
    }

    public void Search()
    {
        int randomNumber = Random.Range(1, 11);
        Debug.Log("Number generated" + randomNumber);

        if(randomNumber <= 9)
        {
            Debug.Log("You found 1 scrap");
            scrapAmmount++;
            isTrashcan = false;
            searchPrompt.text = "";
        }
        else
        {
            Debug.Log("Nothing was found");
            isTrashcan = false;
            searchPrompt.text = "";
        }
    }
}
