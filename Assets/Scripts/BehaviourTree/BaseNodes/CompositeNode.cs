using UnityEngine;
using System.Collections.Generic;

namespace BehaviourTree
{
    public abstract class CompositeNode : Node
    {
        [HideInInspector] public List<Node> childNodes = new List<Node>();
         
        public override Node Clone()
        {
            CompositeNode node = Instantiate(this);
            node.childNodes = childNodes.ConvertAll(c => c.Clone());
            return node;
        }
    }
     
}
