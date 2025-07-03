using UnityEngine;

public class AttackState : AState
{
    

    public override void EnterState(EnemyStateManager enemyManager)
    {
        
        enemyManager.SetSpeed(0);
        enemyManager.animator.SetBool("is_Attacking", true);
        
    }
    public override void ExitState(EnemyStateManager enemyManager)
    {
       
        enemyManager.animator.SetBool("is_Attacking", false);
        
    }
    public override void UpdateState(EnemyStateManager enemyManager)
    {
        if (enemyManager.CheckOnTarget() > enemyManager.attackDistance)
        {
            enemyManager.animator.SetBool("is_Angry", true);
            enemyManager.animator.SetBool("is_Attacking", false);
            enemyManager.SwitchState(enemyManager.walkState);
            
        }
        else if (enemyManager.CheckOnTarget() <= enemyManager.attackDistance)
        {
            enemyManager.animator.SetBool("is_Angry", true);
            enemyManager.animator.SetBool("is_Attacking", true);
            enemyManager.SwitchState(enemyManager.attackState);
            
        }
    }
}



