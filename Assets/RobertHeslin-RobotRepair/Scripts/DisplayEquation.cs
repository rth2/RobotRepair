using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using RobertHeslin.RobotRepair;
using System;

public class DisplayEquation : MonoBehaviour
{
    internal enum FractionType
    {
        unit = 0,
        improper = 1,
        proper = 2
    };

    [SerializeField] TextMeshProUGUI equationDisplay;
    [SerializeField] TextMeshProUGUI resultDisplay;
    [SerializeField] ScenarioHandler scenarioHandler;

    [SerializeField] FractionType fractionType;

    [SerializeField] int denominator = 0;
    [SerializeField] int equationAdditions, equationSubtractions; //Number of parts being used in the equation
    //do i have a list of strings for the equation? Then just concatanate them to display?


    private void Start()
    {

        denominator = scenarioHandler.GetCurrentRobotSize();

        UpdateDisplay();
    }

    #region Setters

    internal void SetDisplayUnitFractions(FractionType type)
    {
        fractionType = type;
    }

    #endregion

    #region Getters

    internal FractionType GetFractionType()
    {
        return fractionType;

    }

    #endregion

    private void UpdateEquation()
    {
        ClearEquation();

        if (equationAdditions == 0 && equationSubtractions == 0)
            {
                equationDisplay.text = "0";
                return;
            }

        //need to put the equation together and then display it.
        switch(fractionType)
        {
            case FractionType.unit:
                for (int i = 0; i < equationAdditions; i++)
                {
                    equationDisplay.text += "1/" + denominator.ToString() + " ";

                    if (i + 1 != equationAdditions)
                        equationDisplay.text += "+ ";
                }

                for (int i = 0; i < equationSubtractions; i++)
                {
                    equationDisplay.text += "- 1/" + denominator.ToString() + " ";
                }
                break;
            case FractionType.improper:
                if (equationAdditions > 0)
                    equationDisplay.text += equationAdditions.ToString() + "/" + denominator.ToString() + " ";

                if (equationSubtractions > 0)
                    equationDisplay.text += "- " + equationSubtractions.ToString() + "/" + denominator.ToString();
                break;
            case FractionType.proper:
                if (equationAdditions > 0)
                {
                    int wholeNumber = Mathf.FloorToInt(equationAdditions / denominator);

                    int remainder = equationAdditions - (wholeNumber * denominator);

                    if (wholeNumber > 0)
                        equationDisplay.text += wholeNumber.ToString() + " ";

                    if (remainder > 0)
                        equationDisplay.text += remainder.ToString() + "/" + denominator.ToString() + " ";

                }

                if (equationSubtractions > 0)
                {
                    int wholeNumber = Mathf.FloorToInt(equationSubtractions / denominator);

                    int remainder = equationSubtractions - (wholeNumber * denominator);

                    if (wholeNumber > 0 || remainder > 0)
                        equationDisplay.text += "- ";

                    if (wholeNumber > 0)
                        equationDisplay.text += wholeNumber.ToString() + " ";

                    if (remainder > 0)
                        equationDisplay.text += remainder.ToString() + "/" + denominator.ToString();

                }
                break;
            default:
                for (int i = 0; i < equationAdditions; i++)
                {
                    equationDisplay.text += "1/" + denominator.ToString() + " ";

                    if (i + 1 != equationAdditions)
                        equationDisplay.text += "+ ";
                }

                for (int i = 0; i < equationSubtractions; i++)
                {
                    equationDisplay.text += "- 1/" + denominator.ToString() + " ";
                }
                break;
        }
    }

    private void UpdateResult()
    {
        ClearResult();

        resultDisplay.text = "=";
        //resultDisplay.text = "= " + (equationAdditions - equationSubtractions).ToString() + "/" + denominator.ToString();

    }

    private void UpdateDisplay()
    {
        UpdateEquation();
        UpdateResult();
    }

    private void ClearEquation()
    {
        equationDisplay.text = "";
    }

    private void ClearResult()
    {
        resultDisplay.text = "";
    }

    internal void AddToEquation()
    {
        equationAdditions++;
        UpdateDisplay();
    }

    internal void SubtractFromEquation()
    {
        equationAdditions--;
        if (equationAdditions < 0)
            equationAdditions = 0;

        UpdateDisplay();
    }
}
