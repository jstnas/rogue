using Saves;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    public class GameOverPanel : MonoBehaviour
    {
        private const string MainMenu = "Scenes/MainMenu";
        [SerializeField] private GameObject lostPanel;
        [SerializeField] private GameObject wonPanel;
        [SerializeField] private LevelManager levelManager;
        [SerializeField] private Text turnText;

        private void Start()
        {
            // make sure that both panels are disabled at the start
            lostPanel.SetActive(false);
            wonPanel.SetActive(false);
        }

        public void Display(bool won)
        {
            if (won)
            {
                var nextLevel = levelManager.GetNextLevel();
                SaveManager.ActiveState.UnlockLevel(nextLevel);
                wonPanel.SetActive(true);
            }
            else
                lostPanel.SetActive(true);
        }

        public void SetTurns(int turns)
        {
            turnText.text = $"Turns: {turns}";
        }

        public void Restart()
        {
            // action for restart button
            LevelLoader.RestartLevel();
        }

        public void NextLevel()
        {
            // action for next level button
            var nextLevel = levelManager.GetNextLevel();
            LevelLoader.LoadLevel(nextLevel);
        }

        public void Return()
        {
            LevelLoader.LoadLevel(MainMenu);
        }
    }
}