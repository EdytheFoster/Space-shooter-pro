using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    //handle to text
    [SerializeField] 
    private Text _scoreText;
    [SerializeField]
    private Image _LivesImg;
    [SerializeField]
    private Sprite[] _liveSprites;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartText;
    [SerializeField]
    private GameManager _gameManager;
    [SerializeField]
    private Text _ammoText;
    [SerializeField]
    private Slider _thrusterFuelSlider;
    
    
  
    


    // Start is called before the first frame update
    void Start()
    {
        //assign text component to the handle
        
        _scoreText.text = "Score: " + 00;
        _gameOverText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        if (_gameManager == null)
        {
            Debug.LogError("GameManager is NULL.");
        }

        
    }
    public void UpdateThrusterFuel(float value)
    {
        _thrusterFuelSlider.value = value;
    }
   

    public void UpdateAmmoCount(int AmmoCount)
    {
        _ammoText.text = "Ammo: " + AmmoCount;
    }

    public void UpdateScore(int PlayerScore)
    {
        _scoreText.text = "Score: " + PlayerScore;
    }

    public void UpdateLives(int currentLives)
    {
        //display image sprite
        //give it a new one based on the current lives index
        _LivesImg.sprite = _liveSprites[currentLives];

        if(currentLives == 0)
        {
            GameOverSequence();
            
        }
    }

    void GameOverSequence()
    {
        _gameManager.GameOver();
        _gameOverText.gameObject.SetActive(true);
        _restartText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlickerRoutine());

    }
   

    IEnumerator GameOverFlickerRoutine()
    {
        while(true)
        {
            _gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }
    
}
