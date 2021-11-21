using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MicroRuleEngine;
using Newtonsoft.Json;
using System.IO;
using System;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject ball;
    private PlayerController player;
    private int currentLives;
    private int currentHealth;
    [SerializeField]
    private Canvas canvas;
    private UIController ui;
    private int points;
    [SerializeField]
    private AudioSource warningSound;

    private Func<Ball, bool> pointsRule;
    private Func<Ball, bool> healthRule;
    private Func<Ball, bool> livesRule;

    // Start is called before the first frame update
    void Start()
    {
        this.player = this.ball.GetComponent<PlayerController>();
        this.currentLives = this.player.GetBall().Lives;
        this.ui = this.canvas.GetComponent<UIController>();
        this.ui.SetGameWonLostState(false);
        this.ui.UpdatePoints(this.points);
        this.ui.UpdateSpeed(this.player.GetSpeed());
        this.ui.UpdateLives(currentLives);
        this.CreateRules();
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (pointsRule(this.player.GetBall()))//Increase speed when rule is True
        {
            this.player.SetSpeed(this.player.GetSpeed()+3f);
            this.ui.UpdateSpeed(this.player.GetSpeed());
        }

        if (healthRule(this.player.GetBall()))
        {
            this.player.LowHealth();
        }

        if (livesRule(this.player.GetBall()))
        {
            if (!warningSound.loop)//If the warning is already looping, dont try to do anything
            {
                warningSound.loop = true;
                warningSound.Play();
            }
        }
        else
        {
            warningSound.loop = false;
        }

        if (this.player.GetBall().Health <= 0 && this.player.GetBall().Lives <= 0)
        {
            Time.timeScale = 0;
            this.ui.SetWonLostMessage(false);
            this.ui.SetGameWonLostState(true);
        }

        if (this.points == 12)
        {
            Time.timeScale = 0;
            this.ui.SetWonLostMessage(true);
            this.ui.SetGameWonLostState(true);
        }

        if (this.player.GetBall().Lives != this.currentLives)//If lives changed
        {
            this.currentLives = this.player.GetBall().Lives;
            this.ui.UpdateLives(currentLives);
        }

        if (this.player.GetBall().Health != this.currentHealth)//If health changed
        {
            this.currentHealth = this.player.GetBall().Health;
            this.ui.UpdateHealth(currentHealth);
        }
    }

    public void UpdatePoints(int value)
    {
        this.points = value;
        this.ui.UpdatePoints(this.points);
    }

    public void UpdateSpeed(float speed)
    {
        this.ui.UpdateSpeed(speed);
    }

    private void CreateRules()
    {
        string jsonContents = File.ReadAllText(@".\Assets\Scripts\ballRules.json");
        List<Rule> rulesList = JsonConvert.DeserializeObject<List<Rule>>(jsonContents);
        MRE engine = new MRE();
        this.pointsRule = engine.CompileRule<Ball>(rulesList[0]);
        this.healthRule = engine.CompileRule<Ball>(rulesList[1]);
        this.livesRule = engine.CompileRule<Ball>(rulesList[2]);
    }
}
