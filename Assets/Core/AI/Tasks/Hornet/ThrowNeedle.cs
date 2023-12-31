using BehaviorDesigner.Runtime.Tasks;
using Core.Character;
using Core.Util;
using DG.Tweening;
using UnityEngine;

namespace Core.AI
{
    public class ThrowNeedle : EnemyAction
    {
        public float throwDistance = 20f;
        public float buildupTime;
        public float throwTime;
        public float retractTime;

        public string buildupAnimation;
        public string mainAnimation;

        public GameObject needlePrefab;
        public Vector3 needleOffset;
        
        public SpriteRenderer throwEffect;
        public SpriteRenderer retractEffect;

        private bool hasLanded;
        private GameObject needleObject;

        private Tween buildupTween;
        private Tween throwTween;
        
        
        public override void OnStart()
        {
            buildupTween = DOVirtual.DelayedCall(buildupTime, StartThrow, false);
            animator.SetTrigger(buildupAnimation);
        }
        
        private void StartThrow()
        {
            animator.SetBool(mainAnimation,true);
            CameraController.Instance.ShakeCamera(0.2f);
            var direction = transform.localScale.x;
            EffectManager.Instance.PlaySpriteOneShot(throwEffect,transform.position+needleOffset,direction>0);
            needleObject = Object.Instantiate(needlePrefab,transform.position + needleOffset,Quaternion.identity);
            needleObject.transform.localScale = new Vector3(direction,1,1);

            throwTween = DOTween.Sequence()
                .Append(needleObject.transform.DOMoveX(transform.position.x + throwDistance * direction, throwTime)
                    .SetEase(Ease.OutCubic))
                .AppendCallback(() =>
                {
                    EffectManager.Instance.PlaySpriteOneShot(retractEffect,transform.position+needleOffset,direction>0); 
                })
                .Append(needleObject.transform.DOMoveX(transform.position.x, retractTime).SetEase(Ease.InQuad))
                .AppendCallback(() =>
                {
                    hasLanded = true;
                    animator.SetBool(mainAnimation, false);
                    Object.Destroy(needleObject);
                });
        }

        public override TaskStatus OnUpdate()
        {
            return hasLanded ? TaskStatus.Success : TaskStatus.Running;
        }

        public override void OnEnd()
        {
            buildupTween?.Kill();
            throwTween?.Kill();
            hasLanded = false;
            Object.Destroy(needleObject);
        }
    }
}