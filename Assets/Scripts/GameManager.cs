using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerCyanPrefab;
    public GameObject playerMagentaPrefab;

    private GameObject playerCyan;
    private GameObject playerMagenta;

    private bool isSpaceKeyPressed = false;

    private void Start()
    {
        SpawnPlayers();
        Time.timeScale = 0f;
    }

    void Update()
    {
        // Sprawdzamy, czy jednocześnie naciśnięty jest klawisz Spacji
        if (Input.GetKey(KeyCode.Space))
        {
            // Jeśli wcześniej nie była naciśnięta, zmieniamy Time.timeScale na 1
            if (!isSpaceKeyPressed)
            {
                Time.timeScale = 1f;
                isSpaceKeyPressed = true;
            }
        }
        else
        {
            // Jeśli klawisz nie jest naciśnięty, resetujemy flagę
            isSpaceKeyPressed = false;
        }

        // Sprawdzamy, czy oba obiekty są zniszczone
        if (playerCyan == null || playerMagenta == null)
        {
            // Jeśli oba obiekty są zniszczone, tworzymy je ponownie
            SpawnPlayers();
            // Resetujemy Time.timeScale na 0
            Time.timeScale = 0f;
        }
    }

    void SpawnPlayers()
    {
        // Tworzymy obiekty graczy i przypisujemy je do zmiennych
        playerCyan = Instantiate(playerCyanPrefab);
        playerMagenta = Instantiate(playerMagentaPrefab);
    }
}