using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    [SerializeField] private GameObject retryScreen;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private int sceneCount = 7;
    [SerializeField] List<Sprite> letters;
    [SerializeField] private Image letterImage;

    private Sprite currentLetter;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RetryScene();
        }
    }

    public static GameManager Instance {
        get 
        { 
            if (instance == null)
            {
                instance = FindAnyObjectByType<GameManager>();
            }
            return instance;
        }
    }

    [SerializeField] private PlayerController playerController;

    public PlayerController PlayerController => playerController;

    private void Awake()
    {
        currentLetter = letters[SceneManager.GetActiveScene().buildIndex];
        letterImage.sprite = currentLetter;
        letterImage.SetNativeSize();
    }

    public void NextScene()
    {
        int nextScene = (SceneManager.GetActiveScene().buildIndex + 1) % sceneCount;
        SceneManager.LoadScene(nextScene);
    }

    public void RetryScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GameOverStart()
    {
        retryScreen.SetActive(true);

    }

    public void WinStart()
    {
        winScreen.SetActive(true);
    }


}
