using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //Movement keys
    public KeyCode upKey;
    public KeyCode downKey;
    public KeyCode rightKey;
    public KeyCode leftKey;

    KeyCode lastClickedKey;

    public float speed = 16f;

    public GameObject wallPrefab;

    // Current Wall
    Collider2D wall;

    // Last Wall's End
    Vector2 lastWallEnd;

    public float rotationSpeed = 5.0f;

    public bool isPlayer1;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;
        lastClickedKey = upKey;
        spawnWall();
    }

    // Update is called once per frame
    void Update()
    {
        // Check for key presses
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
        // Save last wall's position
        lastWallEnd = transform.position;

        // Spawn a new Lightwall
        GameObject g = Instantiate(wallPrefab, transform.position, Quaternion.identity);
        wall = g.GetComponent<Collider2D>();

    }

    void rotateMotorcycle()
    {
        // Pobierz aktualną prędkość obiektu
        Vector3 velocity = GetComponent<Rigidbody2D>().velocity;

        if (velocity != Vector3.zero)
        {
            // Ustal kierunek ruchu na podstawie prędkości
            Vector3 direction = velocity.normalized;

            // Ustalenie kierunku ruchu (up, down, right, left)
            Vector3 upDirection = Vector3.up;
            Vector3 downDirection = Vector3.down;
            Vector3 rightDirection = Vector3.right;
            Vector3 leftDirection = Vector3.left;

            // Wybór najbliższego kierunku
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

            // Ograniczenie obrotu do zakresu od -90 do 90 stopni
            float angle = Mathf.Atan2(closestDirection.y, closestDirection.x) * Mathf.Rad2Deg;

            // Ograniczenie obrotu do kroków 90 stopni
            angle = Mathf.Round(angle / 90.0f) * 90.0f;

            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        }
    }

    void fitColliderBetween(Collider2D co, Vector2 a, Vector2 b)
    {
        // Calculate the Center Position
        co.transform.position = a + (b - a) * 0.5f;

        // Scale it (horizontally or vertically)
        float dist = Vector2.Distance(a, b);
        if (a.x != b.x)
            co.transform.localScale = new Vector2(dist + 1, 1);
        else
            co.transform.localScale = new Vector2(1, dist + 1);
    }

    void OnTriggerEnter2D(Collider2D co)
    {
        // Not the current wall?
        if (co != wall)
        {
            Destroy(gameObject);
            print("Player lost: " + name);
            //if (name == "player_cyan")
            //{
            //    SceneManager.LoadScene("Player2WinScreen", LoadSceneMode.Single);
            //}
            //else
            //{
            //    SceneManager.LoadScene("Player1WinScreen", LoadSceneMode.Single);
            //}
        }
    }
}
