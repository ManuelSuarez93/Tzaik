using UnityEngine;

namespace Tzaik.General
{
    public abstract class Spawnable : MonoBehaviour
    {  
        public delegate void DeactivateEvent(Spawnable o);
        public DeactivateEvent Deactivate { get; set; } 

        [Header("Spawnable Object properties")]
        [Tooltip("ID related to Prefab Pool type")]
        [SerializeField] protected int typeId; 
        public int TypeId => typeId;
        public virtual void Initialization() { }
        public virtual void Deactivation()
        {
            gameObject.SetActive(false);
            Deactivate.Invoke(this);
        }

    }
  
}
