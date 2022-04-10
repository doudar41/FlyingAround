
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "Score", menuName = "Data")]
public class ScoreBaseScriptable : ScriptableObject
{
    
    public int bestScore;
    public Dictionary<int, int> scoreList = new Dictionary<int , int>();
}
