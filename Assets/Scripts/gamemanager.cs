using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int Food;
    public List<GameObject> FoodSprites;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    } 
    void AddFood(int Amount)
    {
        Food += Amount;
    }
    void RemoveFood(int Amount)
    {
        Food -= Amount;
    }
}
