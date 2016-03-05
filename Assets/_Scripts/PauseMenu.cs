using UnityEngine;
using System.Collections;

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
        Application.LoadLevel(1);
    }
    public void OnQuitClick()
    {
        Time.timeScale = 1;
        Application.LoadLevel(0);
    }
}
