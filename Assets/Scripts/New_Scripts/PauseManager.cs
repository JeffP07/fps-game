using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour {

    private bool cursorLocked = false;
    public bool isPaused = false;

    [SerializeField]
    private GameObject escMenu;

    [SerializeField]
    private TextMeshProUGUI textMeshProUGUI;

    [SerializeField]
    private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        LockCursor();
        escMenu.SetActive(!cursorLocked);
        float sens = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MouseLook>().sensitivityVert;
        slider.value = sens;
        textMeshProUGUI.text = sens.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            PauseGame();
        }   
    }

    public void UpdateSensLabel(float sens) {
        textMeshProUGUI.text = sens.ToString();
    }

    public void LockCursor() {
        if (!cursorLocked) {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            cursorLocked = true;
        }
        else {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            cursorLocked = false;
        }
    }

    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PauseGame() {
        isPaused = !isPaused;
        LockCursor();
        escMenu.SetActive(!cursorLocked);
    }
}
