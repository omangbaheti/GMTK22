using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MenuAndSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    
    public void StartGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ShowPanel(GameObject targetPanel)
    {
        if (Time.timeScale == 0f)
            Time.timeScale = 1f;
        else
            Time.timeScale = 0f;
        
        if(targetPanel!=null)
        {
            targetPanel.SetActive(true);
        }
        gameObject.SetActive(false);
    }
    
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", volume);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void HideSettingsMenu()
    {
        gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
