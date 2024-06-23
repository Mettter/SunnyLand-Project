using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatAbility : MonoBehaviour
{
    [SerializeField] GameObject EffectObject;
    public float abilityDuration;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name);
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player detected");
            StartCoroutine(Ability());
        }
    }

    private IEnumerator Ability()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if(player == null)
        {
            Debug.LogError("Player with tag 'Player' not found!");
            yield break;
        }

        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
        if(playerRb == null)
        {
            Debug.LogError("Rigidbody2D component not found on player!");
            yield break;
        }

        Debug.Log("Ability activated: setting gravityScale to -1");
        playerRb.gravityScale = -1; // Set gravity to -1
        Instantiate(EffectObject, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(abilityDuration);
        Debug.Log("Ability duration ended: resetting gravityScale to 3");
        playerRb.gravityScale = 3; // Reset gravity to 3
    }
}
