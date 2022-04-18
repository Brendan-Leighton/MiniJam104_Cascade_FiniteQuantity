using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    // SINGLETONS
    [SerializeField] private MathProblemManager MPM;

    // UI
    [SerializeField] private GameObject _playersUI;
    [SerializeField] private GameObject _npcsUI;
    [SerializeField] private TMP_Text _npcsPower;
    [SerializeField] private TextMeshPro _textViewProblem;
    [SerializeField] private TMP_InputField _inputAnswer;
    [SerializeField] private TMP_Text _tv_Score;
    [SerializeField] private TMP_Text _tv_PowerLevel;

    // GAME STATS
    private int _score = 0;
    private float _currentPowerLevel = 0;
    private int _streakCurrent = 1;
    private int _streakLongest = 0;
    private bool _isCurrentProblem = false;

    // 3D OBJECTS
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _enemy;
    [SerializeField] private GameObject _projectile;

    // CHARACTER SCRIPTS
    private Character _playerCharacter;
    private Character _npcCharacter;

    // TRACKERS
    private bool isPlayersTurn = true;
    private bool isDamgeCalculationsDone = true;

    // PLAYERS TURN
    // timer
    [SerializeField] private TMP_Text _tv_Timer;
    [SerializeField] private float _timeLimit = 20f;
    private Timer _timer;
    private float _remainingTime;
    // problem tracker
    [SerializeField] private int _maxProblemsPerTurn = 5;
    private int _currProblemsThisTurn = 0;

    // DAMAGE TURN


    // LIFE CYCLE METHODS
    private void Awake()
    {
        _timer = new Timer(_tv_Timer, _timeLimit);
        _playerCharacter = _player.GetComponent<Character>();
        _npcCharacter = _enemy.GetComponent<Character>();
    }

    private void Start()
    {
        _playersUI.SetActive(true);
        _npcsUI.SetActive(false);
        StartPlayersTurn();
    }
    void Update()
    {
        // WAIT FOR DAMAGE
        if (!isDamgeCalculationsDone)
        {
            if (_playerCharacter.isProjectileTraveling || _npcCharacter.isProjectileTraveling) return;
            isDamgeCalculationsDone = true;
            isPlayersTurn = true;
            _isCurrentProblem = false;
            ToggleUI();
        }

        // PLAYERS TURN
        if (isPlayersTurn)
        {
            // TIMER
            if (_timer.isStarted)  _timer.Update();
            else _timer.StartTimer();

            // MATH PROBLEM
            if (_isCurrentProblem == false)
            {
                _isCurrentProblem = true;
                string newProblem = MPM.GenerateProblem();
                if (newProblem == null) Debug.Log("new problem == null");
                else _textViewProblem.text = newProblem;
                _inputAnswer.ActivateInputField();
            }

            // handle iterating through problems
            if (Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                Debug.Log("Enter pressed");

                bool isPlayerCorrect = MPM.CheckPlayersAnswer(_inputAnswer.text);

                UpdateAnswerStreak(isPlayerCorrect);

                // GENERATE PROBLEM
                _isCurrentProblem = false;
                _currProblemsThisTurn++;
                if (_currProblemsThisTurn < _maxProblemsPerTurn)
                {
                    MPM.GenerateProblem();
                }
                // RESET PROBLEM
                else
                {
                    SwitchTurn();
                    MPM.ResetProblemStats();
                }
            }

            // TEST ATTACK
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Space pressed");
                _playerCharacter.ShootProjectile(100);
            }
        }
    }

    private void SwitchTurn()
    {
        isPlayersTurn = !isPlayersTurn;

        if (isPlayersTurn)
        {
            StartPlayersTurn();
        }
        else
        {
            StopPlayersTurn();
            HandleDamageTurn();
        }

        ToggleUI();
    }

    private void ToggleUI()
    {
        // player
       _playersUI.SetActive(isPlayersTurn);
        _textViewProblem.gameObject.SetActive(isPlayersTurn);
        _inputAnswer.gameObject.SetActive(isPlayersTurn);
        _tv_Score.gameObject.SetActive(isPlayersTurn);
        _tv_PowerLevel.gameObject.SetActive(isPlayersTurn);
        
        // npc
        _npcsUI.SetActive(!isPlayersTurn);
    }

    private void Update_PowerLevel(float remainingTime)
    {
        if (remainingTime < 1f) remainingTime = 1f;
        if (_streakLongest < 1) _streakLongest = 1;
        _currentPowerLevel = _streakLongest * remainingTime;
        Debug.Log("power level: " + _currentPowerLevel);
        _tv_PowerLevel.text = "Power: " + _currentPowerLevel.ToString();
    }

    private void Update_Score(int addPoints)
    {
        _score += addPoints;
        _tv_Score.text = "Score: " + _score.ToString();
    }

    private void UpdateAnswerStreak(bool isCorrectAnswer)
    {
        if (isCorrectAnswer) _streakCurrent++;
        else
        {
            _streakLongest = Math.Max(_streakCurrent, _streakLongest);
            _streakCurrent = 0;
        }
    }

    private void StartPlayersTurn()
    {
        // start timer
        _timer.StartTimer();
    }

    private void StopPlayersTurn()
    {
        if (_timer.isStarted)
        {
            _remainingTime = _timer.StopTimer();
        }
        else
        {
            _remainingTime = 0;
        }
        Update_PowerLevel(_remainingTime);
    }

    private void HandleDamageTurn()
    {
        isDamgeCalculationsDone = false;
        Debug.Log("Damage Turn");
        _playerCharacter.ShootProjectile(_currentPowerLevel);
        _npcCharacter.ShootProjectile(20f);
        _currProblemsThisTurn = 0;
        MPM.ResetProblemStats();
    }
}
