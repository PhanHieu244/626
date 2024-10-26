using System;
using System.Collections.Generic;

namespace DefaultNamespace
{
    public class Test : MonoBehaviour
    {
        private void Start()
        {
            Program.Main();
        }
    }
    
    public class Program
    {
        public static void Main()
        {
            var items = new List<Item>
            {
                new Item("item1", null),
                new Item("item2", null),
                new Item("item2::1", "item2"),
                new Item("item2::2", "item2"),
                new Item("item2::3", "item2"),
                new Item("item1::1", "item1"),
                new Item("item3::1", "item3"),
                new Item("item1::2", "item1"),
                new Item("item3", null),
                new Item("item1::2::1", "item1::2"),
                new Item("item3::1", "item3"),
            };
            
            var root = CreateTree(items);
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(root);
            Console.WriteLine(json);
        }

        public static TreeNode CreateTree(List<Item> items)
        {
            if (items == null || items.Count <= 0) return null;
            var root = new TreeNode(null);
            var queue = new Queue<(TreeNode node, string parentKey)>();
            int limit = items.Count * 3;
            //create node from item and add to queue
            foreach (var item in items)
            {
                var node = new TreeNode(item.key);
                queue.Enqueue((node, item.parent));
            }

            int count = 0;
            //insert item to queue
            while (queue.TryDequeue(out var node))
            {
                var parentNode = Insert(root, node.node, node.parentKey);
                //if cant find parent node, enqueue again
                if (parentNode == null) queue.Enqueue(node);
                count++;
                //break loop by limit
                if (count >= limit) break;
            }

            return root;
        }

        public static TreeNode Insert(TreeNode root, TreeNode node, String parentKey)
        {
            var parent = FindNode(root, parentKey);
            if (parent == null) return null;
            parent.AddChild(node);
            return parent;
        }

        public static TreeNode FindNode(TreeNode node ,String key)
        {
            if (node == null) return null;
            if (key != null)
            {
                if (node.key != null && node.key.Equals(key)) return node;
                foreach (var nodeChild in node.children)
                {
                    //check path
                    if (key.StartsWith(nodeChild.key)) return FindNode(nodeChild, key);
                }

                return null;
            }
            
            if (node.key == null) return node; //root
            return null;
        }
    }

    public class TreeNode
    {
        public String key;
        public List<TreeNode> children;

        public TreeNode(String key)
        {
            this.key = key;
            children = new List<TreeNode>();
        }

        public void AddChild(TreeNode child)
        {
            children.Add(child);
        }
    }

    public class Item
    {
        public String key;
        public String parent;

        public Item(String key, String parent)
        {
            this.key = key;
            this.parent = parent;
        }
    }
}