using UnityEngine;
using UnityEngine.InputSystem;

public class GameStateController : MonoBehaviour
{
    [SerializeField] private LoseWidget _loseWidget;
    [SerializeField] private PauseWidget _pauseWidget;
    [SerializeField] private SceneSwitcher _sceneSwitcher;

    private PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        _playerInput.Enable();

        _playerInput.Player.Pause.performed += Pause;
    }

    private void OnDisable()
    {
        _playerInput.Player.Pause.performed -= Pause;
        _playerInput.Enable();
    }

    private void OnDestroy()
    {
        _playerInput.Player.Pause.performed -= Pause;
        _playerInput.Enable();
    }

    private void Pause(InputAction.CallbackContext context)
    {
        _pauseWidget.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void OnRoundEnded(bool setNextRound)
    {
        StartCoroutine(CoroutineUtils.CallWithDelay(5.0f, RestartLevel, setNextRound));
    }

    private void RestartLevel(bool setNextRound)
    {
        if (setNextRound)
        {
            UserDataStorage.Instance.Round++;
            _sceneSwitcher.OnGameButtonClick();
        }
        else
        {
            _loseWidget.gameObject.SetActive(true);
        }
    }
}