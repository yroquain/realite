using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public GameObject Pause_Menu;
	public void OnContinueClick()
    {
        Time.timeScale = 1;
        Pause_Menu.SetActive(false);
    }
    public void OnRestartClick()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
    public void OnQuitClick()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
