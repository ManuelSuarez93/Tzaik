using UnityEngine;

namespace Tzaik.Items.Weapons
{
    [System.Serializable]
    public class WeaponAttack
    {  
        [SerializeField] Projectile projectile;
        [SerializeField] Projectile specialProjectile;
        [SerializeField] Transform shootOrigin;
        [SerializeField] float baseDamage;
        [SerializeField] float baseSpeed;
        [SerializeField] float baseForce;
        [SerializeField] int projectileAmount = 1;
        [Header("Random projectile trajectory")]
        [SerializeField] bool randomTrajectory = false;
        [SerializeField] float sphereSize;
        float additionalDamage;
        float additionalSpeed;
        float additionalForce;
        
        public Transform ShootOrigin => shootOrigin; 
        public float AdditionalDamage { get => additionalDamage; set => additionalDamage = value; }
        public float AdditionalSpeed { get => additionalSpeed; set => additionalSpeed = value; }
        public float AdditionalForce { get => additionalForce; set => additionalForce = value; }
        public float BaseDamage { get => baseDamage; set => baseDamage = value; }
        public float BaseSpeed { get => baseSpeed; set => baseSpeed = value; }
        public float BaseForce { get => baseForce; set => baseForce = value; }
        public Projectile Projectile { get => projectile; set => projectile = value; }
        public Projectile SpecialProjectile { get => specialProjectile; set => specialProjectile = value; }

        public void InstantiateProjectile(Projectile projectile)
        {
            for(int i = 0; i < projectileAmount; i++)
            { 
                GameObject o = GameObject.Instantiate(projectile.gameObject, shootOrigin);
                o.transform.parent = null;
                o.GetComponent<Projectile>().Damage = baseDamage + additionalDamage;
                o.GetComponent<Projectile>().Speed = baseSpeed + additionalSpeed;
                o.GetComponent<Projectile>().ForceImpactAmount = baseForce + additionalForce;
                if(randomTrajectory)
                { 
                    var rand = Random.insideUnitCircle * sphereSize;
                    o.transform.localEulerAngles = new Vector3(rand.x + o.transform.localEulerAngles.x,
                        rand.y + o.transform.localEulerAngles.y);
                }
            }
        } 
        

    }
}
