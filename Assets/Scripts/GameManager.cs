using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject playerCyanPrefab;
    public GameObject playerMagentaPrefab;

    public GameObject pressKeyText;

    private GameObject playerCyan;
    private GameObject playerMagenta;

    public TextMeshProUGUI cyanPointsText;
    public TextMeshProUGUI magentaPointsText;

    public bool vsCPU;
    

    int cyanPoints;
    int magentaPoints;

    private bool isSpaceKeyPressed = false;

    private void Start()
    {
        cyanPoints = 0;
        magentaPoints = 0;
        SpawnPlayers();
        Time.timeScale = 0f;
    }

    void Update()
    {
        cyanPointsText.text = cyanPoints.ToString();
        magentaPointsText.text = magentaPoints.ToString();

        if (Input.GetKey(KeyCode.Space))
        {
            if (!isSpaceKeyPressed)
            {
                Time.timeScale = 1f;
                isSpaceKeyPressed = true;
                pressKeyText.SetActive(false);
            }
        }
        else
        {
            isSpaceKeyPressed = false;
        }

        if (playerCyan == null || playerMagenta == null)
        {
            CheckPoints();
            CleanMap();
            SpawnPlayers();
            Time.timeScale = 0f;
            pressKeyText.SetActive(true);
        }

        if(Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }

    }

    void SpawnPlayers()
    {
        Destroy(playerCyan);
        Destroy(playerMagenta);

        playerCyan = Instantiate(playerCyanPrefab);
        playerMagenta = Instantiate(playerMagentaPrefab);
    }
    void CleanMap()
    {
        var clones = GameObject.FindGameObjectsWithTag("wall");
        foreach(var clone in clones)
        {
            Destroy(clone);
        }
    }

    void CheckPoints()
    {
        if(playerCyan == null)
        {
            magentaPoints++;
        }
        else if(playerMagenta == null)
        {
            cyanPoints++;
        }
        if(cyanPoints == 5)
        {
            if(vsCPU)
                SceneManager.LoadScene("YouWinScreen", LoadSceneMode.Single);
            else
                SceneManager.LoadScene("Player1WinScreen", LoadSceneMode.Single);
        }
        else if(magentaPoints == 5)
        {
            if (vsCPU)
                SceneManager.LoadScene("YouLostScreen", LoadSceneMode.Single);
            else
                SceneManager.LoadScene("Player2WinScreen", LoadSceneMode.Single);
        }
    }
}