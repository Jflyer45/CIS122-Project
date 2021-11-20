using UnityEngine;

[System.Serializable]
public class Response 
{
    [SerializeField] private string responseText;
    [SerializeField] private DialogueObject dialogueObject;

    // gets and sets
    public string ResponseText => responseText;
    public DialogueObject DialougeObject => dialogueObject;
}
