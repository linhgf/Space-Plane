using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="EnemyFactory", menuName="Factory/EnemyFactory")]
public class EnemyFactorySO : FactorySO<EnemyController>
{
    List<string> word = new List<string>{ "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
    public EnemyController[] prefabs;
    public override EnemyController Create()
    {
        int index = Random.Range(0, prefabs.Length);
        EnemyController ins = Instantiate(prefabs[index]);
        index = Random.Range(0, 25);
        var text = ins.GetComponentInChildren<TextMesh>().text = word[index];
        return ins;
    }
}
