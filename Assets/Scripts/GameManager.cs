using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    [Header("Variables")]
    public float time = 30;

    [Header("UI")]
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bulletsText;
    public TextMeshProUGUI recordText;

    [Header("Audio")]
    public AudioClip idleAudioClip;
    public AudioClip gameAudioClip;
    public AudioSource audioSource;

    [Header("Mechanics")]
    public SimpleShoot simpleShoot;
    public TargetManager targetManager;

    private bool gameStarted = false;
    private bool gameEnded = false;

    void UpdateRecord(int score)
    {
        int maxScore = Mathf.Max(PlayerPrefs.GetInt("record", 0), score);
        PlayerPrefs.SetInt("record", maxScore);

        recordText.text = "Record: " + maxScore.ToString();
    }

    private void Awake()
    {
        targetManager = GameObject.Find("TargetManager").GetComponent<TargetManager>();

        int record = PlayerPrefs.GetInt("record", 0);
        UpdateRecord(record);
    }

    void EndGame()
    {
        gameEnded = true;

        // cambiar musica 
        audioSource.clip = idleAudioClip;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStarted && !gameEnded)
        {
            if (time <= 0)
            {
                EndGame();
            }

            time -= Time.deltaTime;
            timeText.text = ((int)Mathf.Round(time)).ToString() + "s";

            if(targetManager.AreAllTargetsDown())
            {
                EndGame();
            }
        }

        bulletsText.text = simpleShoot.bullets.ToString();

        int score = simpleShoot.points;
        scoreText.text = simpleShoot.points.ToString();

        UpdateRecord(score);
    }

    public void StartGame()
    {
        gameStarted = true;

        // cambiar musica 
        audioSource.clip = gameAudioClip;
        audioSource.Play();

        // encontrar todos los targets y levantarlos
        targetManager.ActivateAllTargets();
    }
}
