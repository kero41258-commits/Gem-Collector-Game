using UnityEngine;
using TMPro;

public class gem : MonoBehaviour
{
    [Header("Letter Display")]
    public char letter = 'A';
    public TMP_Text letterText;

    [Header("Visual Settings")]
    [Tooltip("Font size of the letter on the falling gem.")]
    public float fontSize = 4.5f;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Keep the gem upright so the letter remains readable to the player
            rb.freezeRotation = true;
        }

        EnsureTextComponent();
    }

    private void EnsureTextComponent()
    {
        if (letterText == null)
        {
            letterText = GetComponentInChildren<TMP_Text>();
        }

        // If no TMP text exists on the prefab, automatically create a 3D label
        if (letterText == null)
        {
            GameObject textObj = new GameObject("LetterLabel");
            textObj.transform.SetParent(transform, false);
            textObj.transform.localPosition = new Vector3(0f, 0f, -0.65f);

            TextMeshPro tmp = textObj.AddComponent<TextMeshPro>();
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.fontSize = fontSize;
            tmp.color = Color.white;
            tmp.fontStyle = FontStyles.Bold;

            letterText = tmp;
        }
        else
        {
            if (letterText is TextMeshPro tmp)
            {
                tmp.fontSize = fontSize;
            }
        }
    }

    /// <summary>
    /// Sets the letter carried by this gem and updates the text display.
    /// </summary>
    public void SetLetter(char newLetter)
    {
        letter = char.ToUpper(newLetter);
        EnsureTextComponent();

        if (letterText != null)
        {
            letterText.text = letter.ToString();
        }
    }

    private void LateUpdate()
    {
        // Keep the letter text facing forward towards the camera
        if (letterText != null)
        {
            letterText.transform.rotation = Quaternion.identity;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.LetterCaught(letter);
            }

            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.LetterMissed(letter);
            }

            Destroy(gameObject);
        }
    }
}
