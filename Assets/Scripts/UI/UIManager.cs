using Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        public PauseMenuUI pauseMenu;
        public SettingsMenuUI settingsMenu;
        public GameOverMenuUI gameOverMenu;
        public EndLevelMenuUI endLevelMenu;

        private void Awake()
        {
            GameManager.OnGameOver += OpenGameOver;
            PlayerInput.OnPausePressed += OpenPause;
            EndLevel.OnLevelFinished += OpenEndLevel;
        }
        
        public void OpenPause()
        {
            CloseAllMenu();
            pauseMenu.gameObject.SetActive(true);
            Time.timeScale = 0;
        }

        public void ClosePauseMenu()
        {
            CloseAllMenu();
            Time.timeScale = 1;
        }

        public void OpenSettings()
        {
            CloseAllMenu();
            settingsMenu.gameObject.SetActive(true);
        }

        private void OpenGameOver()
        {
            CloseAllMenu();
            gameOverMenu.gameObject.SetActive(true);
        }

        private void OpenEndLevel()
        {
            CloseAllMenu();
            endLevelMenu.gameObject.SetActive(true);
        }

        public void QuitToMainMenu()
        {
            SceneManager.LoadSceneAsync((int)SceneEnum.MAIN_MENU);
        }
        
        private void CloseAllMenu()
        {
            pauseMenu.gameObject.SetActive(false);
            settingsMenu.gameObject.SetActive(false);
            gameOverMenu.gameObject.SetActive(false);
            endLevelMenu.gameObject.SetActive(false);
        }
    }
}