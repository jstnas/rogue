using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    public class GameOverPanel : MonoBehaviour
    {
        [SerializeField] private GameObject lostPanel;
        [SerializeField] private GameObject wonPanel;
        [SerializeField] private Text turnText;

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
    }
}
