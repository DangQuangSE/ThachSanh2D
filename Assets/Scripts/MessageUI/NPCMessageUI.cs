using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class NpcDialogue : MonoBehaviour
{
    [Header("Dialogue Lines")]
    [TextArea(2,4)]
    [SerializeField] private string[] lines;

    [Header("References")]
    [SerializeField] private SpeechBubbleUI bubbleUI;

    private bool playerInRange;
    private int index;

    private void Reset()
    {
        var col = GetComponent<Collider2D>();
        col.isTrigger = true;
    }

    private void Update()
    {
        if (!playerInRange) return;
        if (bubbleUI == null) return;

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            NextLine();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        playerInRange = true;
        index = 0;

        if (lines != null && lines.Length > 0)
            bubbleUI.Show(lines[index]);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        playerInRange = false;
        index = 0;

        if (bubbleUI != null)
            bubbleUI.Hide();
    }

    private void NextLine()
    {
        if (lines == null || lines.Length == 0) return;

        index++;
        if (index >= lines.Length)
        {
            bubbleUI.Hide();
            index = 0; 
            return;
        }

        bubbleUI.Show(lines[index]);
    }
}
