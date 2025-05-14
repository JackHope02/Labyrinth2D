using UnityEngine;

public class HideOnStart : MonoBehaviour
{
    public GameObject objectToHide;

    void Start()
    {
        if (objectToHide != null)
        {
            objectToHide.SetActive(false);
        }
        else
        {
            Debug.LogWarning("objectToHide is not assigned in the inspector.");
        }
    }
}
