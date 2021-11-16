// Jeremy Fischer
using TMPro;
using UnityEngine;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private TMP_Text textLabel;

    private void Start()
    {
        textLabel.text = "Hello! \n this is my s4econd line";
    }
}
