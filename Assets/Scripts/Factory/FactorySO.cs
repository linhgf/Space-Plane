using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class FactorySO<T> : ScriptableObject, IFactory<T>
{
    public abstract T Create();
}
