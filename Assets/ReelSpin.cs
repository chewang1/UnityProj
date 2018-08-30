using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReelSpin : MonoBehaviour
{
    
    private Vector3 diff;
    //boundaries for numbers
    private float lowerbound;
    private float topbound;


    //increase speed
    private float curspeed;
    private float acceleration = 1;
    private bool isMaxSpeed;
    public bool isDone = false;

    [SerializeField]
    public List<GameObject> numbers = new List<GameObject>();

    public List<Vector3> displayPos = new List<Vector3>();

    public Sprite n1, n2, n3, n4, n5, n6, n7, n8, n9, n10;
    private int curPtr;
    private int prev;
    private int next;
    public int[] slots;
    public int[] shownNumbers;
    //private GameObject number;
    // Use this for initialization
    void Start ()
	{
        //set lowerbound and topbound
        lowerbound = numbers[numbers.Count - 1].transform.position.y;
        topbound = numbers[0].transform.position.y;
	    diff = numbers[1].transform.position - numbers[0].transform.position;
        isMaxSpeed = false;
        //init for random numbers in reel
        curPtr = Random.Range(0, numbers.Count);
	    slots = new int[10];

	    displayPos.Add(transform.Find("DisplayPos1").position);
	    displayPos.Add(transform.Find("DisplayPos2").position);
	    displayPos.Add(transform.Find("DisplayPos3").position);

        //init for shown panel numbers
	    shownNumbers = new int [3];
        //set 1- 10 into slots
        //set probability, 10 means 10 numbers in reel
        SetProbability(GM.GameManager.probability);
	    
        //do shffule
        Shuffle(slots);   

        //assign to number objects
	    AssignNumber(slots);
	    DisplayOnPanel();

	}
	
	// Update is called once per frame
	void Update () {
	    DisplayOnPanel();
        //spin the reel
        if (GM.GameManager.isSpin)
	    {
            isDone = false;
	        IncreaseSpeed(numbers);
	        Spin();
	    }
	    else
	    {
            if(GM.GameManager.isStart)
	            DecreaseSpeed(numbers);
	    }
	}

    void RepeatMove(GameObject number, int index)
    {
        Vector3 pos = number.transform.position;
        if (number.transform.position.y < lowerbound)
        {
            number.transform.position = new Vector3(pos.x, topbound, pos.z);
            //fix relative distance
            if (index == numbers.Count - 1)
                number.transform.position = numbers[0].transform.position - diff;
            else
                number.transform.position = numbers[index + 1].transform.position - diff;
        }
    }

    void IncreaseSpeed(List<GameObject> numbers)
    {
        
        for (int i = 0; i < numbers.Count; i++)
        {
            
            //moving downward and gradually increase speed
            numbers[i].transform.Translate(Vector3.down * curspeed * Time.deltaTime);

            curspeed += acceleration * Time.deltaTime;

            Mathf.Clamp(curspeed, 0, GM.GameManager.maxspeed);
            Vector3 pos = numbers[i].transform.position;

            RepeatMove(numbers[i], i);
        }
        
    }

    void DecreaseSpeed(List<GameObject> numbers)
    {

        //stop spin with lower velocity
        for (int i = 0; i < numbers.Count; i++)
        {
            if (curspeed != 0)
                curspeed -= acceleration * Time.deltaTime;
            else
                isDone = true;
            //ready for stop       
            if (curspeed < 2 && curspeed > 0)
            {
                isMaxSpeed = true;
                curspeed = 2;
            }
            
            numbers[i].transform.Translate(Vector3.down * curspeed * Time.deltaTime);
            RepeatMove(numbers[i], i);

        }

        if (isMaxSpeed)
        {
            numbers[curPtr].transform.position = numbers[next].transform.position - diff;
            numbers[prev].transform.position = numbers[curPtr].transform.position - diff;
            //reset to old pos for fixed distance
            

            if (numbers[curPtr].transform.position.y <= displayPos[1].y &&
                numbers[curPtr].transform.position.y >= displayPos[2].y)
            {
                curspeed = 0;
                numbers[curPtr].transform.position = displayPos[1];
                numbers[prev].transform.position = displayPos[0];
                numbers[next].transform.position = displayPos[2];
                isMaxSpeed = false;
                
            }
        }
        
    }

    
    void Shuffle(int []slots)
    {
        for (int i = 0; i < slots.Length; i++)
        {   
            int r = Random.Range(i, slots.Length);
            Swap(ref slots[i], ref slots[r]);
        }
    }

    static void Swap(ref int x, ref int y)
    {
        int tempswap = x;
        x = y;
        y = tempswap;
    }
    void AssignNumber(int[] slots)
    {
        //change sprite pics
        for (int i = 0; i < slots.Length; i++)
        {
            SetSpritefromArray(numbers[i], slots[i]);
        }
    }

    void SetSpritefromArray(GameObject obj, int number)
    {
        switch(number)
        {
            case 1:
                obj.GetComponent<SpriteRenderer>().sprite = n1;
                obj.GetComponent<NumberVar>().number = 1;
                break;
            case 2:
                obj.GetComponent<SpriteRenderer>().sprite = n2;
                obj.GetComponent<NumberVar>().number = 2;
                break;
            case 3:
                obj.GetComponent<SpriteRenderer>().sprite = n3;
                obj.GetComponent<NumberVar>().number = 3;
                break;
            case 4:
                obj.GetComponent<SpriteRenderer>().sprite = n4;
                obj.GetComponent<NumberVar>().number = 4;
                break;
            case 5:
                obj.GetComponent<SpriteRenderer>().sprite = n5;
                obj.GetComponent<NumberVar>().number = 5;
                break;
            case 6:
                obj.GetComponent<SpriteRenderer>().sprite = n6;
                obj.GetComponent<NumberVar>().number = 6;
                break;
            case 7:
                obj.GetComponent<SpriteRenderer>().sprite = n7;
                obj.GetComponent<NumberVar>().number = 7;
                break;
            case 8:
                obj.GetComponent<SpriteRenderer>().sprite = n8;
                obj.GetComponent<NumberVar>().number = 8;
                break;
            case 9:
                obj.GetComponent<SpriteRenderer>().sprite = n9;
                obj.GetComponent<NumberVar>().number = 9;
                break;
            case 10:
                obj.GetComponent<SpriteRenderer>().sprite = n10;
                obj.GetComponent<NumberVar>().number = 10;
                break;
            default:
                Debug.Log("no such number for sprite");
                break;
        }
    }

    //function for set random number range into slots array
    void SetProbability(int numberRange)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = (i + 1) % (numberRange + 1);
            if (slots[i] == 0)
                slots[i] = Random.Range(1, numberRange);
        }
            
    }

    //function for get result numbers
    void Spin()
    {
        int randspin = Random.Range(0, GM.GameManager.spintimes);
        
        for (int i = 0; i < randspin; i++)
        {
            curPtr++;
            if (curPtr > 9)
                curPtr = 0;
        }
        prev = curPtr - 1;
        next = curPtr + 1;

        if (prev < 0)
            prev = 9;
        if (next > 9)
            next = 0;

        shownNumbers[0] = slots[prev];
        shownNumbers[1] = slots[curPtr];
        shownNumbers[2] = slots[next];

        //for(int i=0; i < shownNumbers.Length;i++)
            //Debug.Log(shownNumbers[i]);
    }

    void DisplayOnPanel()
    {
        Vector3 showarea = transform.position;
        
        foreach (GameObject number in numbers)
        {
            Vector3 originalpos = number.transform.position;
            //show in panel
            if (number.transform.position.y <= 4f && number.transform.position.y >= -3.5f)
                number.transform.position = new Vector3(originalpos.x, originalpos.y, showarea.z - 0.5f);
            else
                number.transform.position = originalpos;
        }
    }

    
}
