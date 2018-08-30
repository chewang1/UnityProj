using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool isSpin = false;
    public int probability = 10;
    //the speed of reel
    public float maxspeed = 100;
    //user define how many times while spinning
    public int spintimes = 0;
    public bool isStart = false;
    //2d array for saving values from numbers shown in panel
    public int[,] values = new int[3,3];
    public Text resultText1;
    public Text resultText2;
    public Text resultText3;
 

    public void onClickedSpin()
    {
        if (isSpin)
        {
            GetResult();
            //show result
            resultText1.text = values[0, 0].ToString() + " " + values[0, 1].ToString() + " " + values[0, 2].ToString();
            resultText2.text = values[1, 0].ToString() + " " + values[1, 1].ToString() + " " + values[1, 2].ToString();
            resultText3.text = values[2, 0].ToString() + " " + values[2, 1].ToString() + " " + values[2, 2].ToString();
        }

        //for (int i = 0; i < values.GetLength(0); i++)
        //for (int j = 0; j < values.GetLength(1); j++)
        //Debug.Log(values[i, j]);
        isStart = true;
        isSpin = !isSpin;
    }

    public void GetResult()
    {
        //get result from 3 reels and save into 2d array
        ReelSpin reel1 = this.transform.Find("Reel1").GetComponent<ReelSpin>();
        ReelSpin reel2 = this.transform.Find("Reel2").GetComponent<ReelSpin>();
        ReelSpin reel3 = this.transform.Find("Reel3").GetComponent<ReelSpin>();
        for (int i = 0; i < values.GetLength(0); i++)
        {
            values[i, 0] = reel1.shownNumbers[i];
            values[i, 1] = reel2.shownNumbers[i];
            values[i, 2] = reel3.shownNumbers[i];
        }
    }

    
}

public class GM
{
    private static GameManager m_gm = null;
    public static GameManager GameManager
    {
        get
        {
            if (m_gm == null)
                m_gm = GameObject.FindObjectOfType<GameManager>();
            return m_gm;
        }
    }
}
