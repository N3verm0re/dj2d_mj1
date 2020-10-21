using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject menuScreen;
    public GameObject optionsScreen;
    public GameObject learnScreen;
    public AudioSource audioSource;
    public Slider audioSlider;

    private void Awake()
    {
        menuScreen.SetActive(true);
        optionsScreen.SetActive(false);
        learnScreen.SetActive(false);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Options()
    {
        optionsScreen.SetActive(true);
        menuScreen.SetActive(false);
    }

    public void Learn()
    {
        learnScreen.SetActive(true);
        menuScreen.SetActive(false);
    }
    public void backLearn()
    {
        menuScreen.SetActive(true);
        learnScreen.SetActive(false);
    }

    public void backOptions()
    {
        menuScreen.SetActive(true);
        optionsScreen.SetActive(false);
    }
    public void QuitGame()
    {
        Debug.Log("Game Quitting...");
        Application.Quit();
    }

    public void ChangeAudio()
    {
        audioSource.volume = audioSlider.value;
    }
}
