using UnityEngine;

namespace Tzaik.Enemy
{
    public class DopplegangerAttack : EnemyAttack
    {

        [SerializeField] GameObject macahuitl;
        [SerializeField] GameObject blowgun;
        public void EnableMacahuitl(int enable) => macahuitl.SetActive(enable == 0);
        public void EnableBlowgun(int enable) => blowgun.SetActive(enable == 0);
    }
}
