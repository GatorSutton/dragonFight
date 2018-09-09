using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogController : MonoBehaviour {

    //list of conversation nodes which point to each other based on the responses
    List<ConversationNode> conversationNodes = new List<ConversationNode>();

    knightResponseController kRC;
    playerResponseController pRCRight;
    playerResponseController pRCLeft;

    public enum ResponseDirection { left, right};
    ResponseDirection lastResponse;
    int currentID = 0;
    ConversationNode currentConversation;

    private void Awake()
    {
        kRC = GameObject.Find("Canvas/KnightResponse").GetComponent<knightResponseController>();
        pRCRight = GameObject.Find("PlayerResponseRight").GetComponent<playerResponseController>();
        pRCLeft = GameObject.Find("PlayerResponseLeft").GetComponent<playerResponseController>();
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
            toggleConversation();
            askQuestion(currentConversation);
        }
    }

    private void OnEnable()
    {
        playerResponseController.ResponseChosen += questionAnswered;
    }

    private void OnDisable()
    {
        playerResponseController.ResponseChosen -= questionAnswered;
    }

    /*
    private void askQuestion(string knightTalk, string playerResponseRight, string playerResponseLeft)
    {
        //Set Knight Question
        kRC.setSentence(knightTalk);
        //Set player Responses
        pRCRight.setSentence(playerResponseRight);
        pRCLeft.setSentence(playerResponseLeft);
    }
    */

    private void askQuestion(ConversationNode cn)
    {
        //toggleConversation();
        //Set Knight Question
        kRC.setSentence(cn.KnightQuestion);
        //Set player Responses
        pRCRight.setSentence(cn.rightResponse);
        pRCLeft.setSentence(cn.leftResponse);
    }

    private void toggleConversation()
    {
        kRC.gameObject.SetActive(!kRC.gameObject.activeSelf);
        pRCRight.gameObject.SetActive(!pRCRight.gameObject.activeSelf);
        pRCLeft.gameObject.SetActive(!pRCLeft.gameObject.activeSelf);
    }

    private void questionAnswered(ResponseDirection direction)
    {
        lastResponse = direction;
        currentID = conversationNodes[currentID].directionToNextID(lastResponse);       //finds the next node in the list based on the last response
        currentConversation = conversationNodes.Find(x => x.id == currentID);

        if (currentID != -1)
        {
            askQuestion(currentConversation);
        }
        else
        {
            toggleConversation();
        }
    }

    IEnumerator converstion()
    {
        
        yield return null;
    }


    private ConversationNode intro = new ConversationNode(0, "So you have come to fight the dragon?", "We are scared", 1, "This will be easy", 2);
    private ConversationNode fear = new ConversationNode(1, "As you should be. Let's train before you begin.", "How do we attack?", 3, "How do we defend?", 3);
    private ConversationNode confident = new ConversationNode(2, "Pride comes before fall. Let's learn before we begin.", "How do we attack?", 3, "How do we defend?", 3);
    private ConversationNode defense = new ConversationNode(3, "Attacks will turn the ground beneath you red. Don't stand on them to stay healthy", "Bring it on!", -1, "Hit me with your best shot", -1);
    private ConversationNode offense = new ConversationNode(4, "Blast these targets with your crossbows to practice aiming", "OK", -1, "OK", -1);
}
