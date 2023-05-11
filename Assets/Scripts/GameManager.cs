using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI winText;
    public TextMeshProUGUI gameoverText;
    public Button restartButton;

    public GameObject titleScreen;

    private int timerTime = 59;
    public TextMeshProUGUI timeText;

    public bool isGameActive;

    public GameObject[] prefabsPart;

    // Start is called before the first frame update
    void Start()
    {
        

        //StartGame(10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameWin()
    {
        winText.gameObject.SetActive(true);
        isGameActive = false;
        restartButton.gameObject.SetActive(true);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
    IEnumerator ActivateTimer()
    {
        while (isGameActive)
        {
            if (timerTime < 0)
            {
                GameOver();
                break;
            }
            yield return new WaitForSeconds(1);
            timeText.text = "Time Left: " + timerTime--;
        }

        
    }

    public void GameOver()
    {
        gameoverText.gameObject.SetActive(true);
        isGameActive = false;
        restartButton.gameObject.SetActive(true);
    }
    public void StartGame(int diffculty)
    {
        isGameActive = true;
        timeText.gameObject.SetActive(true);
        StartCoroutine(ActivateTimer());

        titleScreen.gameObject.SetActive(false);
        Vector3 Pos;
        isGameActive = true;
        //Spawn random map
        for(int i = 0; i< diffculty; i++)
        {
            Pos = new Vector3(0, 0, 12 * i);
            int randomIndex = Random.Range(0,prefabsPart.Length-1);
            Instantiate(prefabsPart[randomIndex],Pos,prefabsPart[randomIndex].transform.rotation);
        }

        //Spawn the Goal that is the last element in array
        Pos = new Vector3(0, 0, 12 * diffculty);
        Instantiate(prefabsPart[prefabsPart.Length-1], Pos, prefabsPart[prefabsPart.Length-1].transform.rotation);

    }
   
}
