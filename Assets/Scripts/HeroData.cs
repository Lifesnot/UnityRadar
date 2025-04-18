using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "heroData", menuName = "Hero/HeroData")]
public class HeroData : ScriptableObject
{
    [Header("选手名字")]
    public string heroName;
    [Header("分均伤害")]
    [Range(0, 2f)]
    public float averageDamage;
    [Header("伤害转换")]
    [Range(0, 2f)]
    public float damageConversion;
    [Header("伤害占比")]
    [Range(0, 2f)]
    public float damagepercentage;
    [Header("场均击杀")]
    [Range(0, 2f)]
    public float fieldAverageKill;
    [Header("生存能力")]
    [Range(0, 2f)]
    public float Viability;
    [Header("经济占比")]
    [Range(0, 2f)]
    public float economiCproportion;
    [Header("对位经济差")]
    [Range(0, 2f)]
    public float contrapositionEconomicDifference;
    [Header("承伤占比")]
    [Range(0, 2f)]
    public float proportionOfInjuries;
}