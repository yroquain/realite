using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public GameObject CanvasOption;
    public GameObject CanvasStart;
    public Dropdown MyDrop;
    public Sprite[] SpriteListe;
    public Image LightSaberImage;

    public void OnOptionClick()
    {
        LightSaberImage.GetComponent<Image>().sprite = SpriteListe[(int)PlayerPrefs.GetFloat("SaberColor")];
        MyDrop.value = (int)PlayerPrefs.GetFloat("SaberColor");
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
        CanvasStart.SetActive(true);
    }
    public void OnChangeValue()
    {
        LightSaberImage.GetComponent<Image>().sprite = SpriteListe[MyDrop.value];
    }
    public void OnJediClick()
    {
        PlayerPrefs.SetFloat("Type", 1);
        SceneManager.LoadScene(1);
    }
    public void OnSithClick()
    {
        PlayerPrefs.SetFloat("Type", 0);
        SceneManager.LoadScene(1);
    }
}
