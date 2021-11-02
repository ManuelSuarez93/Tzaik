using System.Collections.Generic;
using UnityEngine;

namespace Tzaik.General
{
    public class PrefabPool : MonoBehaviour
    {
        #region Fields
        [SerializeField] List<Spawnable> instances;
        [SerializeField] List<Spawnable> availablePool;
        [SerializeField] PrefabPoolType type = PrefabPoolType.Enemies;
        #endregion

        #region Properties
        public bool HasAvailableObjects => availablePool.Count > 0;
        public PrefabPoolType Type => type;
        #endregion

        #region Unity Methods
        private void Awake()
        {
            availablePool = new List<Spawnable>();
            if(instances != null)
            {
                foreach (Spawnable p in instances)
                {
                    p.GetComponent<Spawnable>().Deactivate += DeactivateObject;
                    availablePool.Add(p);
                    p.gameObject.SetActive(false);
                }
            }
           
        }


        #endregion

        #region Methods
        public Spawnable InstanceSpecificObject(Vector3 position,int id)
        {
            int i = ContainsPrefabID(id);
            if (i == -1 || availablePool.Count <= 0) return null;

            Spawnable sp = availablePool[i];
            sp.gameObject.SetActive(true);
            sp.transform.parent = null;
            sp.transform.position = position;
            availablePool.Remove(sp);
            sp.Initialization();

            return sp;
        }
        public Spawnable InstanceObject(Vector3 position)
        {
            if (availablePool.Count <= 0) return null;

            Spawnable sp = availablePool[Random.Range(0,availablePool.Count)];
            sp.gameObject.SetActive(true);
            sp.transform.parent = null;
            sp.transform.position = position;
            availablePool.Remove(sp);
            sp.Initialization();

            return sp;
        }
        int ContainsPrefabID(int i)
        {
            for(int j = 0; j < availablePool.Count; j ++)
            {
                if (availablePool[j].TypeId == i) return j;
            }
            return -1;
        }
        void DeactivateObject(Spawnable o)
        {
            if (availablePool.Contains(o)) return;
            o.gameObject.SetActive(false);
            availablePool.Add(o);
            o.transform.parent = gameObject.transform;
        }
        #endregion

        public enum PrefabPoolType
        {
            Items = 0, Weapons = 1, Enemies = 2, Decorations = 3, Coins = 4
        }
    }

}
