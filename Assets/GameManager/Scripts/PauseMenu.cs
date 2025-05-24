using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;


public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject deathMenuUI;
    public GameObject winMenuUI;

    public GameObject InGameUI;

    public GameObject SettingsPanel;
    public GameObject cameraButton;
    public GameObject resumeButton;
    public GameObject retryButton;

    public GameObject startButton;

    private bool isDead = false;

    PlayerManager playerManager;
    InputManager inputManager;
    PlayerLocomotion playerLocomotion;

    private void Awake()
    {
        playerManager = FindAnyObjectByType<PlayerManager>();
        inputManager = FindAnyObjectByType<InputManager>();
        playerLocomotion = FindAnyObjectByType<PlayerLocomotion>();

    }

    // Update is called once per frame
    void Update()
    {
        if(isDead || playerManager == null) return;
        {
            if (inputManager.pause_input)
            {
                inputManager.pause_input = false;
                if (GameIsPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
               
            }
        }
        
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        EnablePlayerInputs();

        EventSystem.current.SetSelectedGameObject(null);
        

        if (InGameUI != null)
        {
            InGameUI.SetActive(true);
        }
    }

    

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        DisablePlayerInputs();

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(resumeButton.gameObject);
        

        if (InGameUI != null)
        {
            InGameUI.SetActive(false);
        }

        
    }



    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("DevScene");

        Resume();

    }

    public void RestartHTP()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("TutorialScene");

        Resume();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    
    private void DisablePlayerInputs()
    {
        if (playerLocomotion != null)
        {
            playerLocomotion.enabled = false;  
        }

    }

    public void OpenSettings()
    {
        if(SettingsPanel != null)
        {
            SettingsPanel.SetActive(true);
            
        }
        if(pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
        }
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(cameraButton.gameObject);
    }

    public void CloseSettings()
    {
        if (SettingsPanel != null)
        {
            SettingsPanel.SetActive(false);
            
        }
        if(pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(true);
        }
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(resumeButton.gameObject);
    }

    

    private void EnablePlayerInputs()
    {
        if (playerLocomotion != null)
        {
            playerLocomotion.enabled = true;  
        }
    }

    public void TriggerDeath()
    {
        Time.timeScale = 0f;
        GameIsPaused = true;
        isDead = true;

        deathMenuUI.SetActive(true);

        pauseMenuUI.SetActive(false);

        if (InGameUI != null)
        {   
            InGameUI.SetActive(false);
        }

        DisablePlayerInputs();

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(retryButton.gameObject);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void TriggerWin()
    {
        Time.timeScale = 0f;
        GameIsPaused = true;
        isDead = true;

        winMenuUI.SetActive(true);

        pauseMenuUI.SetActive(false);

        if (InGameUI != null)
        {
            InGameUI.SetActive(false);
        }

        DisablePlayerInputs();

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(retryButton.gameObject);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
