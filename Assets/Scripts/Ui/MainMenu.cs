using UnityEngine;

namespace Ui
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private GameObject buttonPrefab;
        [SerializeField] private GameObject currentMenu;
        [SerializeField] private GameObject exitButton;

        private void Awake()
        {
            // make sure only current menu is enabled at start
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
            currentMenu.SetActive(true);
            // make sure that quit is only visible on pc
            #if !UNITY_STANDALONE
            exitButton.SetActive(false);
            #endif
            // TODO: add level buttons dynamically
        }

        public void SwitchMenu(GameObject newMenu)
        {
            currentMenu.SetActive(false);
            currentMenu = newMenu;
            currentMenu.SetActive(true);
        }

        public void Quit()
        {
            Application.Quit();
        }

        public void LoadLevel(string level)
        {
            LevelLoader.LoadLevel(level);
        }
    }
}