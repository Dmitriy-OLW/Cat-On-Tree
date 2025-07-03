using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    int hp = 100;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.TryGetComponent(out EnemyDamager enemyDamager))
        {
            this.hp -= enemyDamager.enemyDmg;
            Debug.Log("8888888888888888");
            PlayerDead();
        }

        void PlayerDead()
        {
            if (this.hp <= 0)
            {
                Debug.Log("77777777777777777777777777777");
            }
        }
    }
}
