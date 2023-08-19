using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using Core.AI;
using UnityEngine;


namespace Core.AI
{
    public class Patrol : EnemyAction
    {
        public float moveSpeed = 2f;
        public float minDistance = 8f;
        public string runAnimation = "isRunning";
        private float direction;
    
        public override void OnStart()
        {
            animator.SetBool(runAnimation,true);
            var diff = transform.position.x - player.transform.position.x;
            var playerDirection = Mathf.Sign(diff);
            
            direction = Mathf.Abs(diff)<minDistance? playerDirection : -playerDirection;
        }

        public override TaskStatus OnUpdate()
        {
            body.velocity = Vector2.right * moveSpeed * direction;
            var scale = transform.localScale;
            scale.x = direction;
            transform.localScale = scale;
            return TaskStatus.Running;
        }

        public override void OnEnd()
        {
            animator.SetBool(runAnimation,false);
        }
    }
}