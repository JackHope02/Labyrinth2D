using UnityEngine;

public class MazeIntroTrigger : MonoBehaviour
{
    public DialogueController dialogueController;

    [Header("Dialogue Content")]
    public Sprite icarusPortrait;
    public AudioClip line1Audio;
    public AudioClip line2Audio;

    private bool hasTriggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            hasTriggered = true;
            dialogueController.StartDialogue(GetIntroDialogue());
        }
    }

    DialogueLine[] GetIntroDialogue()
    {
        return new DialogueLine[]
        {
            new DialogueLine
            {
                text = "I need to find a way out of here",
                portrait = icarusPortrait,
                voiceLine = line1Audio
            },
            new DialogueLine
            {
                text = "Father had a map on him, I need to find his body",
                portrait = icarusPortrait,
                voiceLine = line2Audio
            }
        };
    }
}
