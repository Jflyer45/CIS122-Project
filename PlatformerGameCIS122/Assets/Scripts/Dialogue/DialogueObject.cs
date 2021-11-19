// Jeremy Fischer
using UnityEngine;

[CreateAssetMenu(menuName ="Dialogue/DialogueObject")]

public class DialogueObject : ScriptableObject
{
    [SerializeField] [TextArea] private string[] dialogue;
    [SerializeField] private Response[] responses;

    // Get Sets
    // Read only
    public string[] Dialogue => dialogue;
    public Response[] Responses => responses;
}
