using UnityEngine;
using System.Collections;

public class Quest
{
    private int hostNPCNumber;
    private int targetNPCNum;
    private int reward;
    private bool progressing;
    private bool complete;

    /* informatino for BattleQuest */
    private float warp_position_x;
    private float warp_position_y;
    private int warp_map_scene;

    public Quest(int hostNumber, int targetNum, int _reward)
    {
        hostNPCNumber = hostNumber;
        targetNPCNum = targetNum;
        reward = _reward;
        progressing = false;
        complete = false;
    }

    public void QuestClear()
    {
        complete = true;
        progressing = false;
        GameManager.pl_stored.GetComponent<CharacterStatus>().addMoney(reward);
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

    public int getHostNPCNumber()
    {
        return hostNPCNumber;
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
