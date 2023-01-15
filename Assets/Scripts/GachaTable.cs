using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu( menuName = "Create GachaTable", fileName = "GachaTableEntity" )]
public class GachaTable : ScriptableObject
{
    public new string name; // レアリティの名称（Normal 、Rareなど）
    public int probability; // 当たる確率
    public new List<string> cards; // 当たるアイテムのリスト
}