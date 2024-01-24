using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    
    public abstract void Move(Vector3 targetPos);
    
    protected abstract void Stop();

   

    public abstract void Initialize(Players playerColor);

    

}
