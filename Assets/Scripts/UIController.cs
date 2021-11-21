using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI points;
    [SerializeField]
    private GameObject gameWonLost;
    [SerializeField]
    private TextMeshProUGUI speed;
    [SerializeField]
    private TextMeshProUGUI lives;
    [SerializeField]
    private TextMeshProUGUI health;

    // Start is called before the first frame update
    void Start()
    {
        this.SetGameWonLostState(false);
    }

    public void UpdatePoints(int points)
    {
        this.points.text = $"Points: {points}";
    }

    public void UpdateSpeed(float speed)
    {
        this.speed.text = $"Speed: {speed.ToString("0")}";
    }

    public void SetGameWonLostState(bool status)
    {
        this.gameWonLost.SetActive(status);
    }

    public void SetWonLostMessage(bool won)
    {
        if (won)
        {
            this.gameWonLost.GetComponent<TextMeshProUGUI>().text = "You Won!";
        }
        else
        {
            this.gameWonLost.GetComponent<TextMeshProUGUI>().text = "You Lost";
        }
    }

    public void UpdateLives(int value)
    {
        this.lives.text = $"Lives: {value}";
    }

    public void UpdateHealth(int value)
    {
        this.health.text = $"Health: {value}";
    }
}
