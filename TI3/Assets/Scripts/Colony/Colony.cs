using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Colony
{
    public string colonyName = "Colony T-900";

    public string[] associatedColonyArray;

    public int taskLevel = 1;
    public int taskMaxAmount = 1;
    public int taskMinAmount = 1;

    public int contentMaxAmountPerTask = 1;
    public int contentMinAmountPerTask = 1;

    public int colonyMaxPayment = 20;
    public int colonyMinPayment = 10;

    //public int workerEngineerLevel = 1;
    //public int workerEngineerMaxAmount = 0;
    //public int workerEngineerMinAmount = 0;
    //public int workerEngineerHirePrice = 999;
    //public int workerResearcherLevel = 1;
    //public int workerResearcherMaxAmount = 0;
    //public int workerResearcherMinAmount = 0;
    //public int workerResearcherHirePrice = 999;

    //public int shopLevel = 1;
    //public float shopBuyPricePercent = 2;
    //public float shopSellPricePercent = 0.5f;

    //public int exploreLevel = 1;



    // Construtor
    public Colony
        (string colonyName,
        int taskLevel,
        int taskMaxAmount,
        int taskMinAmount/*
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
        int exploreLevel*/)
    {
        this.colonyName = colonyName;
        this.taskLevel = taskLevel;
        this.taskMaxAmount = taskMaxAmount;
        this.taskMinAmount = taskMinAmount;
        //this.workerEngineerLevel = workerEngineerLevel;
        //this.workerEngineerMaxAmount = workerEngineerMaxAmount;
        //this.workerEngineerMinAmount = workerEngineerMinAmount;
        //this.workerEngineerHirePrice = workerEngineerHirePrice;
        //this.workerResearcherLevel = workerResearcherLevel;
        //this.workerResearcherMaxAmount = workerResearcherMaxAmount;
        //this.workerResearcherMinAmount = workerResearcherMinAmount;
        //this.workerResearcherHirePrice = workerResearcherHirePrice;
        //this.shopLevel = shopLevel;
        //this.shopBuyPricePercent = shopBuyPricePercent;
        //this.shopSellPricePercent = shopSellPricePercent;
        //this.exploreLevel = exploreLevel;
    }
}

