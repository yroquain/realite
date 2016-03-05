using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public GameObject CanvasOption;
    public Dropdown MyDrop;

    public void OnOptionClick()
    {
        CanvasOption.SetActive(true);
    }
    public void OnReturnClick()
    {
        CanvasOption.SetActive(false);
    }
    public void OnSaveClick()
    {
        PlayerPrefs.SetFloat("SaberColor", MyDrop.value);
        CanvasOption.SetActive(false);
    }
    public void OnLevelClick()
    {
        Application.LoadLevel(1);
    }
}
