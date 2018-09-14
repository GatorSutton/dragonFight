using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogController : MonoBehaviour {

    public bool isFinished = false;
    public bool defenseSuccess = false;
    public bool attackSuccess = false;
    bool dialogUIVisible = false;
    Animator anim;
    BoxCollider bC;

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
        anim = GetComponent<Animator>();
        bC = GetComponent<BoxCollider>();
    }

    // Use this for initialization
    void Start () {
        currentConversation = intro;
        conversationNodes.Add(intro);
        conversationNodes.Add(fear);
        conversationNodes.Add(confident);
        conversationNodes.Add(defense);
        conversationNodes.Add(offense);
        conversationNodes.Add(success);
        conversationNodes.Add(gameTime);
        conversationNodes.Add(training);
        toggleConversation();
        //askQuestion(currentConversation);
    }


    private void OnEnable()
    {
        playerResponseController.ResponseChosen += questionAnswered;
        knightTarget.taskComplete += targetSuccess;
        knightAttack.TaskComplete += dodgeSuccess;
        
    }

    private void OnDisable()
    {
        playerResponseController.ResponseChosen -= questionAnswered;
        knightTarget.taskComplete -= targetSuccess;
        knightAttack.TaskComplete -= dodgeSuccess;
    }

    public void startDialog()
    {
        bC.enabled = false;
        StartCoroutine(askQuestion(currentConversation));
    }

    private void targetSuccess()
    {
        attackSuccess = true;
        toggleConversation();
        currentID = success.id;
        StartCoroutine(askQuestion(success));
    }

    private void dodgeSuccess()
    {
        defenseSuccess = true;
        toggleConversation();
        currentID = success.id;
        StartCoroutine(askQuestion(success));
    }

    private IEnumerator askQuestion(ConversationNode cn)
    {
        if(defenseSuccess && attackSuccess)
        {
            cn = gameTime;
            currentID = cn.id;
        }

        if(dialogUIVisible)
        {
            toggleConversation();
        }
        //toggleConversation();
        //Set Knight Question
        yield return StartCoroutine(kRC.setSentence(cn.KnightQuestion));
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
            StartCoroutine(askQuestion(currentConversation));
        }
        else
        {
            toggleConversation();
            // switch between different events
            switch (currentID)
            {
                case -1:
                    print("attack players");
                    kA.startSnakeSwing();
                    break;
                case -2:
                    print("spawn target");
                    kT.spawnTargets();
                    break;
                case -3:
                    //start coroutine to kick players into pit
                    StartCoroutine(kickOff());
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
    private ConversationNode gameTime = new ConversationNode(6, "What would you like to do?", "Practice a little more", 7, "Bring on the dragon!", -3);
    private ConversationNode training = new ConversationNode(7, "What would you like to practice?", "We would like to try dodging", 3, "We want more crossbow practice", 4);


    private IEnumerator kickOff()
    {
        anim.SetBool("Walk", true);
        yield return new WaitForSeconds(1.3f);
        anim.SetBool("Walk", false);
        anim.SetTrigger("Kick");
        yield return new WaitForSeconds(.5f);
        isFinished = true;
        yield return new WaitForSeconds(3f);
        this.gameObject.SetActive(false);
    }


}
