using Saves;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private string nextLevel;

    public string GetNextLevel()
    {
        return nextLevel;
    }

    public void UnlockLevel()
    {
        SaveManager.ActiveState.UnlockLevel(nextLevel);
    }
}