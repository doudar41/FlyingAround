using System.Collections.Specialized;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "Score", menuName = "Data")]
public class ScoreBaseScriptable : ScriptableObject
{
    public OrderedDictionary scoreList = new OrderedDictionary();
    //Dictionary<string, int> scoreList = new Dictionary<string, int>();
}
