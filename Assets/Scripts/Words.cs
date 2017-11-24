using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Words : MonoBehaviour {

    public GameObject qCreated;                                 //Instantiated question blocks
    public GameObject Question;                                 //Question object prefab    
    public int current;                                         //No. of tries before game over, current question
    public string q, a, b;                                      //shuffled word, incorrect words for other two balls
    public static bool isThere, doCreate;                       //To know whether question is there, to stop new question once list exhausted
    private string[] questionList = new string[] {"EARTH",
                                                 "BURN",
                                                 "GEAR" };      //List of question words     

    private int[] questionOrder = new int[] { 0, 1, 2 };        //Order of question, length needs to be equal to number of question words
    private string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ", shuffle;
    private char[] incorrect = { 'a', 'a', 'a', 'a', 'a', 'a', 'a', 'a', 'a', 'a', 'a', 'a', 'a', 'a', 'a', 'a', 'a', 'a', 'a', 'a', 'a', 'a', 'a', 'a', 'a', 'a' };
    private bool running;

    void Start() {
        doCreate = true;
        current = 0;
        QuestionOrder(questionOrder);                           //Set order of questions in random order
        isThere = false;
    }

    private void Update(){
        //If question is not there, create one
        if (!isThere && doCreate) {
            if (current >= questionList.Length) {
                doCreate = false;
            }
            else {
                CreateQuestion(questionList[questionOrder[current]]);
                isThere = true;
                //Shuffle question, incorrect word array for other balls
                q = new string(RandomizeWord(questionList[questionOrder[current]].ToCharArray()));
                a = new string(RandomizeWord(IncorrectWords(questionList[questionOrder[current]].ToCharArray())));
                b = new string(RandomizeWord(IncorrectWords(questionList[questionOrder[current]].ToCharArray())));
                current++;
            }
        }

        //If all letters of question have been shown, load new question
        if (FallingBall.noOfIterations > questionList[questionOrder[current - 1]].Length && doCreate) {
            foreach (GameObject go in GameObject.FindGameObjectsWithTag("Question")) {
                Destroy(go);
            }
            if (!running)
            StartCoroutine(NextQuestionDelay());
        }
    }

    //Create multiple textboxes having question word, which get striked out as corresponding letter ball is shot
    public void CreateQuestion(string word) {
        for (int i = 0; i <= word.Length-1; i++) {
            qCreated = Instantiate(Question, new Vector3(-320+(i*35), 170, transform.position.z), transform.rotation);
            qCreated.transform.SetParent(GameObject.Find("Canvas").transform, false);
            qCreated.GetComponentInChildren<Text>().text = "" + word[i];
        }
    }

    //Delay next question till previous balls have been destroyed
    private IEnumerator NextQuestionDelay() {
        running = true;
        yield return new WaitForSeconds(1f);
        FallingBall.noOfIterations = 0;
        isThere = false;
        running = false;
    }

    //Shuffle question word, then show it sequentially in the falling balls
    public char[] RandomizeWord(char[] word) {
        for (int i = 0; i < word.Length; i++) {
            int r = UnityEngine.Random.Range(0, word.Length);
            char tmp = word[i];
            word[i] = word[r];
            word[r] = tmp;
        }
        return word;
    }

    //Shuffle integer array to form order of questions
    public void QuestionOrder(int[] array) {
        for (int i = 0; i < questionList.Length; i++) {
            int r = UnityEngine.Random.Range(0, questionList.Length);
            int tmp = array[i];
            array[i] = array[r];
            array[r] = tmp;
        }
    }

    //Create array having wrong letters to be displayed in other balls 
    public char[] IncorrectWords(char[] word) {
        int j = 0, i = 0, wordCnt = 0;
        while (i < alphabet.Length) {
            for (j = 0; j < word.Length; j++) {
                if (alphabet[i] != word[j]) {
                    wordCnt++;

                }
                else if (i != 0) {
                    incorrect[i] = incorrect[i - 1];
                }
                else incorrect[i] = 'Z';
            }
            if (wordCnt >= word.Length && alphabet[i] != 'a') {
                incorrect[i] = alphabet[i];
            }
            i++;
            wordCnt = 0;
        }
        return incorrect;
    }

    //Return current question
    public string CurrentQuestion() {
        return questionList[questionOrder[current-1]];
    }
}
