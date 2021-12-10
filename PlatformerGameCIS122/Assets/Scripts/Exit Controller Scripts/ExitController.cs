using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitController : MonoBehaviour
{
    [SerializeField] private string scenceDestination;

    // Trigger Detection.
    public void OnTriggerEnter2D(Collider2D collision)
    {
        // If the player touches the trophy
        if(collision.tag == "Player")
        {
            SceneManager.LoadScene(scenceDestination);
        }
    }
}
