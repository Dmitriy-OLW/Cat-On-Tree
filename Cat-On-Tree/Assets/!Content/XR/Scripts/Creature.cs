using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class Creature : MonoBehaviour
{
    int enemyhp = 100;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Weapon weapon))
        {
            this.enemyhp -= weapon.Dmg;
            Debug.Log($"HP: {enemyhp}");
            Dead();
        }

        void Dead()
        {
            if (this.enemyhp <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

}
