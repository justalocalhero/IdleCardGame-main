using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnTriggerFire<T>(PlayPackage playPackage, T card);

public interface ITrigger : IDescription
{
    TriggerTag triggerTag { get; }
}

public interface ITrigger<T> : ITrigger
{
    event OnTriggerFire<T> onTriggerFire;
    void Register(T t);
    void Remove(T t);
}