using System.Collections.Generic;
using Tzaik.Enemy;
using UnityEngine;

namespace BehaviourTree
{
    public class BehaviourTreeRunner : MonoBehaviour
    {
        [SerializeField] BehaviourTree tree;
        public BehaviourTree Tree { get => tree; set => tree = value; }
        void Start()
        {
            tree = tree.Clone(); 
            tree.Bind(GetComponent<EnemyContext>().blackboard);
        }
        void Update()
            => tree.Update();
    }
}
