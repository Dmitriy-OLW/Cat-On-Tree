using UnityEngine;

public class IdleState : AState
{
    public override void EnterState(EnemyStateManager enemyManager) 
    {
        // начало анимации idle
        //Debug.Log("Idle Enetered");
        enemyManager.SetSpeed(0);
        enemyManager.animator.SetBool("is_Attacking", false);
        enemyManager.animator.SetBool("is_Angry", false);

    }
    public override void ExitState(EnemyStateManager enemyManager) 
    {
        //Debug.Log("Idle Exit");
    }
    public override void UpdateState(EnemyStateManager enemyManager) 
    {
        if (enemyManager.CheckOnTarget() < enemyManager.agroDistance) 
        {
            enemyManager.SwitchState(enemyManager.walkState);
        }
    }
}
