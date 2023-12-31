﻿using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Core.AI
{
    public class TurnAround : EnemyAction
    {
        public override TaskStatus OnUpdate()
        {
            var scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;

            return TaskStatus.Success;
        }
    }
}