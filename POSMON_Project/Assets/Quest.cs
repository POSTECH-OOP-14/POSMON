using UnityEngine;
using System.Collections;

public class Quest
{
    private string hostNPCName;
    private int targetNPCNum;
    private int reward;
    private bool progressing;
    private bool complete;

    public Quest(string hostName, int targetNum, int _reward)
    {
        hostNPCName = hostName;
        targetNPCNum = targetNum;
        reward = _reward;
        progressing = false;
        complete = false;
    }

    public void QuestClear()
    {
        complete = true;
        progressing = false;
        GameObject.Find(hostNPCName).GetComponent<Dialogue>().ChangeDialogueToDefault();
    }

    public bool isCompleted()
    {
        return complete;
    }

    public int getTarget()
    {
        return targetNPCNum;
    }

    public void ChangeTarget(int target)
    {
        targetNPCNum = target;
    }

    public int getReward()
    {
        return reward;
    }

    public string getHostNPCName()
    {
        return hostNPCName;
    }

    public bool getProgress()
    {
        return progressing;
    }

    public void setProgress(bool progress)
    {
        progressing = progress;
    }
}
