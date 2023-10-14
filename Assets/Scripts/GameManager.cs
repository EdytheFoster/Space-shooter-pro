using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _isGameOver;
    [SerializeField]
    private bool _isYouWin;

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
        {
            SceneManager.LoadScene(1);//current game scene
        }
        else
        if (Input.GetKeyDown(KeyCode.R) && _isYouWin == true)
        {
            SceneManager.LoadScene(1);
        }
    }
    public void GameOver()
    {
        _isGameOver = true;
    }
    public void YouWin()
    {
        _isYouWin = true;
    }
}
