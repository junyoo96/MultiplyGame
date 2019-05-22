using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//UIManager and SceneManager
public class UIManager : MonoBehaviour{

    //StartScene UI variables
    List<int> player_number_digits_input;
    private GameObject playerQuestionNumberInputield;
    int player_question_number;

    //MultiplyQuestionScene UI variables
    private GameObject questionText;
    private GameObject timelimitText;
    private GameObject playeranswerinputText;    
    private GameObject correctImage;
    private GameObject wrongImage;
    private GameObject gameWaitTimerText;
    private GameObject stageQuestionNumberText;

    //EndScene UI variables
    private GameObject stageTimeText;
    private GameObject correctQuestionNumberText;

    //Check if correctImage activated 
    private bool isCorrectImageActivated=false;
    //Check if correctImage finished
    private bool isCorrectImageFinished = false;
    //Check if wrongImage activated
    private bool isWrongImageActivated=false;
    //Check if player press Start Button
    private bool isStartButtonPressed = false;

    //Player answer input
    private List<int> player_answer_input;
    private string player_answer_string;
    private bool isPlayerSubmitAnswer=false;  

    // Use this for initialization
    void Start () {

        if(SceneManager.GetActiveScene().name=="StartMenuScene")
        {
            player_number_digits_input = new List<int>();
            playerQuestionNumberInputield = GameObject.Find("PlayerQuestionNumberInputField");
        }

        //SceneManager.LoadScene("StartMenuScene");
        if (SceneManager.GetActiveScene().name == "MultiplyQuestionScene")
        {
            player_answer_input = new List<int>();            
         
        }

    }
	
	// Update is called once per frame
	void Update () {
       
    }

    //***********************StartMenuScene UI*******************************
    public void LoadMultiplyQuestionScene()
    {
        SceneManager.LoadScene("MultiplyQuestionScene");
    }

    //public int GetPlayerQuestionNumbers()
    //{

    //}

    //public void GetPlayerQuestionNumberInput(string question_number_input)
    //{
    //    Debug.Log("What"+question_number_input);
    //    //return question_number_input;
    //}

    public List<int> GetPlayerNumberDigitsAndQuestionNumbers()
    {
        List<int> digits;
        digits = player_number_digits_input;
        Debug.Log("fdf"+playerQuestionNumberInputield.GetComponent<InputField>().text);
        digits.Add(int.Parse(playerQuestionNumberInputield.GetComponent<InputField>().text));
        //player_number_digits_input.Clear();
        isStartButtonPressed = false;
           
        return digits;
    }

    public void GetPlayerStartButton()
    {
        isStartButtonPressed = true;
        LoadMultiplyQuestionScene();
    }

    public bool IsStartButtonPressed()
    {
        return isStartButtonPressed;
    }

    public void GetOneMultiplyOneButton()
    {
        player_number_digits_input.Clear();
        player_number_digits_input.Add(1);
        player_number_digits_input.Add(1);
        //SceneManager.LoadScene("MultiplyQuestionScene");
    }

    public void GetOneMultiplyTwoButton()
    {
        player_number_digits_input.Clear();
        player_number_digits_input.Add(1);
        player_number_digits_input.Add(2);
        //SceneManager.LoadScene("MultiplyQuestionScene");
    }

    public void GetOneMultiplyThreeButton()
    {
        player_number_digits_input.Clear();
        player_number_digits_input.Add(1);
        player_number_digits_input.Add(3);
        //SceneManager.LoadScene("MultiplyQuestionScene");
    }   

    public void GetTwoMultiplyOneButton()
    {
        player_number_digits_input.Clear();
        player_number_digits_input.Add(2);
        player_number_digits_input.Add(1);
        //SceneManager.LoadScene("MultiplyQuestionScene");
    }

    public void GetTwoMultiplyTwoButton()
    {
        player_number_digits_input.Clear();
        player_number_digits_input.Add(2);
        player_number_digits_input.Add(2);
        //SceneManager.LoadScene("MultiplyQuestionScene");
    }

    public void GetTwoMultiplyThreeButton()
    {
        player_number_digits_input.Clear();
        player_number_digits_input.Add(2);
        player_number_digits_input.Add(3);
        //SceneManager.LoadScene("MultiplyQuestionScene");
    }

