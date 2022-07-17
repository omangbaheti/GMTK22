using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowDiceRoll : MonoBehaviour
{
    private const string DiceRollSceneName = "DiceRoll";

    private void Start()
    {
        if (SceneManager.GetSceneByName(DiceRollSceneName).name != DiceRollSceneName)
        {
            SceneManager.LoadScene(DiceRollSceneName, LoadSceneMode.Additive);
        }
    }
}
