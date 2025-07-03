using UnityEngine;

public class Breakable : MonoBehaviour
{
    [SerializeField] GameObject hitEffectPrefab;
    int toughness = 150;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Weapon weapon))
        {
            this.toughness -= weapon.Dmg;
            if (hitEffectPrefab != null)
                Instantiate(hitEffectPrefab, collision.contacts[0].point, Quaternion.identity);
            Broken();
        }

    }
    void Broken()
    {
        if (this.toughness <= 0)
        {
            Destroy(gameObject);
        }
    }

}
