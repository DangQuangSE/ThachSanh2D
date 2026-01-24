using TMPro;
using UnityEngine;

public class SpeechBubbleUI : MonoBehaviour
{
    [SerializeField] private GameObject bubbleRoot;   
    [SerializeField] private TMP_Text bubbleText;    

    private void Awake()
    {
        Hide();
    }

    public void Show(string message)
    {
        if (bubbleRoot != null) bubbleRoot.SetActive(true);
        if (bubbleText != null) bubbleText.text = message;
    }

    public void Hide()
    {
        if (bubbleRoot != null) bubbleRoot.SetActive(false);
    }
}
