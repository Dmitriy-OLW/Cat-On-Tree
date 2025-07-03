using UnityEngine;

public class Is_hit : MonoBehaviour
{
    [SerializeField] GameObject Capsule;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Weapon weapon))
        {
            Capsule.transform.position += new Vector3(0.3f, 0, 0);
            weapon.gameObject.SetActive(false);
        }
        // Capsule.transform.position += new Vector3(0.3f, 0, 0);
        // Capsule.transform.position += new Vector3(-0.3f, 0, 0);

    }
}
