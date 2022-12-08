using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSavedData : MonoBehaviour
{
    [SerializeField] private int points = 0;
    [SerializeField] private float coins = 0f;
    [SerializeField] private string names = "Loki";

    private void Start()
    {
        LoadData();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SaveData();
        }
    }

    public void AddPoints(int pointsToAdd)
    {
        points ++;
        coins = coins + 1.5f;
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("Points", points);
        PlayerPrefs.SetFloat("Coins", coins);
        PlayerPrefs.SetString("El nombre más hermoso de la vida", names);
        Debug.Log("Data saved");
    }

    public void LoadData()
    {
        points = PlayerPrefs.GetInt("Points");
        coins = PlayerPrefs.GetInt("Coins");
        names = PlayerPrefs.GetString("El nombre más hermoso de la vida");
        Debug.Log("Data loaded");
    }
}
