using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColonySystem : MonoBehaviour
{
    public string currentColonyName;

    public Colony[] colonyArray = {//lvl, max, min         lvl, max, min, price          lvl, max, min, price     lvl, buy, sell              lvl
        new Colony("Earth", /*Tasks: */1, 1, 1, /*Engineer: */ 1, 0, 0, 99, /*Researcher: */ 1, 0, 0, 99, /*Shop: */ 1, 2, 0.5f, /*Explore: */ 1),
        new Colony("Mars", /*Tasks: */1, 2, 1, /*Engineer: */ 1, 1, 0, 99, /*Researcher: */ 1, 0, 0, 99, /*Shop: */ 1, 2, 0.5f, /*Explore: */ 1),};

    private void OnLevelWasLoaded(int level)
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name.Contains("Colony") == true)
        {
            Debug.Log("Chegou à Colônia. -- Aqui caberia uma Action fácil -- André -- ColonySystemScript --");
        }
    }
}

public class Colony
{
    public string colonyName;

    public int taskLevel;
    public int taskMaxAmount;
    public int taskMinAmount;

    public int workerEngineerLevel;
    public int workerEngineerMaxAmount;
    public int workerEngineerMinAmount;
    public int workerEngineerHirePrice;
    public int workerResearcherLevel;
    public int workerResearcherMaxAmount;
    public int workerResearcherMinAmount;
    public int workerResearcherHirePrice;

    public int shopLevel;
    public float shopBuyPricePercent;
    public float shopSellPricePercent;

    public int exploreLevel;

    // Construtor
    public Colony
        (string colonyName,
        int taskLevel,
        int taskMaxAmount,
        int taskMinAmount,
        int workerEngineerLevel,
        int workerEngineerMaxAmount,
        int workerEngineerMinAmount,
        int workerEngineerHirePrice,
        int workerResearcherLevel,
        int workerResearcherMaxAmount,
        int workerResearcherMinAmount,
        int workerResearcherHirePrice,
        int shopLevel,
        float shopBuyPricePercent,
        float shopSellPricePercent,
        int exploreLevel)
    {
        this.colonyName = colonyName;
        this.taskLevel = taskLevel;
        this.taskMaxAmount = taskMaxAmount;
        this.taskMinAmount = taskMinAmount;
        this.workerEngineerLevel = workerEngineerLevel;
        this.workerEngineerMaxAmount = workerEngineerMaxAmount;
        this.workerEngineerMinAmount = workerEngineerMinAmount;
        this.workerEngineerHirePrice = workerEngineerHirePrice;
        this.workerResearcherLevel = workerResearcherLevel;
        this.workerResearcherMaxAmount = workerResearcherMaxAmount;
        this.workerResearcherMinAmount = workerResearcherMinAmount;
        this.workerResearcherHirePrice = workerResearcherHirePrice;
        this.shopLevel = shopLevel;
        this.shopBuyPricePercent = shopBuyPricePercent;
        this.shopSellPricePercent = shopSellPricePercent;
        this.exploreLevel = exploreLevel;
    }
}