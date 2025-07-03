using UnityEngine;

public abstract class AState
{
    public abstract void EnterState(EnemyStateManager enemyStateManager);
    public abstract void ExitState(EnemyStateManager enemyStateManager);

    public abstract void UpdateState(EnemyStateManager enemyStateManager);
}
