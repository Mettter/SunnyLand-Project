using UnityEngine;

public class SpawnOnDestroy : MonoBehaviour
{
    public GameObject objectToSpawn;  // Объект, который нужно спавнить

    void OnDestroy()
    {
        // Проверка, установлен ли объект для спавна
        if (objectToSpawn != null)
        {
            // Спавним объект в позиции текущего объекта
            Instantiate(objectToSpawn, transform.position, transform.rotation);
        }
    }
}