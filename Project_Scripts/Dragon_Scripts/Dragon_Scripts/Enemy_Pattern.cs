using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Enemy_Pattern : MonoBehaviour
{
    Dragon dragon;
    Enemy_AI enemy_ai;

    public string[] Pattern;
    public string[] PatternCycle;
    List<string> patternStorage;
   
    private int currentPattern = 0;
    private float patternCount = 0f;
    private int patternIndex;
    private void Awake()
    {
        dragon = this.transform.GetComponent<Dragon>();
        enemy_ai = dragon.GetComponent<Enemy_AI>();
        ListArrangeMent(dragon);
    }
    private List<string> ParseCommands(string str)
    {
        List<string> list = new List<string>();
        string[] splits = str.Split(',');
        foreach (var split in splits)
            list.Add(split);
        return list;
    }

    public void ListArrangeMent(Dragon dragon)
    {
        List<string> command = ParseCommands(PatternCycle[0]);
        patternCount = command.Count;
        patternStorage = command;
    }
    public void NextState(Dragon dragon)
    {
        if (Pattern.Length == 0)
        {
            return;
        }
        else if (currentPattern > patternCount - 1)//{
        {
            currentPattern = 0;
        }
           
        List<string> currentpattern = ParseCommands(Pattern[Convert.ToInt32(patternStorage[currentPattern])]);
        StartCoroutine(NextStateCoroutine(dragon, currentpattern));
        patternIndex += 1;
    }
    IEnumerator NextStateCoroutine(Dragon dragon, List<string> currentpattern)
    {
        SetState(dragon, currentpattern[patternIndex]);

        if (patternIndex >= currentpattern.Count - 1)// 1     2
        {
            yield return new WaitForSeconds(0.1f);       
            currentPattern += 1;
            patternIndex = 0;
        }      
    }
    void SetState(Dragon boss, string command)
    {
        switch (command)
        {
            case "Bite":
                dragon.ChangeState(Dragon.EnemyState.ATTACK_BITE);
                break;
            case "Leg":
                dragon.ChangeState(Dragon.EnemyState.ATTACK_LEG);
                break;
            case "Rush":
                dragon.ChangeState(Dragon.EnemyState.ATTACK_RUSH);
                break;
            case "Brt":
                dragon.ChangeState(Dragon.EnemyState.ATTACK_BREATH);
                break;
            case "Takeoff":
                dragon.ChangeState(Dragon.EnemyState.FLY_TAKEOFF);
                break;
            case "RunTakeoff":
                dragon.ChangeState(Dragon.EnemyState.FLY_RUNTAKEOFF);
                break;
            case "FlyFast":
                dragon.ChangeState(Dragon.EnemyState.FLY_FAST);
                break;
            case "Flyaround":
                dragon.ChangeState(Dragon.EnemyState.FLY_AROUND);
                break;
            case "Land":
                dragon.ChangeState(Dragon.EnemyState.FLY_LAND);
                break;
            case "FlyBrt":
                dragon.ChangeState(Dragon.EnemyState.FLY_BREATHATK);
                break;
            case "FlyBack":
                dragon.ChangeState(Dragon.EnemyState.FLY_BACKWARD);
                break;
            case "Runland":
                dragon.ChangeState(Dragon.EnemyState.FLY_RUNLAND);
                break;
        }
    }
}

