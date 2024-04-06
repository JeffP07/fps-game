using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPManager : MonoBehaviour
{
    [SerializeField]
    List<GameObject> sprites = new List<GameObject>();

    private PlayerCharacter player;
    private PauseManager pauseManager;
    [SerializeField] private GameObject deathScreen;
    // Start is called before the first frame update
    void Start()
    {
        player = FindAnyObjectByType<PlayerCharacter>();
        pauseManager = FindAnyObjectByType<PauseManager>();
        deathScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        int health = player.getHealth();
        if (health < 5) {
            sprites[health].SetActive(false);
        }
        if (health <= 0) {
            sprites[0].SetActive(false);
            pauseManager.isPaused = true;
            deathScreen.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            return;
        }
    }
}
