using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseService: IAwake, IUpdate, IFixedUpdate, ILateUpdate
{
    public abstract void OnAwake();
    public abstract void OnUpdate();
    public abstract void OnFixedUpdate();
    public abstract void OnLateUpdate();
}
