using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameOverGUI : MonoBehaviour {

    public static GameOverGUI instance;
    public GameObject gameOverPanel;
    public int highestFloor;
    public TextMeshProUGUI floorText;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }else { Destroy(this); }
        gameOverPanel.SetActive(false);
    }

    public void triggerGameOver()
    {
        gameOverPanel.SetActive(true);
        floorText.text = highestFloor.ToString();
    }
    public void tryAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void quitGame()
    {
        Application.Quit();
    }
}
