using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionData : MonoBehaviour
{
    public int maxAmountEnemys = 2; //4 is max
    public string BattleScene;
    public List<GameObject> possibleEnemys = new List<GameObject>();
}
