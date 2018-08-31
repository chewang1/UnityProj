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
    private bool isAgain = false;
    private int count = 0;
    //2d array for saving values from numbers shown in panel
    public int[,] values = new int[3,3];
    public Text resultText1;
    public Text resultText2;
    public Text resultText3;
    public Text countText;
    public Text congText;
    public bool[] allStop = new bool[3];

    public GameObject hlinemid;
    public GameObject hlineup;
    public GameObject hlinebtm;
    public GameObject vlinemid;
    public GameObject vlineright;
    public GameObject vlinemidleft;
    public GameObject dline;
    public GameObject dline2;
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
        if (isAgain)
            LineDisable();
        if (allStop[0] && allStop[1] && allStop[2])
        {
            CheckLines();
            allStop[0] = allStop[1] = allStop[2] = false;
            if (isWin)
            {
                congText.gameObject.SetActive(true);
                countText.gameObject.SetActive(true);
                countText.text = "You got " + count.ToString() + " Line.";
                isWin = false;      
            }
            count = 0;
            isAgain = true;
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
        
        //horizon lines
        if ((values[0, 0] == values[0, 1] && values[0, 1] == values[0, 2]) ||
            (values[1, 0] == values[1, 1] && values[1, 1] == values[1, 2]) ||
            (values[2, 0] == values[2, 1] && values[2, 1] == values[2, 2]))
        {
            isWin = true;
            //up row
            if (values[0, 0] == values[0, 1] && values[0, 1] == values[0, 2])
            {
                hlineup.SetActive(true);
                count++;
            }
            //middle row
            if (values[1, 0] == values[1, 1] && values[1, 1] == values[1, 2])
            {
                hlinemid.SetActive(true);
                count++;
            }
            //low row
            if (values[2, 0] == values[2, 1] && values[2, 1] == values[2, 2])
            {
                hlinebtm.SetActive(true);
                count++;
            }
        }
        //vertical lines
        if ((values[0, 0] == values[1, 0] && values[1, 0] == values[2, 0]) ||
                 (values[0, 1] == values[1, 1] && values[1, 1] == values[2, 1]) ||
                 (values[0, 2] == values[1, 2] && values[1, 2] == values[2, 2]))
        {
            isWin = true;
            //left coloum
            if (values[0, 0] == values[1, 0] && values[1, 0] == values[2, 0])
            {
                vlinemidleft.SetActive(true);
                count++;
            }
            //middle coloum
            if (values[0, 1] == values[1, 1] && values[1, 1] == values[2, 1])
            {
                vlinemid.SetActive(true);
                count++;
            }
            //right coloum
            if (values[0, 2] == values[1, 2] && values[1, 2] == values[2, 2])
            {
                vlineright.SetActive(true);
                count++;
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
                dline.SetActive(true);
                count++;

            }
            //righttop to leftbottom diag
            if (values[0, 2] == values[1, 1] && values[1, 1] == values[2, 0])
            {
                dline2.SetActive(true);
                count++;

            }
        }
    
        
    }

    public void LineDisable()
    {
        hlinemid.SetActive(false);
        hlineup.SetActive(false);
        hlinebtm.SetActive(false);
        vlinemid.SetActive(false);
        vlineright.SetActive(false);
        vlinemidleft.SetActive(false);
        dline.SetActive(false);
        dline2.SetActive(false);
        congText.gameObject.SetActive(false);
        countText.gameObject.SetActive(false);
        isAgain = false;
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
