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
    private bool isWin = false;
    //2d array for saving values from numbers shown in panel
    public int[,] values = new int[3,3];
    public Text resultText1;
    public Text resultText2;
    public Text resultText3;
    public bool[] allStop = new bool[3];


    void Update()
    {
        ReelSpin reel1 = this.transform.Find("Reel1").GetComponent<ReelSpin>();
        ReelSpin reel2 = this.transform.Find("Reel2").GetComponent<ReelSpin>();
        ReelSpin reel3 = this.transform.Find("Reel3").GetComponent<ReelSpin>();
        if (reel1.isDone)
            allStop[0] = true;
        if (reel2.isDone)
            allStop[1] = true;
        if (reel3.isDone)
            allStop[2] = true;

       
        if (!allStop[0] || !allStop[1] || !allStop[2])
            LineDisable();
        if (allStop[0] && allStop[1] && allStop[2])
        {
            CheckLines();
            allStop[0] = allStop[1] = allStop[2] = false;
        }
            
    }

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

    public void CheckLines()
    {
        GameObject hline = transform.Find("HorizonLine").gameObject;
        GameObject vline = transform.Find("VerticalLine").gameObject;
        GameObject dline = transform.Find("DiagonalLine").gameObject;
        GameObject dline2 = transform.Find("DiagonalLine2").gameObject;
        //horizon lines
        if ((values[0, 0] == values[0, 1] && values[0, 1] == values[0, 2]) ||
            (values[1, 0] == values[1, 1] && values[1, 1] == values[1, 2]) ||
            (values[2, 0] == values[2, 1] && values[2, 1] == values[2, 2]))
        {
            isWin = true;
            //up row
            if (values[0, 0] == values[0, 1] && values[0, 1] == values[0, 2])
            {
                hline.GetComponent<LineRenderer>().enabled = true;
                Vector3 pos = transform.Find("Reel1").Find("").GetComponent<ReelSpin>().displayPos[0];
                hline.transform.position = pos + new Vector3(0, 0, -0.5f);
            }
            //middle row
            if (values[1, 0] == values[1, 1] && values[1, 1] == values[1, 2])
            {
                hline.GetComponent<LineRenderer>().enabled = true;
            }
            //low row
            if (values[2, 0] == values[2, 1] && values[2, 1] == values[2, 2])
            {
                hline.GetComponent<LineRenderer>().enabled = true;
                Vector3 pos = transform.Find("Reel1").Find("").GetComponent<ReelSpin>().displayPos[2];
                hline.transform.position = pos + new Vector3(0, 0, -0.5f);
            }
        }
        //vertical lines
        if ((values[0, 0] == values[1, 0] && values[1, 0] == values[2, 0]) ||
                 (values[0, 1] == values[1, 1] && values[1, 1] == values[1, 2]) ||
                 (values[0, 2] == values[1, 2] && values[1, 2] == values[2, 2]))
        {
            isWin = true;
            //left coloum
            if (values[0, 0] == values[1, 0] && values[1, 0] == values[2, 0])
            {
                vline.GetComponent<LineRenderer>().enabled = true;
                Vector3 pos = transform.Find("Reel1").Find("").GetComponent<ReelSpin>().displayPos[0];
                vline.transform.position = pos + new Vector3(0, 0, -0.5f);
            }
            //middle coloum
            if (values[0, 1] == values[1, 1] && values[1, 1] == values[1, 2])
            {
                vline.GetComponent<LineRenderer>().enabled = true;
            }
            //right coloum
            if (values[0, 2] == values[1, 2] && values[1, 2] == values[2, 2])
            {
                vline.GetComponent<LineRenderer>().enabled = true;
                Vector3 pos = transform.Find("Reel3").Find("").GetComponent<ReelSpin>().displayPos[0];
                vline.transform.position = pos + new Vector3(0, 0, -0.5f);
            }
        }
        //diagonal lines
        if ((values[0, 0] == values[1, 1] && values[1, 1] == values[2, 2]) ||
                 (values[0, 2] == values[1, 1] && values[1, 1] == values[2, 0]))
        {
            isWin = true;
            //lefttop to rightbottom diag
            if (values[0, 0] == values[1, 1] && values[1, 1] == values[2, 2])
            {
                dline.GetComponent<LineRenderer>().enabled = true;
                
            }
            //righttop to leftbottom diag
            if (values[0, 2] == values[1, 1] && values[1, 1] == values[2, 0])
            {
                dline2.GetComponent<LineRenderer>().enabled = true;
                
            }
        }
    
        
    }

    public void LineDisable()
    {
        GameObject hline = transform.Find("HorizonLine").gameObject;
        GameObject vline = transform.Find("VerticalLine").gameObject;
        GameObject dline = transform.Find("DiagonalLine").gameObject;
        GameObject dline2 = transform.Find("DiagonalLine2").gameObject;

        hline.GetComponent<LineRenderer>().enabled = false;
        vline.GetComponent<LineRenderer>().enabled = false;
        dline.GetComponent<LineRenderer>().enabled = false;
        dline2.GetComponent<LineRenderer>().enabled = false;
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
