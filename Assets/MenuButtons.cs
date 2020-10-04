using UnityEngine;
using UnityEngine.Playables;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] private CanvasGroup _gameHUD;
    [SerializeField] private CanvasGroup _mainMenu;
    
    [SerializeField] private GameObject _startButton;
    [SerializeField] private GameObject _resumeButton;
 
    [SerializeField] private PlayableDirector _introDirector;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
            Time.timeScale = 0;
        }
    }
    
    public void OnStartButtonClick()
    {
        ResumeGame();
        
        _introDirector.Play();
    }
    
    public void OnResumeButtonClick()
    {
        ResumeGame();
    }
    
    public void OnExitButtonClick()
    {
        Application.Quit();
    }

    private void PauseGame()
    {
        _gameHUD.alpha = 0;
        _mainMenu.alpha = 1;
        
        _startButton.SetActive(false);
        _resumeButton.SetActive(true);
        
        Time.timeScale = 0;
    }
    
    private void ResumeGame()
    {
        _gameHUD.alpha = 1;
        _mainMenu.alpha = 0;
        
        Time.timeScale = 1;
    }
}
