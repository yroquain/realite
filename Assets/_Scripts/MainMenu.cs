using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public GameObject CanvasOption;
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
        Application.LoadLevel(1);
    }
    public void OnChangeValue()
    {
        LightSaberImage.GetComponent<Image>().sprite = SpriteListe[MyDrop.value];
    }
}