    public void GetThreeMultiplyOneButton()
    {
        player_number_digits_input.Clear();
        player_number_digits_input.Add(3);
        player_number_digits_input.Add(1);
        //SceneManager.LoadScene("MultiplyQuestionScene");
    }

    public void GetThreeMultiplyTwoButton()
    {
        player_number_digits_input.Clear();
        player_number_digits_input.Add(3);
        player_number_digits_input.Add(2);
        //SceneManager.LoadScene("MultiplyQuestionScene");
    }

    public void GetThreeMultiplyThreeButton()
    {
        player_number_digits_input.Clear();
        player_number_digits_input.Add(3);
        player_number_digits_input.Add(3);
        //SceneManager.LoadScene("MultiplyQuestionScene");
    }

    //***********************MultiplyQuestionScene*******************************    
    public void ShowQuestion(List<int> random_numbers)
    {        
        if(questionText==null)
        {
            questionText = GameObject.Find("Question");
        }
        questionText.GetComponent<Text>().text = random_numbers[0] + "X" + random_numbers[1];

    }

    public void ShowCountDownTimer(int time)
    {       
        if(timelimitText==null)
        {
            timelimitText = GameObject.Find("TimeLimit");
        }
        timelimitText.GetComponent<Text>().text = "남은시간 : "+time;
    }

    

    public void ShowPlayerAnswerInput()
    {
        if(playeranswerinputText==null)
        {
            playeranswerinputText = GameObject.Find("PlayerAnswerInput");
        }

        if(player_answer_input.Count!=0)
        {
            int[] answer_tmp = player_answer_input.ToArray();
            string[] answer_string_tmp = new string[answer_tmp.Length];
            
            for(int i=0; i<answer_string_tmp.Length; ++i)
            {
                answer_string_tmp[i] = answer_tmp[i].ToString();
            }

            player_answer_string = string.Join("", answer_string_tmp);

            playeranswerinputText.GetComponent<Text>().text = player_answer_string;
        }
        else
        {
            playeranswerinputText.GetComponent<Text>().text = "";
        }
       
    }
    
    //Functions about Keypad Button
    public void GetNumberZeroButtonFromPlayer()
    {
        player_answer_input.Add(0);
        ShowPlayerAnswerInput();
    }
    public void GetNumberOneButtonFromPlayer()
    {
        player_answer_input.Add(1);
        ShowPlayerAnswerInput();
    }
    public void GetNumberTwoButtonFromPlayer()
    {
        player_answer_input.Add(2);
        ShowPlayerAnswerInput();
    }
    public void GetNumberThreeButtonFromPlayer()
    {
        player_answer_input.Add(3);
        ShowPlayerAnswerInput();
    }
    public void GetNumberFourButtonFromPlayer()
    {
        player_answer_input.Add(4);
        ShowPlayerAnswerInput();
    }
    public void GetNumberFiveButtonFromPlayer()
    {
        player_answer_input.Add(5);
        ShowPlayerAnswerInput();
    }
    public void GetNumberSixButtonFromPlayer()
    {
        player_answer_input.Add(6);
        ShowPlayerAnswerInput();
    }
    public void GetNumberSevenButtonFromPlayer()
    {
        player_answer_input.Add(7);
        ShowPlayerAnswerInput();
    }
    public void GetNumberEightButtonFromPlayer()
    {
        player_answer_input.Add(8);
        ShowPlayerAnswerInput();
    }
    public void GetNumberNineButtonFromPlayer()
    {
        player_answer_input.Add(9);
        ShowPlayerAnswerInput();
    }

    public void GetNumberDeleteButtonFromPlayer()
    {       
        player_answer_input.Clear();
        ShowPlayerAnswerInput();
    }

    public void GetAnswerButtonFromPlayer()
    {
        if(player_answer_input.Count==0)
        {
            isPlayerSubmitAnswer = false;
        }
        else
        {
            isPlayerSubmitAnswer = true;
        }
            
    }

    public int GetPlayerAnswer()
    {
        player_answer_input.Clear();
        ShowPlayerAnswerInput();
        return int.Parse(player_answer_string);
    }

