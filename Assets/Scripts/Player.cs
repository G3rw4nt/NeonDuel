using UnityEngine;

public class Player : MonoBehaviour
{
    public KeyCode upKey;
    public KeyCode downKey;
    public KeyCode rightKey;
    public KeyCode leftKey;

    KeyCode lastClickedKey;

    public float speed = 16f;

    public GameObject wallPrefab;

    Collider2D wall;

    Vector2 lastWallEnd;

    public float rotationSpeed = 5.0f;

    public bool isPlayer1;

    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;
        lastClickedKey = upKey;
        spawnWall();
    }

    void Update()
    {
        if (Input.GetKeyDown(upKey) && (lastClickedKey != KeyCode.DownArrow && lastClickedKey != KeyCode.S))
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;
            spawnWall();
            lastClickedKey = upKey;
            rotateMotorcycle();
        }
        else if (Input.GetKeyDown(downKey) && (lastClickedKey != KeyCode.UpArrow && lastClickedKey != KeyCode.W))
        {
            GetComponent<Rigidbody2D>().velocity = -Vector2.up * speed;
            spawnWall();
            lastClickedKey = downKey;
            rotateMotorcycle();
        }
        else if (Input.GetKeyDown(rightKey) && (lastClickedKey != KeyCode.LeftArrow && lastClickedKey != KeyCode.A))
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
            spawnWall();
            lastClickedKey = rightKey;
            rotateMotorcycle();
        }
        else if (Input.GetKeyDown(leftKey) && (lastClickedKey != KeyCode.RightArrow && lastClickedKey != KeyCode.D))
        {
            GetComponent<Rigidbody2D>().velocity = -Vector2.right * speed;
            spawnWall();
            lastClickedKey = leftKey;
            rotateMotorcycle();
        }

        fitColliderBetween(wall, lastWallEnd, transform.position);

    }

    void spawnWall()
    {
        lastWallEnd = transform.position;

        GameObject g = Instantiate(wallPrefab, transform.position, Quaternion.identity);
        wall = g.GetComponent<Collider2D>();

    }

    void rotateMotorcycle()
    {
        Vector3 velocity = GetComponent<Rigidbody2D>().velocity;

        if (velocity != Vector3.zero)
        {
            Vector3 direction = velocity.normalized;

            Vector3 upDirection = Vector3.up;
            Vector3 downDirection = Vector3.down;
            Vector3 rightDirection = Vector3.right;
            Vector3 leftDirection = Vector3.left;

            Vector3 closestDirection = Vector3.zero;
            float angleUp = Vector3.Angle(direction, upDirection);
            float angleDown = Vector3.Angle(direction, downDirection);
            float angleRight = Vector3.Angle(direction, rightDirection);
            float angleLeft = Vector3.Angle(direction, leftDirection);

            float minAngle = Mathf.Min(angleUp, angleDown, angleRight, angleLeft);

            if (minAngle == angleUp)
                closestDirection = upDirection;
            else if (minAngle == angleDown)
                closestDirection = downDirection;
            else if (minAngle == angleRight)
                closestDirection = rightDirection;
            else
                closestDirection = leftDirection;

            float angle = Mathf.Atan2(closestDirection.y, closestDirection.x) * Mathf.Rad2Deg;

            angle = Mathf.Round(angle / 90.0f) * 90.0f;

            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        }
    }

    void fitColliderBetween(Collider2D co, Vector2 a, Vector2 b)
    {
        co.transform.position = a + (b - a) * 0.5f;

        float dist = Vector2.Distance(a, b);
        if (a.x != b.x)
            co.transform.localScale = new Vector2(dist + 1, 1);
        else
            co.transform.localScale = new Vector2(1, dist + 1);
    }

    void OnTriggerEnter2D(Collider2D co)
    {
        if (co != wall)
        {
            Destroy(gameObject);
        }
    }
}
