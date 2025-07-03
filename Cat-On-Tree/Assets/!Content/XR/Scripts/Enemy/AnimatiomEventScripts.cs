using UnityEngine;


public class AnimationEventSripts : MonoBehaviour
{
    [SerializeField] public Animator animator1;
    [SerializeField] EnemyStateManager enemyStateManager;
    [SerializeField] AttackState attackState;
    [SerializeField] Collider damager1;
    [SerializeField] Collider damager2;
    //[SerializeField] WalkState walkState;



    private void Awake()
    {
        enemyStateManager = this.GetComponent<EnemyStateManager>();
    }
    /*
    public void CheckPlayerDistance()
    {
        if (enemyStateManager.currentState == attackState)
        {
            if (enemyStateManager.CheckOnTarget() > enemyStateManager.attackDistance)
            {
                enemyStateManager.SwitchState(enemyStateManager.walkState);
                //enemyManager.animator.SetBool("is_Attacking", false);
            }
        }
    }
    */

    public void OnOffDamager1(int onoff)
    {
        if (onoff == 2)
        {
            damager1.enabled = false;
            //Debug.Log("000");
        }
        else if (onoff == 1) 
        {
            damager1.enabled = true;
            //Debug.Log("111");
        }
    }

    public void OnOffDamager2(int onoff)
    {
        if (onoff == 2)
        {
            damager2.enabled = false;
            //Debug.Log("222");
        }
        else if (onoff == 1)
        {
            damager2.enabled = true;
            //Debug.Log("333");
        }
    }

    
    

}