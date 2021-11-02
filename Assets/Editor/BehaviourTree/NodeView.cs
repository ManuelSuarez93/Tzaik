using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using UnityEditor;

namespace BehaviourTree
{

    public class NodeView : UnityEditor.Experimental.GraphView.Node
    {
        public Action<NodeView> OnNodeSelected;
        public Node node;
        public Port input;
        public Port output;
        public NodeView(Node node) : base("Assets/Editor/BehaviourTree/NodeView.uxml")
        {
            this.node = node;
            title = node.nodeName; 
            viewDataKey = node.guid;

            style.left = node.position.x;
            style.top = node.position.y;

            CreateInputPort();
            CreateOutputPort();
            SetUpClasses();
        }

        private void SetUpClasses()
        {
            if (node is ActionNode)
                AddToClassList("action");
            else if (node is SequenceNode)
                AddToClassList("sequence");
            else if (node is SelectorNode)
                AddToClassList("selector");
            else if (node is DecoratorNode)
                AddToClassList("decorator");
            else if (node is RootNode)
                AddToClassList("root");
        }

        private void CreateInputPort()
        {
            if (node is ActionNode) 
                input = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool)); 
            else if (node is CompositeNode) 
                input = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool)); 
            else if (node is DecoratorNode) 
                input = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));  
            else if (node is RootNode)
                { }
            if (input != null)
            {
                input.portName = "";
                
                input.style.flexDirection = FlexDirection.Column;
                inputContainer.Add(input);
            }
        }
        private void CreateOutputPort()
        {
            if (node is ActionNode) { }
            else if (node is CompositeNode) 
                output = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Multi, typeof(bool)); 
            else if (node is DecoratorNode)
                output = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Single, typeof(bool));
            else if (node is RootNode)
                output = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Single, typeof(bool));

            if (output != null)
            {
                output.portName = "";
                output.style.flexDirection = FlexDirection.ColumnReverse;
                outputContainer.Add(output);
            }
        }


        public override void SetPosition(Rect newPos)
        {
            base.SetPosition(newPos);
            Undo.RecordObject(node, "Behaviour Tree (SetPosition)");
            node.position = new Vector2(newPos.xMin, newPos.yMin);
            EditorUtility.SetDirty(node); 
        }

        public override void OnSelected()
        {
            base.OnSelected();
            if (OnNodeSelected != null)
                OnNodeSelected.Invoke(this);
        }

        public void SortChildren()
        {
            CompositeNode composite = node as CompositeNode;
            if (composite)
                composite.childNodes.Sort(SortByHorizontalPosition);
        }

        private int SortByHorizontalPosition(Node left, Node right)
            =>  left.position.x < right.position.x ? -1 : 1; 

        public void UpdateState()
        {
            RemoveFromClassList("running");
            RemoveFromClassList("success");
            RemoveFromClassList("failure");
            if (Application.isPlaying)
                switch(node.state)
                {
                    case NodeState.Running:
                        if(node.started)
                            AddToClassList("running");
                        break;
                    case NodeState.Success:
                        AddToClassList("success");
                        break;
                    case NodeState.Failure:
                        AddToClassList("failure");
                        break;
                }
        }
    }
     
}
