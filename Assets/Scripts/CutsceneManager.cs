using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    [Header("Main Menu")]
    public GameObject menuPanel;
    public Button startButton;
    public Button exitButton;
    public VideoPlayer menuVideoPlayer;

    [Header("Cutscene Media Display")]
    public Image cutsceneImageDisplay;
    public RawImage cutsceneVideoDisplay;
    public VideoPlayer cutsceneVideoPlayer;
    public RenderTexture cutsceneRenderTexture;

    [Header("Dialogue UI")]
    public GameObject dialogueUI;
    public Image portraitImage;
    public Text dialogueText;
    public AudioSource voiceSource;

    [Header("Cutscene Steps")]
    public CutsceneStep[] cutscenes;
    public string nextSceneName;

    private int index = 0;
    private bool playingCutscene = false;
    private bool videoActive = false;

    private int currentLineIndex = 0;
    private string[] currentDialogueLines;
    private AudioClip[] currentVoiceLines;

    void Start()
    {
        menuPanel.SetActive(true);
        cutsceneImageDisplay.gameObject.SetActive(false);
        cutsceneVideoDisplay.gameObject.SetActive(false);
        dialogueUI.SetActive(false);
        menuVideoPlayer.Play();

        cutsceneVideoPlayer.loopPointReached += OnVideoEnd;
    }

    public void StartCutscene()
    {
        startButton.interactable = false;
        exitButton.interactable = false;
        menuPanel.SetActive(false);
        menuVideoPlayer.Stop();

        playingCutscene = true;
        index = 0;
        PlayStep();
    }

    void Update()
    {
        if (!playingCutscene || videoActive)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            if (currentDialogueLines != null && currentLineIndex < currentDialogueLines.Length)
            {
                ShowDialogueLine(); // Continue current step dialogue
            }
            else
            {
                index++;
                if (index < cutscenes.Length)
                    PlayStep();
                else
                    SceneManager.LoadScene(nextSceneName);
            }
        }
    }

    void PlayStep()
    {
        var step = cutscenes[index];

        // Reset visuals
        cutsceneImageDisplay.gameObject.SetActive(false);
        cutsceneVideoDisplay.gameObject.SetActive(false);
        dialogueUI.SetActive(false);
        voiceSource.Stop();
        portraitImage.enabled = false; // Default hidden unless assigned

        currentLineIndex = 0;
        currentDialogueLines = null;
        currentVoiceLines = null;

        // Display media
        if (step.videoClip != null)
        {
            cutsceneVideoDisplay.gameObject.SetActive(true);
            cutsceneVideoPlayer.clip = step.videoClip;
            cutsceneVideoPlayer.targetTexture = cutsceneRenderTexture;
            cutsceneVideoPlayer.Play();
            videoActive = true;
        }
        else if (step.image != null)
        {
            cutsceneImageDisplay.sprite = step.image;
            cutsceneImageDisplay.gameObject.SetActive(true);
            videoActive = false;
        }

        // Setup dialogue
        if (step.dialogueLines != null && step.dialogueLines.Length > 0)
        {
            currentDialogueLines = step.dialogueLines;
            currentVoiceLines = step.voiceLines;
            dialogueUI.SetActive(true);

            // Handle portrait
            if (step.portrait != null)
            {
                portraitImage.sprite = step.portrait;
                portraitImage.enabled = true;
            }

            ShowDialogueLine(); // Show first line
        }
    }

    void ShowDialogueLine()
    {
        if (currentDialogueLines == null || currentLineIndex >= currentDialogueLines.Length)
            return;

        dialogueText.text = currentDialogueLines[currentLineIndex];

        if (currentVoiceLines != null && currentLineIndex < currentVoiceLines.Length)
        {
            AudioClip voiceClip = currentVoiceLines[currentLineIndex];
            if (voiceClip != null)
            {
                voiceSource.Stop();
                voiceSource.PlayOneShot(voiceClip);
            }
        }

        currentLineIndex++;
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        videoActive = false;

        if (currentDialogueLines != null && currentLineIndex < currentDialogueLines.Length)
        {
            ShowDialogueLine();
            return;
        }

        index++;
        if (index < cutscenes.Length)
            PlayStep();
        else
            SceneManager.LoadScene(nextSceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
