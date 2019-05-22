using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionScorer : MonoBehaviour {   

    //External variable
    public UIManager uimanager_script;

    private void Awake()
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

    public bool ScoreQuestion(int question_answer, int player_input_answer)
    {
        if(question_answer==player_input_answer)
        {
            return true;
        }

        return false;
    }
}
