// Jeremy Fischer
using TMPro;
using UnityEngine;
using System.Collections;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;
    private TypeWritterEffect typeWritterEffect;
    private ResponseHandler responseHandler;

    public bool IsOpen { get; set; }

    private void Start()
    {
        typeWritterEffect = GetComponent<TypeWritterEffect>();
        responseHandler = GetComponent<ResponseHandler>();
        CloseDialogueBox();
        
    }

    public void ShowDialogue(DialogueObject dialogueObject)
    {
        IsOpen = true;
        dialogueBox.SetActive(true);
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
        Debug.Log(dialogueObject);
        for(int i = 0; i < dialogueObject.Dialogue.Length; i++)
        {
            
            string dialogue = dialogueObject.Dialogue[i];
            yield return typeWritterEffect.Run(dialogue, textLabel);

            if(i == dialogueObject.Dialogue.Length && dialogueObject.HasResponses)
            {
                break;
            }

            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.F));
        }

        // If there are responses, then shows then, else remove the dialog box!
        if (dialogueObject.HasResponses)
        {
            responseHandler.ShowResponses(dialogueObject.Responses);
        }
        else
        {
            CloseDialogueBox();
        }
    }

    private void CloseDialogueBox()
    {
        IsOpen = false;
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
    }
}
