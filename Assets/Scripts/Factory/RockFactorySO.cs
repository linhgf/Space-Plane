using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="RockFactory", menuName="Factory/RockFactory")]
public class RockFactorySO : FactorySO<Rock>
{
    public List<Rock> prefabs;
    public override Rock Create(){
        int rand = Random.Range(0, prefabs.Count);
        Rock ins = Instantiate(prefabs[rand]);
        return ins;
    }
}
