using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionGenerator : MonoBehaviour {

    private int number_one;
    private int number_two;
    private int question_answer;

    //External variable
    public UIManager uimanager_script;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        //StartScene이 다시 로드될때 DontDestroyOnLoad했던 GameObject들이 Duplicate되는 것 방지
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public int GenerateQuestion(List<int> digits)
    {
        number_one = MakeRandomNumber(digits[0]);
        number_two = MakeRandomNumber(digits[1]);

        List<int> random_numbers = new List<int>();
        random_numbers.Add(number_one);
        random_numbers.Add(number_two);        

        if(uimanager_script==null)
        {
            uimanager_script = GameObject.Find("UIManager").GetComponent<UIManager>();
        }

        //Player에게 문제를 보여주는 코드
        uimanager_script.ShowQuestion(random_numbers);

        return number_one*number_two;       
    }

    //Make random number based on digit
    public int MakeRandomNumber(int digit)
    {       
        if (digit==1)
        {
            //Generate random number from 0 to 9
            int tmp = Random.Range(1, 10);
           
            return tmp;
        }
        else if(digit==2)
        {
            //Generate random number from 10 to 99
            int tmp = Random.Range(10, 100);
           
            return tmp;
        }
        else if (digit == 3)
        {
            //Generate random number from 100 to 999
            int tmp = Random.Range(100, 1000);

            return tmp;
        }

        return -1;
    }
}
