using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MathProblemManager : MonoBehaviour
{ // SINGLETON
    public static MathProblemManager Instance; // Instance is the Singleton

    // PROBLEM VARS
    private string _mathProblemString = "";
    private int _number1;
    private int _number2;
    private int _mathPoblemSolution = 0;

    // LIFE CYCLE METHODS
    private void Awake()
    {
        // SINGLETON
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        this._number1 = GenerateRandomNumber();
        _mathProblemString = _number1.ToString() + " + ";
    }

    private void Update()
    {
    }

    /// <summary>
    /// Retrieve the current math problem as a string
    /// </summary>
    /// <returns>String : current math problem</returns>
    public String GetMathProblemString()
    {
        return _mathProblemString;
    }

    /// <summary>
    /// Retrieve the current math problems solution
    /// </summary>
    /// <returns>int : current math problem's solution</returns>
    public int GetSolution()
    {
        return _mathPoblemSolution;
    }

    /// <summary>
    /// Reset problem trackers and generate a new problem.
    /// </summary>
    /// <returns>String : returns the math problem as a string for easy assignment to a text view</returns>
    public string GenerateProblem()
    {
        // _number1
        if (_mathPoblemSolution != 0) _number1 = _mathPoblemSolution;
        else _number1 = GenerateRandomNumber();
        // _number2
        _number2 = GenerateRandomNumber();
        // solution int
        _mathPoblemSolution = _number1 + _number2;
        // problem string
        return _number1 + " + " + _number2;
    }

    public void ResetProblemStats()
    {
        _mathProblemString = "";
        _number1 = 0;
        _number2 = 0;
        _mathPoblemSolution = 0;
    }

    /// <summary>
    /// Check players answering returning True if they're correct, false otherwise.
    /// </summary>
    /// <param name="answer">String representation of the players answer, good for plugging in the input elements text</param>
    /// <returns>bool : True if player's answer is correct, False if player's answer is incorrect</returns>
    public bool CheckPlayersAnswer(String answer)
    {
        // CHECK ANSWER
        int playersAnswer = Int32.Parse(answer);
        bool isPlayerCorrect = (_mathPoblemSolution == playersAnswer);
        Debug.Log("Correct Answer: " + _mathPoblemSolution);

        // RETURN
        return isPlayerCorrect;
    }

    /// <summary>
    /// Generates a random nuber between 2 - 10
    /// </summary>
    /// <returns>int 2 - 10</returns>
    private int GenerateRandomNumber()
    {
        return (int)UnityEngine.Random.Range(2f, 10);
    }
}