    //Check if player submit answer
    public bool IsPlayerSubmitAnswer()
    {
        return isPlayerSubmitAnswer;
    }

    public void SetIsPlayerSubmitAnswerFalse()
    {
        isPlayerSubmitAnswer = false;
    }

    public IEnumerator ShowCorrectImageForSeconds(float time)
    {
        int time_count = 1;

        if (correctImage == null)
        {
            correctImage = GameObject.Find("CorrectImage");
        }
        while (true)
        {
            if(isCorrectImageActivated==false)
            {
                
                isCorrectImageActivated = true;               
                //Hide correct image after few seconds
                correctImage.GetComponent<Image>().enabled = true;               
            }

            yield return new WaitForSeconds(time);
            --time_count;

            if(time_count==0)
            {               
                //Hide wrong image after few seconds

                if(correctImage!=null)
                {
                    correctImage.GetComponent<Image>().enabled = false;
                }                            
                //이거 넣으면 두번돌음
                //yield return new WaitForSeconds(0.5f);
                isCorrectImageFinished = true;
               
                //Stop current coroutine
                yield break;
            }
            
        }
        
    }   

    public IEnumerator ShowWrongImageForSeconds(float time)
    {
        int time_count = 1;

        if (wrongImage == null)
        {
            wrongImage = GameObject.Find("WrongImage");
        }
        while (true)
        {
            if (isWrongImageActivated == false)
            {
                isWrongImageActivated = true;
                //Hide correct image after few seconds
                wrongImage.GetComponent<Image>().enabled = true;
            }

            yield return new WaitForSeconds(time);
            --time_count;

            if (time_count == 0)
            {
                isWrongImageActivated = false;
                //Hide wrong image after few seconds
                if(wrongImage!=null)
                {
                    wrongImage.GetComponent<Image>().enabled = false;
                }                
                //Stop current coroutine
                yield break;
            }

        }
    }    

    public bool GetIsCorrectImageActivated()
    {
        return isCorrectImageActivated;
    }

    public bool GetIsWrongImageActivated()
    {
        return isWrongImageActivated;
    }

    public bool GetIsCorrectImageFinished()
    {
        return isCorrectImageFinished;
    }

    public void SetIsCorrectImageActivated(bool activated)
    {
        isCorrectImageActivated = activated;
    }

    public void SetIsCorrectImageFinished(bool finished)
    {
        isCorrectImageFinished = finished;        
    }

    public void ShowStageQuestionNumber(int stage_question_number)
    {
        if(stageQuestionNumberText==null)
        {
            stageQuestionNumberText = GameObject.Find("StageQuestionNumber");
        }

        stageQuestionNumberText.GetComponent<Text>().text = ""+stage_question_number;
    }

    public void ShowWaitTimerBeforeGameStart(float time)
    {
        if(gameWaitTimerText==null)
        {
            gameWaitTimerText = GameObject.Find("GameWaitTimer");
        }

        if(time>0)
        {
            gameWaitTimerText.GetComponent<Text>().text = "" + time;
        }
        else if(time==0)
        {
            gameWaitTimerText.GetComponent<Text>().text = "Game Start!";
        }
        else
        {
            gameWaitTimerText.GetComponent<Text>().text = "";
        }
        
    }

    //***********************EndMenuScene******************************* 
    public void ShowCorrectQuestionNumber(int correct_question_number,int stage_question_number)
    {
        if(correctQuestionNumberText==null)
        {
            correctQuestionNumberText = GameObject.Find("CorrectQuestionNumber");
        }
        correctQuestionNumberText.GetComponent<Text>().text = "맞힌개수: "+correct_question_number + "/" + stage_question_number;
    }

    //Show stage time after game is over
    public void ShowStageTimer(float time)
    {
        if (stageTimeText == null)
        {
            stageTimeText = GameObject.Find("StageTime");
        }
        stageTimeText.GetComponent<Text>().text = "걸린 시간 : " + time;
    }

    public void GetRetryButtonPlayer()
    {
        SceneManager.LoadScene("MultiplyQuestionScene");
    }

    public void GetGoToMainMenuButtoPlayer()
    {
        SceneManager.LoadScene("StartMenuScene");
    }

}
