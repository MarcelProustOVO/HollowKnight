using BehaviorDesigner.Runtime.Tasks;
using Core.Bosses;
using Core.UI;
using UnityEngine;

namespace Core.AI
{
    public class InitBoss : EnemyAction
    {
        public override TaskStatus OnUpdate()
        {
            GuiManager.Instance.ShowBossName(GetComponent<BossConfig>().bossName);
            return TaskStatus.Success;
        }
    }
}