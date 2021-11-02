using System.Collections;
using Tzaik.General;
using Tzaik.Items;
using Tzaik.Items.Weapons;
using Tzaik.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Tzaik
{
    public class Sling : Weapon
    {

        [Header("Sling properties")]
        [SerializeField] float additionalSpeed;
        [SerializeField] float additionalDamage;
        [SerializeField] float maxHoldShot;
        [SerializeField] Image holdSlingImage; 


        bool isPreparing;
        float currentHold;
        float currentSpecialTime;
         
        void HoldSling() 
            => currentHold = currentHold < maxHoldShot ? currentHold += Time.deltaTime : currentHold = maxHoldShot;
        void ResetCurrentHold()
        {
            currentHold = 0;
            attack.AdditionalDamage = currentHold;
            attack.AdditionalSpeed = currentHold;
        }

        float HoldPercentage => currentHold / maxHoldShot;
        void SetAdditionalDamage() 
            => attack.AdditionalDamage = additionalDamage * HoldPercentage;
        void SetAdditionalSpeed()
            => attack.AdditionalSpeed = additionalSpeed * HoldPercentage; 
        
        public override void PerformAttack()
        {
            if(AttackConidition && !isPreparing)
            {
                isPreparing = true;
                animator.SetBool("IsPreparing", true);
                StartCoroutine(StartHolding());
            }
        } 

        public override void ReleaseShot()
        {
            if (AttackConidition && isPreparing)
            {
                ProjectileSet(); 
                ResetCurrentHold(); 
                isPreparing = false; 
                animator.SetBool("IsPreparing", false);
            } 
        }

        private void ProjectileSet()
        {
            SetAdditionalDamage();
            SetAdditionalSpeed();
        }
       
        IEnumerator StartHolding()
        {
            while(isPreparing)
            {  
                HoldSling(); 
                yield return null;
            }
        }
         
    }
}
