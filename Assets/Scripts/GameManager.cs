using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{

    // UI MATH PROBLEM
    [SerializeField] private TextMeshPro _textViewProblem;
    private string _mathProblemString = "";
    private bool _isMathProblemFull = false;
    private int _number1;
    private int _number2;
    private int _mathPoblemSolution = 0;

    // UI INPUT ANSWER
    [SerializeField] private TMP_InputField _inputAnswer;

    // STATS
    private int _score = 0;
    private int _currentPowerLevel = 0;
    private int _answerStreak = 1;

    // Start is called before the first frame update
    void Start()
    {
        this._number1 = GenerateRandomNumber();
        _mathProblemString = _number1.ToString() + " + ";
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isMathProblemFull)
        {
            _isMathProblemFull = true;
            GenerateNewProblem();
        }


        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            Debug.Log("Enter pressed");
            CheckPlayersAnswer();
        }
    }

    private void CheckPlayersAnswer()
    {
        int playersAnswer = Int32.Parse(_inputAnswer.text);
        Debug.Log("Correct Answer: " + _mathPoblemSolution);
        if (_mathPoblemSolution == playersAnswer)
        {
            Debug.Log("answer correct");
            HangleCorrectAnswer();
        }
        else
        {
            Debug.Log("incorrect");
            HangleIncorrectAnswer();
        }
        _isMathProblemFull = false;
        GenerateNewProblem();
    }

    private void GenerateNewProblem()
    {
        // reset numbers
        if (_mathPoblemSolution != 0) _number1 = _mathPoblemSolution;
        else _number1 = GenerateRandomNumber();
        _number2 = GenerateRandomNumber();
        _mathPoblemSolution = _number1 + _number2;

        _textViewProblem.text = _number1 + " + " + _number2;
        _inputAnswer.text = "";
        _inputAnswer.ActivateInputField();
    }

    private void Update_PowerLevel(int addPower)
    {
        _currentPowerLevel += addPower;
        // todo: add ui updates
    }

    private void Update_Score(int addPoints)
    {
        _score += addPoints;
        // todo: add ui updates
    }

    private void HangleCorrectAnswer()
    {
        Update_PowerLevel(_mathPoblemSolution * _answerStreak);
        _answerStreak++;
    }

    private void HangleIncorrectAnswer()
    {
        _answerStreak = 1;
    }

    private int AddSecondNumber()
    {
        _number2 = GenerateRandomNumber();
        return this._number2;
    }

    /// <summary>
    /// Generates a random nuber between 2 - 10
    /// </summary>
    /// <returns>int 2 - 10</returns>
    private int GenerateRandomNumber()
    {
        return (int) UnityEngine.Random.Range(2f, 10);
    }
}
