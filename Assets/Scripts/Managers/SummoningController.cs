using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummoningController : MonoBehaviour
{
    public SummoningCircle northCircle, southCircle, eastCircle, westCircle;
    public GameObject summoningCircle;
    private GameObject working;
    public OwnTime ownTime;
    [SerializeField]
    private int summoningCooldown, summoningDifficulty, summoningPattern, summoningRotation;
    private bool summoningNorth, summoningSouth, summoningEast, summoningWest;
    private bool successfulSummon1=true, successfulSummon2=true,successfulSummon3=true,successfulSummon4=true;
    private bool failedSummon=false;
    int numFailed = 0;
    // Start is called before the first frame update
    void Start()
    {
        //Initalize(summoningCircle);
        working=Instantiate(summoningCircle);
        northCircle=working.GetComponent<SummoningCircle>();
        northCircle.startingX = 19;
        northCircle.startingY = 17;
        working = Instantiate(summoningCircle);
        southCircle = working.GetComponent<SummoningCircle>();
        southCircle.startingX = 20;
        southCircle.startingY = 2;
        working = Instantiate(summoningCircle);
        westCircle = working.GetComponent<SummoningCircle>();
        westCircle.startingX = 5;
        westCircle.startingY = 10;
        working = Instantiate(summoningCircle);
        eastCircle = working.GetComponent<SummoningCircle>();
        eastCircle.startingX = 32;
        eastCircle.startingY = 9;
    }

    // Update is called once per frame
    void Update()
    {
        if (ownTime.currentTurn<TurnStorage.currentTurn)
        {
            while (summoningRotation>3)
            {
                summoningRotation -= 4;
            }
            summoningCooldown--;
            if (summoningCooldown < 0)
            {

            switch(summoningPattern)
            {
                    case 0:
                        successfulSummon1 = Summon(0, summoningRotation);
                    break;
                    case 1:
                    if (failedSummon)
                    {
                        if (!successfulSummon1)
                        {
                            successfulSummon1 = Summon(1, summoningRotation);
                        }
                        else if (!successfulSummon2)
                        {
                            successfulSummon2 = Summon(1, (summoningRotation + 2) % 4);
                        }
                    }
                    else
                    {
                        successfulSummon1 = Summon(1, summoningRotation);
                        successfulSummon2 = Summon(1, (summoningRotation + 2) % 4);
                    }
                        break;
                    case 2:
                    successfulSummon1 = Summon(2, summoningRotation);
                        break;
                    case 3:
                    successfulSummon1 = (Summon(3, summoningRotation));
                        break;
                    case 4:
                        successfulSummon1 = Summon(0, summoningRotation);
                        if (successfulSummon1)
                        {
                        summoningRotation++;
                        }
                    break;
                    case 5:
                    if (failedSummon)
                    {
                        if (!successfulSummon1)
                        {
                            successfulSummon1 = Summon(1, summoningRotation);
                        }
                        else if (!successfulSummon2)
                        {
                            successfulSummon2 = Summon(1, (summoningRotation + 2) % 4);
                        }
                    }
                    else
                    {
                        successfulSummon1 = Summon(1, summoningRotation);
                        successfulSummon2 = Summon(1, (summoningRotation + 2) % 4);
                    }
                    break;
                        case 6:
                    successfulSummon1 = Summon(2, summoningRotation);
                    break;
                    case 7:
                    successfulSummon1 = (Summon(3, summoningRotation));
                    if (successfulSummon1)
                    {
                        summoningRotation++;
                    }
                    break;
                    case 8:
                    //Intentionally Empty
                        break;
                    case 9:
                    if (failedSummon)
                    {
                        if (!successfulSummon1)
                        {
                            successfulSummon1 = Summon(1, summoningRotation);

                        }
                        else if (!successfulSummon2)
                        {
                            successfulSummon2 = Summon(1, (summoningRotation + 1) % 4);

                        }
                        else if (successfulSummon3)
                        {
                            successfulSummon3 = Summon(1, summoningRotation + 2 % 4);

                        }
                        else if (successfulSummon4)
                        {
                            successfulSummon4 = Summon(1, (summoningRotation + 2) % 4);

                        }
                    }
                    else
                    {
                        successfulSummon1 = Summon(1, summoningRotation);
                        successfulSummon2 = Summon(1, (summoningRotation + 1) % 4);
                        successfulSummon3 = Summon(1, summoningRotation + 2 % 4);
                        successfulSummon4 = Summon(1, (summoningRotation + 2) % 4);
                    }
                    break;
            }
            if (successfulSummon1 && successfulSummon2 && successfulSummon3 && successfulSummon4)
            {
                if (summoningCooldown <= 0)
                {
                    summoningCooldown += (2 * summoningDifficulty);
                    if (summoningDifficulty > 1 && summoningPattern == 9)
                    {
                        summoningDifficulty--;
                    }
                }
                summoningRotation++;
                summoningPattern++;
                activateCircles();
                failedSummon = false;
                    numFailed = 0;
            }
            else
            {
                    numFailed++;
                 if (numFailed > 4)
                    {
                        if (summoningCooldown <= 0)
                        {
                            for (int i=0;i<3;i++)
                            {
                                if (summoningDifficulty > 1)
                                {
                                    summoningDifficulty--;
                                }
                            }
                            summoningCooldown += (2 * summoningDifficulty);
                        }
                        summoningRotation++;
                        summoningPattern++;
                        activateCircles();
                        failedSummon = false;
                        numFailed = 0;
                        successfulSummon1 = true; successfulSummon2 = true; successfulSummon3 = true; successfulSummon4 = true;
                    }
                failedSummon = true;
            }
            }

            ownTime.currentTurn=TurnStorage.currentTurn;
        }
    }
    bool Summon(int summonGuy,int summonLocation) //2=Right,3=Up,1=Down,0=Left
    {
        if (summonLocation==0)
        {
            return westCircle.summonAttempt(summonGuy);
        }
        else if (summonLocation==1)
        {
            return southCircle.summonAttempt(summonGuy);
        }
        else if (summonLocation==2)
        {
            return eastCircle.summonAttempt(summonGuy);
        }
        else if(summonLocation==3)
        {
            return northCircle.summonAttempt(summonGuy);
        }
        return false;
    }
    void activateCircles() //2=Right,3=Up,1=Down,0=Left
    {
        northCircle.active = false;
        westCircle.active = false;
        southCircle.active = false;
        eastCircle.active = false;
        if (summoningPattern == 8)
        {
            northCircle.active = true;
            southCircle.active = true;
            eastCircle.active = true;
            westCircle.active = true;
        }
        else
        {
            if (summoningRotation == 0)
            {
                westCircle.active = true;
            }
            else if (summoningRotation == 1)
            {
                southCircle.active = true;

            }
            else if (summoningRotation == 2)
            {
                eastCircle.active = true;
            }
            else if (summoningRotation == 3)
            {

        northCircle.active = true;
            }
            if (summoningPattern % 4 == 1)
            {
                if ((summoningRotation+2)%4 == 0)
                {
                    westCircle.active = true;
                }
                else if ((summoningRotation+2)%4 == 1)
                {
                    southCircle.active = true;

                }
                else if ((summoningRotation + 2) % 4 == 2)
                {
                    eastCircle.active = true;

                }
                else if ((summoningRotation + 2) % 4 == 3)
                {

                    northCircle.active = true;
                }
            }
        }
    }
}
