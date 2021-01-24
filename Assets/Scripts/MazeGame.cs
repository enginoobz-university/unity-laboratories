using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MazeGame : MonoBehaviour
{
    [SerializeField] int winDiamondNumber = 10;
    [SerializeField] TextMeshProUGUI diamondLabel;
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

    }
}
