using UnityEngine;

namespace Ui
{
    public class RestartButton : MonoBehaviour
    {
        public void Restart()
        {
            LevelLoader.RestartLevel();
        }
    }
}