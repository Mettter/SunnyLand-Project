using System.Collections;
using UnityEngine;
using TMPro;

public class TargetObject : MonoBehaviour
{
    public TextMeshProUGUI tmpText;  // The TextMeshPro text object to be activated
    public float textSeconds = 3f;   // Duration for which the TMP text should be active
    public GameObject targetObject;  // The target object to be destroyed on collision

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == targetObject)
        {
            StartCoroutine(HandleCollision());
        }
    }

    private IEnumerator HandleCollision()
    {
        // Activate the TMP text
        Destroy(targetObject);
        tmpText.gameObject.SetActive(true);

        // Wait for textSeconds seconds
        yield return new WaitForSeconds(textSeconds);

        // Deactivate the TMP text
        tmpText.gameObject.SetActive(false);

        // Destroy the target object
        Destroy(targetObject);
    }
}
