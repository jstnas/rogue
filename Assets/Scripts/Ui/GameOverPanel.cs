using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    public class GameOverPanel : MonoBehaviour
    {
        private const string MainMenu = "Scenes/MainMenu";
        [SerializeField] private GameObject lostPanel;
        [SerializeField] private GameObject wonPanel;
        [SerializeField] private Text turnText;
        [SerializeField] private string nextLevel;

        private void Start()
        {
            // make sure that both panels are disabled at the start
            lostPanel.SetActive(false);
            wonPanel.SetActive(false);
        }

        public void Display(bool won)
        {
            if (won)
                wonPanel.SetActive(true);
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
            LevelLoader.LoadLevel(nextLevel);
        }

        public void Return()
        {
            LevelLoader.LoadLevel(MainMenu);
        }
    }
}