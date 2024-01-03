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
        }
        else if (Input.GetKeyDown(downKey) && (lastClickedKey != KeyCode.UpArrow && lastClickedKey != KeyCode.W))
        {
            GetComponent<Rigidbody2D>().velocity = -Vector2.up * speed;
            spawnWall();
            lastClickedKey = downKey;
        }
        else if (Input.GetKeyDown(rightKey) && (lastClickedKey != KeyCode.LeftArrow && lastClickedKey != KeyCode.A))
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
            spawnWall();
            lastClickedKey = rightKey;
        }
        else if (Input.GetKeyDown(leftKey) && (lastClickedKey != KeyCode.RightArrow && lastClickedKey != KeyCode.D))
        {
            GetComponent<Rigidbody2D>().velocity = -Vector2.right * speed;
            spawnWall();
            lastClickedKey = leftKey;
        }

        fitColliderBetween(wall, lastWallEnd, transform.position);
        rotateMotorcycle();

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
        float horizontalInput = isPlayer1 ? Input.GetAxis("Horizontal_Player1") : Input.GetAxis("Horizontal_Player2");
        float verticalInput = isPlayer1 ? Input.GetAxis("Vertical_Player1") : Input.GetAxis("Vertical_Player2");

        if (horizontalInput != 0 || verticalInput != 0)
        {
            Vector3 direction = new Vector3(horizontalInput, verticalInput, 0f);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

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
            print("Player lost: " + name);
            Destroy(gameObject);
            if(name == "player_cyan")
            {
                SceneManager.LoadScene("Player2WinScreen", LoadSceneMode.Single);
            }
            else
            {
                SceneManager.LoadScene("Player1WinScreen", LoadSceneMode.Single);
            }
        }
    }
}
