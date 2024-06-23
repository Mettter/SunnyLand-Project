using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHP = 100;
    private int currentHP;

    public float invincibilityDuration = 2f;
    private bool isInvincible = false;

    public TMP_Text WIN;
    public TMP_Text Tutor;
    public TMP_Text DEATH;
    public TMP_Text coinText;
    public TMP_Text hpText;
    public TMP_Text eternalCoinText; // TMP text for displaying eternalCoin
    public Button MENU;
    public Button RESTART;

    public GameObject star1;
    public GameObject star2;
    public GameObject star3;

    private int coinCount = 0;
    private int starrCount = 0;

    private Rigidbody2D rb;

    // Eternal coin variable and key for PlayerPrefs
    private int eternalCoin = 0;
    private string eternalCoinKey = "EternalCoin";
    private bool isSKeyDown = false;
    private bool yKeyPressed = false;

    private Movement playerMovement; // Reference to the Movement script

    void Start()
    {
        currentHP = maxHP;
        rb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<Movement>(); // Get the Movement script component
        WIN.gameObject.SetActive(false);
        DEATH.gameObject.SetActive(false);
        RESTART.gameObject.SetActive(false);
        MENU.gameObject.SetActive(false);

        // Ensure stars are initially inactive
        star1.SetActive(false);
        star2.SetActive(false);
        star3.SetActive(false);

        // Load eternal coin value from PlayerPrefs
        eternalCoin = PlayerPrefs.GetInt(eternalCoinKey, 0);
        UpdateCoinText();
    }

    public void TakeDamage(int damage, Vector2 knockbackForce, float stunDuration)
    {
        if (isInvincible) return;

        currentHP -= damage;
        if (currentHP <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(InvincibilityCoroutine());
            ApplyKnockback(knockbackForce);
            if (stunDuration > 0)
            {
                StartCoroutine(Stun(stunDuration));
            }
        }
    }

    public void Heal(int amount)
    {
        currentHP += amount;
        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
    }

    public int GetCurrentHP()
    {
        return currentHP;
    }

    private void Die()
    {
        Debug.Log(gameObject.name + " has died.");
        gameObject.SetActive(false);
        DEATH.gameObject.SetActive(true);
        Time.timeScale = 0;
        RESTART.gameObject.SetActive(true);
        MENU.gameObject.SetActive(true);
    }

    private IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibilityDuration);
        isInvincible = false;
    }

    private void ApplyKnockback(Vector2 force)
    {
        rb.velocity = Vector2.zero; // Reset velocity before applying knockback force
        rb.AddForce(force, ForceMode2D.Impulse);
    }

    private IEnumerator Stun(float duration)
    {
        playerMovement.enabled = false;
        yield return new WaitForSeconds(duration);
        playerMovement.enabled = true;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Win"))
        {
            WIN.gameObject.SetActive(true);
            Time.timeScale = 0;
            RESTART.gameObject.SetActive(true);
            MENU.gameObject.SetActive(true);

            // Activate stars based on starrCount
            if (starrCount >= 1)
            {
                star1.SetActive(true);
            }
            if (starrCount >= 2)
            {
                star2.SetActive(true);
            }
            if (starrCount >= 3)
            {
                star3.SetActive(true);
            }

            // Save eternalCoin to PlayerPrefs
            PlayerPrefs.SetInt(eternalCoinKey, eternalCoin);
            PlayerPrefs.Save();
        }

        if (collision.CompareTag("TutorEnd"))
        {
            Tutor.gameObject.SetActive(false);
        }

        if (collision.CompareTag("TutorStart"))
        {
            Tutor.gameObject.SetActive(true);
        }

        if (collision.CompareTag("Coin"))
        {
            coinCount++;
            eternalCoin++; // Increment eternal coin on collecting a coin
            UpdateCoinText();
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("StarAdder"))
        {
            starrCount++;
            Destroy(collision.gameObject); // Optionally destroy the StarAdder object after collision
        }

        if (collision.CompareTag("Chest"))
        {
            coinCount += 5;
            coinCount += Random.Range(1, 10); // Add random number of coins from 1 to 10
            UpdateCoinText();
            Destroy(collision.gameObject); // Destroy the chest object
        }
    }

    private void UpdateCoinText()
    {
        coinText.text = "Coins: " + coinCount;
    }

    private void UpdateHPText()
    {
        hpText.text = "Health: " + currentHP;
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1); // Ensure scene 1 is the main menu scene
    }

    public void LoadLVL()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0); // Ensure scene 0 is the desired level
    }

    // Method to set the starrCount
    public void SetStarrCount(int count)
    {
        starrCount = count;
    }

    private void FixedUpdate()
    {
        UpdateHPText();

        // Check if the "S" key is held down
        isSKeyDown = Input.GetKey(KeyCode.S);

        // Show or hide eternal coin text based on "S" key status
        eternalCoinText.gameObject.SetActive(isSKeyDown);

        // Update eternal coin text
        eternalCoinText.text = "Eternal Coins: " + eternalCoin;

        // Check if the "Y" key is pressed down
        if (Input.GetKeyDown(KeyCode.Y))
        {
            yKeyPressed = true;
        }

        // Check if the "Y" key is released and it was pressed down before
        if (Input.GetKeyUp(KeyCode.Y) && yKeyPressed)
        {
            yKeyPressed = false; // Reset the flag
        }
    }
}
