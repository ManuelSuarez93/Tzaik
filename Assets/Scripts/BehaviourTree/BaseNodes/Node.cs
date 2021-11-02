using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tzaik.Enemy;
using UnityEngine;

namespace BehaviourTree
{
    public enum NodeState
    {
        Running, Failure, Success
    }
    public abstract class Node: ScriptableObject
    {
        public NodeState state = NodeState.Running;
        [HideInInspector] public bool started = false;
        [HideInInspector] public string guid;
        [HideInInspector] public Vector2 position;
        [HideInInspector] public Blackboard blackboard;
        [HideInInspector] public bool isStunned;

        public virtual string nodeName { get; set; }
        public NodeState Update()
        {
            if (!started)
            {
                OnStart();
                started = true;
            }

            state = OnUpdate();

            if(state == NodeState.Failure || state == NodeState.Success)
            {
                OnStop(); 
                started = false;
            }

            return state;
        }
         
        protected abstract void OnStart();
        protected abstract void OnStop();
        protected abstract NodeState OnUpdate();
        public virtual Node Clone() => Instantiate(this); 
    }
}
