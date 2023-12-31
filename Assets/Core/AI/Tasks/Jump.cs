﻿using BehaviorDesigner.Runtime.Tasks;
using Core.Character;
using Core.Util;
using DG.Tweening;
using UnityEngine;

namespace Core.AI
{
    public class Jump : EnemyAction
    {
        public float horizontalForce = 5.0f;
        public float jumpForce = 10.0f;

        public float buildupTime;
        public float jumpTime;

        public string buildupAnimation;
        public string mainAnimation;
        
        public bool shakeCameraOnLanding;

        public SpriteRenderer jumpEffect;
        public Vector3 effectOffset;

        private bool hasLanded;

        private Tween buildupTween;
        private Tween jumpTween;
        
        public override void OnStart()
        {
            buildupTween = DOVirtual.DelayedCall(buildupTime, StartJump, false);
            animator.SetTrigger(buildupAnimation);
        }

        private void StartJump()
        {
            if(!string.IsNullOrEmpty(mainAnimation))
                animator.SetTrigger(mainAnimation);
            
            var direction = player.transform.position.x < transform.position.x ? -1 : 1;
            body.AddForce(new Vector2(horizontalForce * direction, jumpForce), ForceMode2D.Impulse);
            
            if(jumpEffect != null)
                EffectManager.Instance.PlaySpriteOneShot(jumpEffect,transform.position + effectOffset,direction>0);

            jumpTween = DOVirtual.DelayedCall(jumpTime, () =>
            {
                hasLanded = true;
                body.velocity = Vector2.zero;
                if (shakeCameraOnLanding)
                    CameraController.Instance.ShakeCamera(0.5f);
            }, false);
        }

        public override TaskStatus OnUpdate()
        {
            return hasLanded ? TaskStatus.Success : TaskStatus.Running;
        }

        public override void OnEnd()
        {
            buildupTween?.Kill();
            jumpTween?.Kill();
            hasLanded = false;
        }
    }
}