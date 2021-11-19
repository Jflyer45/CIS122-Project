// Jeremy Fischer
using UnityEngine;

[CreateAssetMenu(menuName ="Dialogue/DialogueObject")]

public class DialogueObject : ScriptableObject
{
    [SerializeField] [TextArea] private string[] dialogue;

    // Get Sets
    // Read only
    public string[] Dialogue => dialogue;
}
