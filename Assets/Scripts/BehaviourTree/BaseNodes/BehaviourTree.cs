using System.Collections.Generic;
using Tzaik.Enemy;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BehaviourTree
{
    [CreateAssetMenu]
    public class BehaviourTree : ScriptableObject
    {
        public Node RootNode;
        public NodeState TreeState = NodeState.Running;
        public List<Node> Nodes = new List<Node>();
        public Blackboard blackboard = new Blackboard();

        public NodeState Update() => 
            RootNode.state == NodeState.Running ? TreeState = RootNode.Update() : TreeState = NodeState.Failure;

        public BehaviourTree Clone()
        {
            BehaviourTree tree = Instantiate(this);
            tree.RootNode = RootNode.Clone();
            tree.Nodes = new List<Node>();
            Traverse(tree.RootNode, (n) =>
            {
                tree.Nodes.Add(n);
            });
            return tree;
        }

        public void Traverse(Node node, System.Action<Node> visiter)
        {
            if(node)
            {
                visiter.Invoke(node);
                var children = GetChildren(node);
                children.ForEach((n) => Traverse(n, visiter));
            }
        }

        public void Bind(Tzaik.Enemy.Blackboard blackboard)
        {
            this.blackboard = blackboard;
            Traverse(RootNode, node =>
            {
                node.blackboard = blackboard; 
            });
        }
        public List<Node> GetChildren(Node parent)
        {
            DecoratorNode decorator = parent as DecoratorNode;
            if (decorator && decorator.childNode != null)
                return new List<Node> { decorator.childNode };

            CompositeNode compositeNode = parent as CompositeNode;
            if (compositeNode && compositeNode.childNodes != null)
                return compositeNode.childNodes;

            RootNode rootNode = parent as RootNode;
            if (rootNode && rootNode.childNode != null)
                return new List<Node> { rootNode.childNode };

            return new List<Node>();
        }
        #region Behaviour Tree Editor methods 
#if UNITY_EDITOR
        public Node CreateNode(System.Type type)
        {
            var node = ScriptableObject.CreateInstance(type) as Node;
            node.name = type.Name;
            node.guid = GUID.Generate().ToString();
            node.position = Mouse.current.position.ReadValue();
            Nodes.Add(node);

            if(!Application.isPlaying) 
                AssetDatabase.AddObjectToAsset(node, this);  

            Undo.RegisterCreatedObjectUndo(node, "Behaviour Tree(CreateNode)");
            AssetDatabase.SaveAssets();
            return node;

        }
        public void AddChild(Node parent, Node child)
        {
            DecoratorNode decorator = parent as DecoratorNode;
            if (decorator && decorator.childNode != child)
            {
                Undo.RecordObject(decorator, "Behaviour Tree(AddChild)");
                decorator.childNode = child;
                EditorUtility.SetDirty(decorator);
            }

            CompositeNode composite = parent as CompositeNode;
            if (composite && !composite.childNodes.Contains(child))
            {
                Undo.RecordObject(composite, "Behaviour Tree(AddChild)");
                composite.childNodes.Add(child);
                EditorUtility.SetDirty(composite);
            }

            RootNode root = parent as RootNode;
            if (root && root.childNode != child)
            {
                Undo.RecordObject(root, "Behaviour Tree(AddChild)");
                root.childNode = child;
                EditorUtility.SetDirty(root);
            }

        } 


        public void RemoveChild(Node parent, Node child)
        {
            DecoratorNode decorator = parent as DecoratorNode;
            if (decorator)
            {
                Undo.RecordObject(decorator, "Behaviour Tree(RemoveChild)"); 
                decorator.childNode = null;
                EditorUtility.SetDirty(decorator);
            }

            CompositeNode composite = parent as CompositeNode;
            if (composite)
            {
                Undo.RecordObject(composite, "Behaviour Tree(RemoveChild)");
                composite.childNodes.Remove(child);
                EditorUtility.SetDirty(composite);
            }

            RootNode root = parent as RootNode;
            if (root && root.childNode != child)
            {
                Undo.RecordObject(root, "Behaviour Tree(RemoveChild)");
                root.childNode = null;
                EditorUtility.SetDirty(root);
            }
        } 
     
        public void DeleteNode(Node node)
        {
            Undo.RecordObject(this, "Behaviour Tree(DeleteNode)"); 
            Nodes.Remove(node);  
            Undo.DestroyObjectImmediate(node);
            AssetDatabase.SaveAssets();
        }
#endif

        #endregion
    }


}
