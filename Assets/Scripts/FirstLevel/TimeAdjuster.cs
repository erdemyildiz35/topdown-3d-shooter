using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class TimeAdjuster : MonoBehaviour
{
    public TextMeshProUGUI yearFirstText;
    public TextMeshProUGUI yearSecondText;
    public TextMeshProUGUI yearThirdText;
    public TextMeshProUGUI yearForthText;

    public TextMeshProUGUI monthFirstText;
    public TextMeshProUGUI monthSecondText;

    public TextMeshProUGUI dayFirstText;
    public TextMeshProUGUI daySecondText;

    public void AdjustYearOtherTime(TextMeshProUGUI yearText)
    {
        var timeValue = int.Parse(yearText.text);

        if (timeValue >= 9)
        {
            yearText.text = "0";
        }
        else if (timeValue >= 0)
        {
            yearText.text = (timeValue + 1).ToString();
        }
    }

    public void AdjustYearFirstTime()
    {
        var timeValue = int.Parse(yearFirstText.text);

        if (timeValue >= 3)
        {
            yearFirstText.text = "0";
        }
        else if (timeValue >= 0)
        {
            yearFirstText.text = (timeValue + 1).ToString();
        }
    }

    public void AdjustMonthFirstTime()
    {
        if (int.Parse(monthFirstText.text) >= 0)
        {
            monthFirstText.text = (int.Parse(monthFirstText.text) + 1).ToString();
        }
        
        if (int.Parse(monthFirstText.text) >= 1)
        {
            monthSecondText.text = "0";
        }
        
        if (int.Parse(monthFirstText.text) >= 2)
        {
            monthFirstText.text = "0";
            monthSecondText.text = "0";
        }
    }

    public void AdjustMonthSecondTime()
    {
        if (int.Parse(monthFirstText.text) >=1)
        {
            if (int.Parse(monthSecondText.text) >= 2)
            {
                monthSecondText.text = "0";
            }
            else if (int.Parse(monthSecondText.text) >= 0)
            {
                monthSecondText.text = (int.Parse(monthSecondText.text) + 1).ToString();
            }
        }
        else
        {
            if (int.Parse(monthSecondText.text) >= 9)
            {
                monthSecondText.text = "0";
            }
            else if (int.Parse(monthSecondText.text) >= 0)
            {
                monthSecondText.text = (int.Parse(monthSecondText.text) + 1).ToString();
            }
        }
    }
    
    public void AdjustDayFirstTime()
    {
        if (int.Parse(dayFirstText.text) >= 0)
        {
            dayFirstText.text = (int.Parse(dayFirstText.text) + 1).ToString();
        }

        if (int.Parse(dayFirstText.text) >= 3 )
        {
            
            daySecondText.text = "0";
        }
        
        if (int.Parse(dayFirstText.text) >= 4 )
        {
            dayFirstText.text = "0";
            daySecondText.text = "0";
        }
    }
    
    public void AdjustDaySecondTime()
    {
        if (int.Parse(dayFirstText.text) >=3)
        {
            if (int.Parse(daySecondText.text) >= 1)
            {
                daySecondText.text = "0";
            }
            else if (int.Parse(daySecondText.text) >= 0)
            {
                daySecondText.text = (int.Parse(daySecondText.text) + 1).ToString();
            }
        }
        else
        {
            if (int.Parse(daySecondText.text) >= 9)
            {
                daySecondText.text = "0";
            }
            else if (int.Parse(daySecondText.text) >= 0)
            {
                daySecondText.text = (int.Parse(daySecondText.text) + 1).ToString();
            }
        }
    }
}