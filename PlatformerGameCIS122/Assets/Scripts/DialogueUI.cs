// Jeremy Fischer
using TMPro;
using UnityEngine;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private TMP_Text textLabel;

    private void Start()
    {
        GetComponent<TypeWritterEffect>().Run("This is a bit of text!\n Hellow", textLabel);
    }
}
