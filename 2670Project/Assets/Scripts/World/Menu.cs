using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Menu : MonoBehaviour
{
    private bool isLoaded;
    [SerializeField] private UnityEvent callEvent;
    public IntData respawnValue;
    public IntData respawnUnlock;
    public PlayerData player;
    public GameObject title;
    public GameObject background;
    public GameObject play;
    public GameObject endGame;
    public GameObject respawn;
    public GameObject yes;
    public GameObject no;
    public GameObject spawn1;
    public GameObject spawn2;
    public GameObject spawn3;
    public GameObject spawn4;
    public GameObject spawn5;
    public GameObject spawn6;
    public GameObject spawn7;
    public GameObject spawn8;
    public GameObject gameOver;

    // Start is called before the first frame update
    void Start()
    {
        respawnUnlock.value = 0;
        Time.timeScale = 0;
        isLoaded = true;
    }

    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isLoaded)
        {
            LoadMenu();
        }

        if (player.lives == 0)
        {
            GameOver();
        }
    }

    public void LoadMenu()
    {
        Time.timeScale = 0;
        background.SetActive(true);
        play.SetActive(true);
        endGame.SetActive(true);
        respawn.SetActive(true);
        isLoaded = true;
        title.SetActive(true);
    }

    public void PlayGame()
    {
        Time.timeScale = 1;
        isLoaded = false;
        play.SetActive(false);
        endGame.SetActive(false);
        respawn.SetActive(false);
        spawn1.SetActive(false);
        spawn2.SetActive(false);
        spawn3.SetActive(false);
        spawn4.SetActive(false);
        spawn5.SetActive(false);
        spawn6.SetActive(false);
        spawn7.SetActive(false);
        spawn8.SetActive(false);
        title.SetActive(false);
        background.SetActive(false);
    }

    public void EndGame()
    {
        play.SetActive(false);
        endGame.SetActive(false);
        respawn.SetActive(false);
        yes.SetActive(true);
        no.SetActive(true);
    }

    public void QuitCheckYes()
    {
        Application.Quit();
    }

    public void QuitCheckNo()
    {
        yes.SetActive(false);
        no.SetActive(false);
        play.SetActive(true);
        endGame.SetActive(true);
        respawn.SetActive(true);
    }

    public void Respawn()
    {
        play.SetActive(false);
        endGame.SetActive(false);
        respawn.SetActive(false);
        spawn1.SetActive(true);
        if (respawnUnlock.value >= 1) spawn2.SetActive(true);
        if (respawnUnlock.value >= 2) spawn3.SetActive(true);
        if (respawnUnlock.value >= 3) spawn4.SetActive(true);
        if (respawnUnlock.value >= 4) spawn5.SetActive(true);
        if (respawnUnlock.value >= 5) spawn6.SetActive(true);
        if (respawnUnlock.value >= 6) spawn7.SetActive(true);
        if (respawnUnlock.value >= 7) spawn8.SetActive(true);
    }

    public void RespawnPoint(int respawnNo)
    {
        respawnValue.value = respawnNo;
        callEvent?.Invoke();
        isLoaded = false;
        PlayGame();
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        background.SetActive(true);
        yes.SetActive(true);
        isLoaded = true;
        gameOver.SetActive(true);
    }

}
