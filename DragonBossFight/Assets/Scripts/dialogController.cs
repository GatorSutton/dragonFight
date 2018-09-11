using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogController : MonoBehaviour {

    public bool isFinished = false;
    bool defenseSuccess = false;
    bool attackSuccess = false;
    bool dialogUIVisible = false;

    //list of conversation nodes which point to each other based on the responses
    List<ConversationNode> conversationNodes = new List<ConversationNode>();

    knightResponseController kRC;
    playerResponseController pRCRight;
    playerResponseController pRCLeft;

    knightAttack kA;
    knightTarget kT;

    public enum ResponseDirection { left, right};
    ResponseDirection lastResponse;
    int currentID = 0;
    ConversationNode currentConversation;

    private void Awake()
    {
        kRC = GameObject.Find("Canvas/KnightResponse").GetComponent<knightResponseController>();
        pRCRight = GameObject.Find("PlayerResponseRight").GetComponent<playerResponseController>();
        pRCLeft = GameObject.Find("PlayerResponseLeft").GetComponent<playerResponseController>();
        kA = GetComponent<knightAttack>();
        kT = GetComponent<knightTarget>();
    }

    // Use this for initialization
    void Start () {
        currentConversation = intro;
        conversationNodes.Add(intro);
        conversationNodes.Add(fear);
        conversationNodes.Add(confident);
        conversationNodes.Add(defense);
        conversationNodes.Add(offense);

        toggleConversation();
        //askQuestion(currentConversation);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            askQuestion(currentConversation);
        }
    }

    private void OnEnable()
    {
        playerResponseController.ResponseChosen += questionAnswered;
        knightTarget.taskComplete += targetSuccess;
        
    }

    private void OnDisable()
    {
        playerResponseController.ResponseChosen -= questionAnswered;
        knightTarget.taskComplete -= targetSuccess;
    }

    private void targetSuccess()
    {
        attackSuccess = true;
        toggleConversation();
        askQuestion(success);
    }

    private void dodgeSuccess()
    {
        defenseSuccess = true;
        toggleConversation();
        askQuestion(success);
    }

    private void askQuestion(ConversationNode cn)
    {
        if(defenseSuccess && attackSuccess)
        {
            cn = gameTime;
        }

        if(dialogUIVisible)
        {
            toggleConversation();
        }
        //toggleConversation();
        //Set Knight Question
        kRC.setSentence(cn.KnightQuestion);
        //Set player Responses
        pRCRight.setSentence(cn.rightResponse);
        pRCLeft.setSentence(cn.leftResponse);
    }

    private void toggleConversation()
    {
        dialogUIVisible = !dialogUIVisible;
        kRC.gameObject.SetActive(!kRC.gameObject.activeSelf);
        pRCRight.gameObject.SetActive(!pRCRight.gameObject.activeSelf);
        pRCLeft.gameObject.SetActive(!pRCLeft.gameObject.activeSelf);
    }

    private void questionAnswered(ResponseDirection direction)
    {
        
        lastResponse = direction;
        currentID = conversationNodes[currentID].directionToNextID(lastResponse);       //finds the next node in the list based on the last response
        currentConversation = conversationNodes.Find(x => x.id == currentID);
        print(currentID);

        if (currentID >= 0)
        {
            askQuestion(currentConversation);
        }
        else
        {
            toggleConversation();
            // switch between different events
            switch (currentID)
            {
                case -1:
                    print("attack players");
                    break;
                case -2:
                    print("spawn target");
                    kT.spawnTarget();
                    break;
                case -3:
                    print("start game");
                    break;
            }
        }
    }





    private ConversationNode intro = new ConversationNode(0, "So you have come to fight the dragon?", "We are scared", 1, "This will be easy", 2);
    private ConversationNode fear = new ConversationNode(1, "As you should be. Let's train before you begin.", "How do we attack?", 4, "How do we defend?", 3);
    private ConversationNode confident = new ConversationNode(2, "Pride comes before fall. Let's learn before we begin.", "How do we attack?", 4, "How do we defend?", 3);
    private ConversationNode defense = new ConversationNode(3, "Attacks will turn the ground beneath you red. Don't stand on them to stay healthy", "Bring it on!", -1, "Hit me with your best shot", -1);
    private ConversationNode offense = new ConversationNode(4, "Blast these targets with your crossbows to practice aiming", "OK", -2, "OK", -2);
    private ConversationNode success = new ConversationNode(5, "Great job. Complete both to continue. What next?", "Attack Practice", 4, "Defense Practice",  3);
    private ConversationNode failure = new ConversationNode(6, "I would try that again if I were you. What Next?", "Attack Practice", 4, "Defense Practice", 3);
    private ConversationNode gameTime = new ConversationNode(7, "What would you like to do?", "Practice a little more", 8, "Bring on the dragon!", -3);
    private ConversationNode training = new ConversationNode(8, "What would you like to practice?", "We would like to try dodging", 3, "We want more crossbow practice", 4);


}
