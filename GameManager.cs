using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    //SceneControl variable
    bool isStartMenuScene;
    bool isOneMultipleOneScene;

    //If game is start
    private bool isGameStart=false;
    //If game wait timer is activated
    private bool isGameWaitTimerOn = false;

    //timer for entire stage
    private float stage_timer=0;
    //timer for one question
    public int count_down_timer=10;    

    //number of correct question
    private int correct_question_number=0;
    //number of entire question in stage
    private int stage_question_number;
    //stage_question_number_const
    public const int k_stage_question_number = 10;
    //player_digit
    private List<int> player_number_digit_input;
    //isSetPlayerNumberDigit
    private bool isSetPlayerNumberDigits=false;

    //check countdown is timeover
    private bool isTimeOver=true;
    //check stagetimer start
    private bool isStageTimerStart = false;
    //Timer Coroutine
    private Coroutine count_down_coroutine;
    private Coroutine stage_timer_coroutine;

    //Answer of question
    private int question_answer;
    //Answer of player
    private int player_answer;
    //If answer is right
    bool isAnswerRight = false;   

    //External script  
    private QuestionGenerator questiongenerator_script;
    private QuestionScorer questionscorer_script;

    public UIManager uimanager_script;
    private bool isUIManagerNew = false;

    //user answer input
    private ArrayList player_answer_input;

    //AdMob -광고
    public AdMobManager adMobManager;
    private bool isAdMobActivated=false;
    

    private void Awake()
    {
        //모바일 해상도 설정(Galaxy Note9기준)
        Screen.SetResolution(720, 1480,true);

        DontDestroyOnLoad(this.gameObject);

        //StartScene이 다시 로드될때 DontDestroyOnLoad했던 GameObject들이 Duplicate되는 것 방지
        if(FindObjectsOfType(GetType()).Length>1)
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start ()
    {
       
    }
	
	// Update is called once per frame
    //Main과 같은 역할
	void Update ()
    {
        //If Current Scene is StartMenuScene
        if (SceneManager.GetActiveScene().name=="StartMenuScene")
        {
            if(isAdMobActivated==false)
            {
                isAdMobActivated = true;
                adMobManager.ShowInterstitialAd();               
            }

            uimanager_script = GameObject.Find("UIManager").GetComponent<UIManager>();
            
            if(uimanager_script.IsStartButtonPressed()==true)
            {
                SetPlayerNumberDigits();
            }                  

        }
        //If Current Scene is MultiplyQuestionScene
        else if (SceneManager.GetActiveScene().name=="MultiplyQuestionScene")
        {            

            if (isUIManagerNew == false)
            {
                isUIManagerNew = true;
                uimanager_script = GameObject.Find("UIManager").GetComponent<UIManager>();
                questiongenerator_script = GameObject.Find("QuestionGenerator").GetComponent<QuestionGenerator>();
                questionscorer_script = GameObject.Find("QuestionScorer").GetComponent<QuestionScorer>();
            }

            //If game is start
            if (isGameStart==true)
            {               

                uimanager_script.ShowStageQuestionNumber(stage_question_number);

                //If question remains
                if (stage_question_number != 0)
                {
                    StartStageTimer();

                    //문제 풀다가 제한시간이 다되면
                    if (isTimeOver == true)
                    {
                        isTimeOver = false;
                        //Stage에 따른 곱셈 자릿수설정(***여기 버튼에따라 자릿수달라지게하는거하기)                       

                        //문제 생성
                        question_answer = StartQuestion(player_number_digit_input);
                        //문제 카운트 다운 시작
                        StartCountdown();

                    }

                    //제한시간이 안되었는데 Player가 문제답을 제출했다면
                    if (isTimeOver == false)
                    {
                        Debug.Log("문제답제출" + uimanager_script.IsPlayerSubmitAnswer());

                        if (uimanager_script.IsPlayerSubmitAnswer() == true)
                        {

                            player_answer = uimanager_script.GetPlayerAnswer();

                            isAnswerRight = questionscorer_script.ScoreQuestion(question_answer, player_answer);

                            //If answer is right
                            if (isAnswerRight == true)
                            {
                                //isAnswerRight = false;

                                if (uimanager_script.GetIsCorrectImageActivated() == false)
                                {
                                    //Show correct image to player                           
                                    StartCoroutine(uimanager_script.ShowCorrectImageForSeconds(0.8f));

                                }

                                if (uimanager_script.GetIsCorrectImageFinished() == true)
                                {                                    
                                    uimanager_script.SetIsCorrectImageFinished(false);
                                    //Show correct Image to player
                                    StopCountDown();                                   

                                }

                            }
                            //If answer is wrong
                            else
                            {
                                if (uimanager_script.GetIsWrongImageActivated() == false)
                                {
                                    //Show wrong image to player
                                    StartCoroutine(uimanager_script.ShowWrongImageForSeconds(0.8f));
                                    uimanager_script.SetIsPlayerSubmitAnswerFalse();
                                }

                            }

                            //If answer is wrong

                        }
                    }

                }
                //If game is end
                else
                {
                    isUIManagerNew = false;
                    isGameStart = false;
                    isGameWaitTimerOn = false;
                    SceneManager.LoadScene("EndMenuScene");
                }
            }

            //Before Game Start
            else
            {
                //Wait 4 seconds before game starts ( 3  seconds + game start )
                if(isGameWaitTimerOn==false)
                {
                    isGameWaitTimerOn = true;
                    isSetPlayerNumberDigits = false;
                    stage_timer = 0;
                    //stage_question_number = k_stage_question_number;
                    Debug.Log("+" + player_number_digit_input.Count);
                    Debug.Log("++" + player_number_digit_input[2]);
                    stage_question_number = player_number_digit_input[2];
                    correct_question_number = 0;
                    isGameWaitTimerOn = true;
                    StartCoroutine(WaitBeforeGameStart(4));
                }                
            }

        }
        //If Current Scene is EndMenuScene
        else if(SceneManager.GetActiveScene().name=="EndMenuScene")
        {
            uimanager_script.ShowCorrectQuestionNumber(correct_question_number,player_number_digit_input[2]);
            uimanager_script.ShowStageTimer(stage_timer);
        }
	}
    
    public void SetPlayerNumberDigits()
    {       
        player_number_digit_input = uimanager_script.GetPlayerNumberDigitsAndQuestionNumbers();        
    }

    public int StartQuestion(List<int> digits)
    {       
        return questiongenerator_script.GenerateQuestion(digits);
    }

    public void StartCountdown()
    {
        count_down_coroutine = StartCoroutine(Countdown(count_down_timer));
    }

    public void StopCountDown()
    {
        StopCoroutine(count_down_coroutine);
        //Erase player answer on UI when time is up
        uimanager_script.GetNumberDeleteButtonFromPlayer();
        uimanager_script.SetIsCorrectImageActivated(false);       

        //If answer is wrong
        //제출한 정답이 틀렸거나 정답을 제출하지않았거나
        if (isAnswerRight == false || uimanager_script.IsPlayerSubmitAnswer() == false)
        {
            Debug.Log("틀림!");
            --stage_question_number;
            uimanager_script.SetIsPlayerSubmitAnswerFalse();
            StartCoroutine(WaitForSecondsTimeIsUp(0.8f));
        }
        //If answer is correct
        else
        {
            Debug.Log("정답!");
            ++correct_question_number;
            --stage_question_number;
            uimanager_script.SetIsPlayerSubmitAnswerFalse();
            isTimeOver = true;
        }       
       
    }

    public IEnumerator Countdown(int time)
    {
        while (true)
        {
            uimanager_script.ShowCountDownTimer(time--);
            yield return new WaitForSeconds(1);

            if(time==0)
            {
                StopCountDown();                
            }
        }
    }

    private void StartStageTimer()
    {
        stage_timer += Time.deltaTime;        
    }

    public IEnumerator WaitForSecondsTimeIsUp(float time)
    {
        //Show wrong image when time is up
        StartCoroutine(uimanager_script.ShowWrongImageForSeconds(time));
        yield return new WaitForSeconds(time);

        isTimeOver = true;
        yield break;
    }

    public IEnumerator WaitBeforeGameStart(float time)
    {
        while(true)
        {            
            uimanager_script.ShowWaitTimerBeforeGameStart(--time);
            if(time>-1)
            {
                yield return new WaitForSeconds(1);
            }   
            else
            {
                isGameStart = true;
                isGameWaitTimerOn = false;
                yield break;
            }            
        }       
        
    }

    //public IEnumerator StageTimeCount()
    //{
    //    while(true)
    //    {
    //        yield return new WaitForSeconds(1);
    //        uimanager_script.ShowStageTimer(stage_timer++);           
    //    }
    //}   

}
