using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;


namespace Core.AI
{
    public class CheckForPeriodicEvent : EnemyConditional
    {
        public float interval = 1f;
        public SharedFloat PeriodicTimer;
        public override TaskStatus OnUpdate()
        {
            PeriodicTimer.Value += Time.deltaTime;
            if(PeriodicTimer.Value >= interval)
            {
                return TaskStatus.Success;
            }
            return TaskStatus.Failure;
        }
    }
}