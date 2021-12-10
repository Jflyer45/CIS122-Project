using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1Exit : MonoBehaviour 
{
    [SerializeField] private string scenceDestination;

    // Trigger Detection.
    public void OnTriggerEnter2D(Collider2D collision)
    {
        // If the player touches the trophy
        if (collision.tag == "Player")
        {
            PlayerController player = collision.GetComponent<PlayerController>();

            Dictionary<string, int> fruitStats = player.getFruitStats();

            bool hasEnoughFruit = true;

            foreach(string key in fruitStats.Keys)
            {
                if(fruitStats[key] < 1)
                {
                    hasEnoughFruit = false;
                    break;
                }
            }

            if (hasEnoughFruit)
            {
                SceneManager.LoadScene(scenceDestination);
            }
            else
            {
                Debug.Log("Not enough Fruit");
            }
        }
    }
}
