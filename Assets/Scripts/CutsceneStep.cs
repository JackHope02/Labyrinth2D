using UnityEngine;
using UnityEngine.Video;

[System.Serializable]
public class CutsceneStep
{
    public Sprite image;
    public VideoClip videoClip;

    [TextArea(2, 4)]
    public string[] dialogueLines;       // Multiple dialogue lines per step
    public Sprite portrait;              // Optional speaker portrait
    public AudioClip[] voiceLines;       // Optional voice lines, matched to dialogue
}
