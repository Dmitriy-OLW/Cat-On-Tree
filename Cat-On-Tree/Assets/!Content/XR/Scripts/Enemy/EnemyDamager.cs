using UnityEngine;

public class EnemyDamager : MonoBehaviour
{
    public int enemyDmg = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerHP>(out PlayerHP playerHP))
        {
            GetComponent<Collider>().enabled = false;
        }
    }
}
