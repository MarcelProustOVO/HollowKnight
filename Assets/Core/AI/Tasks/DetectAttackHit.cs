using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Core.AI
{
    public class DetectAttackHit : EnemyConditional
    {
        private bool isGettingHit;
        public bool waitForHit;

        public override void OnAwake()
        {
            base.OnAwake();
            destructable.OnHit += OnHit;
        }

        public override void OnStart()
        {
            base.OnStart();
            if(waitForHit)
                isGettingHit = false;
        }

        void OnHit(Vector2 position, Vector2 force)
        {
            isGettingHit = true;
        }

        public override TaskStatus OnUpdate()
        {
            var returnTypeNegative = waitForHit ? TaskStatus.Running : TaskStatus.Failure;
            return isGettingHit ? TaskStatus.Success : returnTypeNegative;
        }

        public override void OnEnd()
        {
            isGettingHit = false;
        }

    }
}
