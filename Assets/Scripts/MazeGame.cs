using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MazeGame : MonoBehaviour
{
    [SerializeField] int winDiamondNumber = 10;
    [SerializeField] TextMeshProUGUI diamondLabel;
    [SerializeField] TextMeshProUGUI gameOverLabel;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject helpPanel;

    private static MazeGame _instance;
    public static MazeGame Instance { get { return _instance; } }

    int currentDiamonds = 0;
    private void Awake()
    {
        _instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        diamondLabel.text = "Diamonds\n" + "0/" + winDiamondNumber;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnDiamondCollected()
    {
        diamondLabel.text = "Diamonds\n" + (++currentDiamonds) + "/" + winDiamondNumber;
        if (currentDiamonds == winDiamondNumber) Win();
    }

    private void Win()
    {
        Time.timeScale = 0;
        gameOverLabel.text = "Game clear";
        gameOverPanel.SetActive(true);
    }

    public void Loose()
    {
        Time.timeScale = 0;
        gameOverLabel.text = "Game over";
        gameOverPanel.SetActive(true);
    }

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void ShowInstruction()
    {
        helpPanel.SetActive(true);
    }

    public void Resume()
    {
        helpPanel.SetActive(false);
    }
}
