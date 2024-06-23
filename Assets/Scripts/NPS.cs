using System.Collections;
using UnityEngine;
using TMPro;

public class NPS : MonoBehaviour
{
    public GameObject playerObject;  // The player object that will be paused
    public TextMeshProUGUI tmpText;  // The TextMeshPro text object to be activated
    public float textS = 3f;         // Duration for which the TMP text should be active
    public GameObject goalObject;    // The object to be activated

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == playerObject)
        {
            StartCoroutine(HandleCollision());
        }
    }

    private IEnumerator HandleCollision()
    {
        // Activate the TMP text
        tmpText.gameObject.SetActive(true);

        // Wait for textS seconds
        yield return new WaitForSeconds(textS);

        // Deactivate the TMP text
        tmpText.gameObject.SetActive(false);

        // Activate the goal object
        if (goalObject != null)
        {
            goalObject.SetActive(true);
        }
    }
}
