using GameActions;
using Observable;
using UnityEngine;
using UnityEngine.Playables;

public class MenuButtons : MonoBehaviour,
    ISessionStartedListener,
    IGameActionInvokedListener,
    IObserverNotifyEmpty<ISessionStartedListener>
{
    [SerializeField] private CanvasGroup _gameHUD;
    [SerializeField] private CanvasGroup _mainMenu;
    [SerializeField] private CanvasGroup _storyLayer;
    [SerializeField] private CanvasGroup _winScreen;
    [SerializeField] private CanvasGroup _looseScreen;
    
    [SerializeField] private GameObject _startButton;
    [SerializeField] private GameObject _resumeButton;
    [SerializeField] private GameObject _skipButton;
 
    [SerializeField] private PlayableDirector _introDirector;
    [SerializeField] private Animator _stateDrivenCM;
    
    private static readonly int Main = Animator.StringToHash("Main");

    private void Awake()
    {
        this.Subscribe<IGameActionInvokedListener, GameActionParams>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
            Time.timeScale = 0;
        }
    }

    private void OnDestroy()
    {
        this.Unsubscribe<ISessionStartedListener>();
        this.Unsubscribe<IGameActionInvokedListener, GameActionParams>();
    }

    public void OnStartButtonClick()
    {
        this.Subscribe<ISessionStartedListener>();
        
        _mainMenu.alpha = 0;
        _storyLayer.alpha = 1;
        _mainMenu.interactable = false;
        
        Time.timeScale = 1;
        
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
    
    public void OnSkipButtonClick()
    {
        this.NotifyListeners<ISessionStartedListener>();
        
        _stateDrivenCM.SetTrigger(Main);

        ResumeGame();

        Camera.main.orthographic = true;
        
        _skipButton.SetActive(false);
    }

    private void PauseGame()
    {
        _gameHUD.alpha = 0;
        _mainMenu.alpha = 1;
        _mainMenu.interactable = true;
        
        _startButton.SetActive(false);
        _resumeButton.SetActive(true);
        
        Time.timeScale = 0;
    }
    
    private void ResumeGame()
    {
        _gameHUD.alpha = 1;
        _mainMenu.alpha = 0;
        _mainMenu.interactable = false;
        
        Time.timeScale = 1;
    }

    void IObserver<ISessionStartedListener, EmptyParams>.Completed(EmptyParams parameters)
    {
        _gameHUD.alpha = 1;
        _storyLayer.alpha = 0;
    }

    void IObserver<IGameActionInvokedListener, GameActionParams>.Completed(GameActionParams parameters)
    {
        if (parameters.Action == GameAction.SheriffSavesGranny)
        {
            _winScreen.alpha = 1;
            _gameHUD.alpha = 0;
            Time.timeScale = 0;
        } 
        else if (parameters.Action == GameAction.GrannyHanged)
        {
            _looseScreen.alpha = 1;
            _gameHUD.alpha = 0;
            Time.timeScale = 0;
        }
    }
}
