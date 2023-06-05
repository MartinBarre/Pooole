using Utils;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class MainMenuUI : MonoBehaviour
    {
        public GameObject mainMenu;
        public GameObject settingsMenu;
        public GameObject levelSelection;

        [HideInInspector] public int egg1;
        [HideInInspector] public int egg2;
        [HideInInspector] public int egg3;
        [HideInInspector] public int totEgg1;
        [HideInInspector] public int totEgg2;
        [HideInInspector] public int totEgg3;
        [HideInInspector] public bool isLevel2;
        [HideInInspector] public bool isLevel3;

        private void Start()
        {
            OpenMainMenu();
            AssignPlayerPrefs();
        }

        private void AssignPlayerPrefs()
        {
            totEgg1 = PlayerPrefs.GetInt("totEgg1", -1);
            totEgg2 = PlayerPrefs.GetInt("totEgg2", -1);
            totEgg3 = PlayerPrefs.GetInt("totEgg3", -1);
            egg1 = PlayerPrefs.GetInt("egg1", -1);
            egg2 = PlayerPrefs.GetInt("egg2", -1);
            egg3 = PlayerPrefs.GetInt("egg3", -1);
            isLevel2 = PlayerPrefs.GetInt("2", 0) == 1;
            isLevel3 = PlayerPrefs.GetInt("3", 0) == 1;
        }

        public void QuitToDesktop()
        {
            Application.Quit();
        }

        public void OpenMainMenu()
        {
            mainMenu.SetActive(true);
            settingsMenu.SetActive(false);
            levelSelection.SetActive(false);
        }

        public void OpenSettings()
        {
            settingsMenu.SetActive(true);
            mainMenu.SetActive(false);
            levelSelection.SetActive(false);
        }

        public void ApplySettings()
        {
            OpenMainMenu();
        }

        public void OpenLevelSelection()
        {
            levelSelection.SetActive(true);
            mainMenu.SetActive(false);
            settingsMenu.SetActive(false);

            var body = levelSelection.transform.GetChild(1).GetChild(0).transform;
            body.GetChild(1).gameObject.GetComponent<Button>().interactable = isLevel2;
            body.GetChild(2).gameObject.GetComponent<Button>().interactable = isLevel3;


            int[] eggs = { egg1, egg2, egg3 };
            int[] tot = { totEgg1, totEgg2, totEgg3 };

            for (int i = 0; i < 3; i++)
            {
                var oeuf = body.GetChild(i).transform.GetChild(1).gameObject;
                if (eggs[i] > 0)
                {
                    oeuf.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = eggs[i].ToString();
                    oeuf.transform.GetChild(3).GetComponent<TMPro.TextMeshProUGUI>().text = tot[i].ToString();
                    oeuf.SetActive(true);
                }
                else
                {
                    oeuf.SetActive(false);
                }
            }
        }

        public void LoadLevel1()
        {
            SceneManager.LoadSceneAsync((int)SceneEnum.LEVEL_1);
        }
        
        public void LoadLevel2()
        {
            SceneManager.LoadSceneAsync((int)SceneEnum.LEVEL_2);
        }
        
        public void LoadLevel3()
        {
            SceneManager.LoadSceneAsync((int)SceneEnum.LEVEL_3);
        }
    }
}