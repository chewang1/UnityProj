using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReelSpin : MonoBehaviour
{
    
    private float numDiff = 3;
    //boundaries for numbers
    private float lowerbound;
    private float topbound;

    //increase speed
    private float curspeed = 0;
    private float acceleration = 1;
    [SerializeField]
    public List<GameObject> numbers = new List<GameObject>();


    public Sprite n1, n2, n3, n4, n5, n6, n7, n8, n9, n10;
    private int curPtr;
    public int[] slots;
    public int[] shownNumbers;
    //private GameObject number;
    // Use this for initialization
    void Start ()
	{
        //set lowerbound and topbound
        lowerbound = numbers[numbers.Count - 1].transform.position.y;
        topbound = numbers[0].transform.position.y + numDiff;

        //init for random numbers in reel
	    curPtr = Random.Range(0, numbers.Count);
	    slots = new int[10];

        //init for shown panel numbers
	    shownNumbers = new int [3];
        //set 1- 10 into slots
        //set probability, 10 means 10 numbers in reel
        setProbability(GM.GameManager.probability);
	    
        //do shffule
        shuffle(slots);   

        //assign to number objects
	    assignNumber(slots);

	}
	
	// Update is called once per frame
	void Update () {
        //spin the reel
	    if (GM.GameManager.isSpin)
	    {
	        foreach (GameObject number in numbers)
	        {
	            //moving downward and gradually increase speed
	            number.transform.Translate(Vector3.down * curspeed * Time.deltaTime);
	            curspeed += acceleration * Time.deltaTime;
	            
                Mathf.Clamp(curspeed, 0 ,GM.GameManager.maxspeed);
                Vector3 pos = number.transform.position;

	            if (number.transform.position.y < lowerbound)
	            {
	                number.transform.position = new Vector3(pos.x, topbound, pos.z);
	                //float diff = numbers[9].transform.position.y - numbers[0].transform.position.y;
	                //Debug.Log(diff);
	            }
	        }

	        Spin();
	        //GM.GameManager.GetResult();
	    }
	    else
	    {
            //stop spin with lower velocity
	        foreach (GameObject number in numbers)
	        {
	            //curspeed = Mathf.Lerp(0, GM.GameManager.maxspeed, Time.deltaTime);
                //number.transform.Translate(Vector3.down * curspeed * Time.deltaTime);
                
	        }
	    }
	}

    void shuffle(int []slots)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            
            int r = Random.Range(i, slots.Length);
            //swap
            int temp = slots[i];
            slots[i] = slots[r];
            slots[r] = temp;

        }
    }

    void assignNumber(int[] slots)
    {
        //change sprite pics
        for (int i = 0; i < slots.Length; i++)
        {
            setSpritefromArray(numbers[i], slots[i]);
        }
    }

    void setSpritefromArray(GameObject obj, int number)
    {
        switch(number)
        {
            case 1:
                obj.GetComponent<SpriteRenderer>().sprite = n1;
                break;
            case 2:
                obj.GetComponent<SpriteRenderer>().sprite = n2;
                break;
            case 3:
                obj.GetComponent<SpriteRenderer>().sprite = n3;
                break;
            case 4:
                obj.GetComponent<SpriteRenderer>().sprite = n4;
                break;
            case 5:
                obj.GetComponent<SpriteRenderer>().sprite = n5;
                break;
            case 6:
                obj.GetComponent<SpriteRenderer>().sprite = n6;
                break;
            case 7:
                obj.GetComponent<SpriteRenderer>().sprite = n7;
                break;
            case 8:
                obj.GetComponent<SpriteRenderer>().sprite = n8;
                break;
            case 9:
                obj.GetComponent<SpriteRenderer>().sprite = n9;
                break;
            case 10:
                obj.GetComponent<SpriteRenderer>().sprite = n10;
                break;
            default:
                Debug.Log("no such number for sprite");
                break;
        }
    }

    //function for set random number range into slots array
    void setProbability(int numberRange)
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
            if (curPtr > 8)
                curPtr = 0;
        }

        int prev = curPtr - 1;
        int next = curPtr + 1;

        if (prev < 0)
            prev = 8;
        if (next > 8)
            next = 0;

        shownNumbers[0] = slots[prev];
        shownNumbers[1] = slots[curPtr];
        shownNumbers[2] = slots[next];

        //for(int i=0; i < shownNumbers.Length;i++)
            //Debug.Log(shownNumbers[i]);
    }


}
