 using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView; 
using UnityEngine.UIElements;

namespace BehaviourTree
{

#if UNITY_EDITOR
    public class BehaviourTreeView : GraphView
    {
        public new class UxmlFactory : UxmlFactory<BehaviourTreeView, GraphView.UxmlTraits> { }
        BehaviourTree tree; 
        public Action<NodeView> OnNodeSelected; 

        public BehaviourTreeView() 
        {
            Insert(0, new GridBackground());

            this.AddManipulator(new ContentZoomer());
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector()); 

            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/BehaviourTree/BehaviourTreeEditor.uss");
            styleSheets.Add(styleSheet);
        }
        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            return ports.ToList().Where(endPort => endPort.direction != startPort.direction && endPort.node != startPort.node).ToList();
        }

        NodeView FindNodeView(Node node) => GetNodeByGuid(node.guid) as NodeView;
        internal void PopulateView(BehaviourTree currentTree)
        {
            tree = currentTree;

            graphViewChanged -= OnGraphViewChanged;
            DeleteElements(graphElements); 
            graphViewChanged += OnGraphViewChanged;

            if(tree.RootNode == null)
            {
                tree.RootNode = tree.CreateNode(typeof(RootNode)) as RootNode;
                EditorUtility.SetDirty(tree);
                AssetDatabase.SaveAssets();
            }

            tree.Nodes.ForEach(n => CreateNodeView(n));

            tree.Nodes.ForEach(n =>
            {
                var children = tree.GetChildren(n);
                children.ForEach(c =>
                {
                    NodeView parentView = FindNodeView(n);
                    NodeView childView = FindNodeView(c);

                    var edge = parentView.output.ConnectTo(childView.input);
                    AddElement(edge); 
                    EditorUtility.SetDirty(tree);
                    AssetDatabase.SaveAssets();
                });
            });
        } 
        private GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange)
        {
            if(graphViewChange.elementsToRemove != null) 
                graphViewChange.elementsToRemove.ForEach(elem =>
                {
                    NodeView nodeView = elem as NodeView;
                    if(nodeView != null) 
                        tree.DeleteNode(nodeView.node);

                    Edge edge = elem as Edge;
                    if(edge != null)
                    {
                        NodeView parentView = edge.output.node as NodeView;
                        NodeView childView = edge.input.node as NodeView;
                        tree.RemoveChild(parentView.node, childView.node);
                    }
                }); 

            if(graphViewChange.edgesToCreate != null) 
                graphViewChange.edgesToCreate.ForEach(edge =>
                {
                    NodeView parentView = edge.output.node as NodeView; 
                    NodeView childView = edge.input.node as NodeView;
                    tree.AddChild(parentView.node, childView.node);
                });

            if (graphViewChange.movedElements != null)
                nodes.ForEach((n) => { NodeView view = n as NodeView; view.SortChildren(); });

            EditorUtility.SetDirty(tree);
            AssetDatabase.SaveAssets(); 


            return graphViewChange; 
        } 
        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            {
                var types = TypeCache.GetTypesDerivedFrom<ActionNode>();
                foreach(var type in types)
                {
                    evt.menu.AppendAction($"[{type.BaseType.Name}] {type.Name}", (a) => CreateNode(type));
                }

            }
            {
                var types = TypeCache.GetTypesDerivedFrom<CompositeNode>();
                foreach (var type in types)
                {
                    evt.menu.AppendAction($"[{type.BaseType.Name}] {type.Name}", (a) => CreateNode(type));
                }

            }
            {
                var types = TypeCache.GetTypesDerivedFrom<DecoratorNode>();
                foreach (var type in types)
                {
                    evt.menu.AppendAction($"[{type.BaseType.Name}] {type.Name}", (a) => CreateNode(type));
                }

            }
        } 
        void CreateNode(System.Type type)
        {
            Node node = tree.CreateNode(type);
            CreateNodeView(node);
        }
        void CreateNodeView(Node node)
        {
            NodeView nodeView = new NodeView(node);
            nodeView.OnNodeSelected = OnNodeSelected;
            AddElement(nodeView);
        }

        public void UpdateNodeState()
            => nodes.ForEach(n =>  {
               NodeView view = n as NodeView;
               view.UpdateState();});

    }
#endif
}
