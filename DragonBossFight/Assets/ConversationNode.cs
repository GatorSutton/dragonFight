using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationNode{

    public int id;
    public string KnightQuestion;
    public string leftResponse;
    public int leftNextid;
    public string rightResponse;
    public int rightNextid;

    public ConversationNode(int aId, string aKnightQuestion, string aLeftResponse, int aLeftNextid, string aRightResponse, int aRightNextid)
    {
        id = aId;
        KnightQuestion = aKnightQuestion;
        leftResponse = aLeftResponse;
        leftNextid = aLeftNextid;
        rightResponse = aRightResponse;
        rightNextid = aRightNextid;
    }

    public int directionToNextID(dialogController.ResponseDirection direction)
    {
        int id = 0;
        if (direction == dialogController.ResponseDirection.left)
        {
            id = leftNextid;
        }
        if (direction == dialogController.ResponseDirection.right)
        {
            id = rightNextid;
        }

        return id;
    }


}