using UnityEngine;

public class ChasePlayer : MonoBehaviour
{
    public Transform playerObject;    // Ссылка на объект игрока
    public Transform distanceObject;  // Ссылка на объект для измерения расстояния
    public float speed = 5f;          // Скорость преследования
    public float chaseDistance = 10f; // Расстояние для начала преследования

    private Rigidbody2D rb;           // Ссылка на Rigidbody2D компонент
    private Vector2 initialPosition;  // Начальная позиция объекта
    private bool isFacingRight = true; // Направление объекта

    void Start()
    {
        // Получите ссылку на Rigidbody2D
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component is missing.");
            return;
        }

        // Отключите гравитацию
        rb.gravityScale = 0;

        // Сохраните начальную позицию объекта
        initialPosition = rb.position;
    }

    void Update()
    {
        if (playerObject == null || distanceObject == null)
        {
            Debug.LogError("PlayerObject or DistanceObject is not assigned.");
            return;
        }

        // Вычислите расстояние между объектом скрипта и объектом для измерения расстояния
        float distanceToDistanceObject = Vector2.Distance(transform.position, distanceObject.position);
        float distanceToPlayer = Vector2.Distance(transform.position, playerObject.position);

        Debug.Log("Distance to distanceObject: " + distanceToDistanceObject);
        Debug.Log("Distance to playerObject: " + distanceToPlayer);

        // Проверьте расстояния и выполняйте соответствующие действия
        if (distanceToDistanceObject < chaseDistance && distanceToPlayer <= chaseDistance * 2)
        {
            if (distanceToPlayer <= chaseDistance)
            {
                Debug.Log("Within chase distance. Starting to chase.");
                // Вычислите направление к игроку
                Vector2 direction = (playerObject.position - transform.position).normalized;

                // Двигайтесь к игроку, используя Rigidbody2D
                rb.MovePosition(rb.position + direction * speed * Time.deltaTime);

                // Флипаем объект в зависимости от направления движения
                if ((direction.x > 0 && !isFacingRight) || (direction.x < 0 && isFacingRight))
                {
                    Flip();
                }
            }
            else
            {
                Debug.Log("Player is too far. Returning to initial position.");
                // Вычислите направление к начальной позиции
                Vector2 directionToInitial = (initialPosition - rb.position).normalized;

                // Двигайтесь к начальной позиции, используя Rigidbody2D
                rb.MovePosition(rb.position + directionToInitial * speed * Time.deltaTime);

                // Флипаем объект в зависимости от направления движения
                if ((directionToInitial.x > 0 && !isFacingRight) || (directionToInitial.x < 0 && isFacingRight))
                {
                    Flip();
                }
            }
        }
        else
        {
            Debug.Log("Outside chase distance. Returning to initial position.");
            // Вычислите направление к начальной позиции
            Vector2 directionToInitial = (initialPosition - rb.position).normalized;

            // Двигайтесь к начальной позиции, используя Rigidbody2D
            rb.MovePosition(rb.position + directionToInitial * speed * Time.deltaTime);

            // Флипаем объект в зависимости от направления движения
            if ((directionToInitial.x > 0 && !isFacingRight) || (directionToInitial.x < 0 && isFacingRight))
            {
                Flip();
            }
        }

        // Отладочные сообщения для отслеживания позиции
        Debug.Log("Current Position: " + rb.position);
    }

    void Flip()
    {
        // Меняем направление
        isFacingRight = !isFacingRight;

        // Меняем масштаб по оси X для флипа
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}
