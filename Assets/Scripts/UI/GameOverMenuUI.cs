using Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class GameOverMenuUI : MonoBehaviour
    {
        public void Retry()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void QuitToMainMenu()
        {
            SceneManager.LoadScene((int)SceneEnum.MAIN_MENU);
        }
    }
}