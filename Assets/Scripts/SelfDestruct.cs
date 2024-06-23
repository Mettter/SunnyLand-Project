using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    public float deathTime = 5f;  // Время до уничтожения в секундах

    void Start()
    {
        // Уничтожаем объект через deathTime секунд
        Destroy(gameObject, deathTime);
    }
}