using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Vulpine.Core.Data;
using Vulpine.Core.Data.Lists;
using Vulpine.Core.Data.Trees;

namespace Data_Viewer.Views
{
    public partial class BinTreeViewer : UserControl
    {
        //flag, used to diferentiate user action, from programed action 
        private bool user_action;

        //stores the diffrent tree types examined
        private TreeBasic<String> basic;
        private TreeAVL<String> avl;
        private TreeRedBlack<String> redblack;
        private TreeSplay<String> splay;

        //stores a list of nodes that apear in the tree view
        private VList<TreeNode> tv_nodes;

        //stores the curently selected item
        private string selection;

        public BinTreeViewer()
        {
            InitializeComponent();

            basic = new TreeBasic<string>();
            avl = new TreeAVL<string>();
            redblack = new TreeRedBlack<string>();
            splay = new TreeSplay<string>();

            tv_nodes = new VListLinked<TreeNode>();
            user_action = true;
            selection = null;


            user_action = false;
            cboSort.SelectedIndex = 1;
            user_action = true;
        }

        /// <summary>
        /// Returns the tree implementation that is curently being
        /// displayed in the viewer
        /// </summary>
        public Tree<String> CurImpl
        {
            get
            {
                if (rdoBasic.Checked) return basic;
                if (rdoAVL.Checked) return avl;
                if (rdoRedBlack.Checked) return redblack;
                if (rdoSplay.Checked) return splay;

                throw new Exception("Should never reach here. ");
            }
        }

        /// <summary>
        /// Obtains the order in which the items in the tree should be
        /// listed, by reading from the drop-down box.
        /// </summary>
        public TreeOrder CurTreeOrder
        {
            get
            {
                switch(cboSort.SelectedIndex)
                {
                    case 0: return TreeOrder.PreOrder;
                    case 1: return TreeOrder.InOrder;
                    case 2: return TreeOrder.PostOrder;
                    //case 3: return TreeOrder.LevelOrder;
                    default: return TreeOrder.InOrder;
                }
            }
        }

        public IEnumerable<Tree<String>> ListImpls()
        {
            yield return basic;
            yield return avl;
            yield return redblack;
            yield return splay;
        }

        #region UpdateTreeView...

        /// <summary>
        /// Updates the tree view to reflect whatever changes have happend
        /// to the curent tree implementation, by basicly clearing out
        /// the view and rebuilding it from statch.
        /// </summary>
        private void UpdateTreeView()
        {
            Node<String> nroot = CurImpl.GetRoot();
            tv_nodes.Clear();

            //if there is no tree, we just need to clear the view
            if (nroot == null)
            {
                treeMainView.Nodes.Clear();
                lblStats.Text = "Size: 0   Max-Depth: 0   Min-Depth: 0";
                gboTree.Refresh();
                return;
            }

            //rebuilds the tree view recursivly
            TreeNode root = BuildTree(nroot);
            treeMainView.Nodes.Clear();
            treeMainView.Nodes.Add(root);

            //calculates the maximum and minimum depth the same way
            int maxDepth = CalMaxDepth(nroot);
            int minDepth = CalMinDepth(nroot);
            lblStats.Text = String.Format(
                "Size: {0}   Max-Depth: {1}   Min-Depth: {2}",
                CurImpl.Count, maxDepth, minDepth);

            //refreshes the entire area
            treeMainView.ExpandAll();
            SelectInView(root.Text);

            //treeMainView.SelectedNode = root;
            //gboTree.Refresh();
        }

        /// <summary>
        /// Used to recursivly build a TreeView tree from a set of nodes
        /// obtaned from a CVL Tree.
        /// </summary>
        /// <param name="node">Root of the tree to build</param>
        /// <returns>The corisponding TreeView node</returns>
        private TreeNode BuildTree(Node<String> node)
        {
            //obtains the children and creates an array to keep them in
            Node<String>[] children = node.ListChildren().ToArray();
            TreeNode tnode = null;

            string item = node.Data;
            string lable = String.Format("{0} [{1}]", item, item.Length);

            if (children.Length == 0)
            {
                //we have no children and the node is balanced
                tnode = new TreeNode(lable, 0, 0);
            }
            else if (children.Length == 1)
            {
                int balance = 0;

                //we must calculate the balance from the single height
                int height = CalMaxDepth(children[0]);
                if (height > 2) balance = 2;
                else balance = height;

                //we must recursivly create the children of the node
                TreeNode[] tchildren = new TreeNode[1];
                tchildren[0] = BuildTree(children[0]);

                //returns the new node, with it's single child
                tnode = new TreeNode(lable, balance, balance, tchildren);
            }
            else
            {
                //we must calculate the balance as the diffrenc of both subtrees
                int left = CalMaxDepth(children[0]);
                int right = CalMaxDepth(children[1]);
                int balance = Math.Abs(left - right);
                if (balance > 2) balance = 2;

                //we must recursivly create the children of the node
                TreeNode[] tchildren = new TreeNode[2];
                tchildren[0] = BuildTree(children[0]);
                tchildren[1] = BuildTree(children[1]);

                //returns the new node, with both of it's children
                tnode = new TreeNode(lable, balance, balance, tchildren);
            }

            tv_nodes.Add(tnode);
            return tnode;
        }

        /// <summary>
        /// Calculates the maximum depth of a subtree, given it's root node.
        /// </summary>
        /// <param name="node">Root of the subtree</param>
        /// <returns>Maximum depth of the subtree</returns>
        private int CalMaxDepth(Node<String> node)
        {
            int depth = 0;

            //determins the maximum depth of each subtree
            foreach (Node<String> child in node.ListChildren())
            {
                int temp = CalMaxDepth(child);
                if (temp > depth) depth = temp;
            }

            //adds one to the depth for ourselves
            return (depth + 1);
        }

        /// <summary>
        /// Calculates the minimum depth of a subtree, given it's root node.
        /// </summary>
        /// <param name="node">Root of the subtree</param>
        /// <returns>Minimum depth of the subtree</returns>
        private static int CalMinDepth(Node<String> root)
        {
            //if we don't have two children, our minimum depth is one
            Node<String>[] children = root.ListChildren().ToArray();
            if (children.Length < 2) return 1;

            int depth = Int32.MaxValue;

            //determins the minimum depth of each subtree
            foreach (Node<String> child in children)
            {
                int temp = CalMinDepth(child);
                if (temp < depth) depth = temp;
            }

            //adds one to the depth for ourselves
            return (depth + 1);
        }

        #endregion //////////////////////////////////////////////////////////////////

        /// <summary>
        /// Updates the list view, listing all the items curently in
        /// the tree in the curent view
        /// </summary>
        private void UpdateListView()
        {
            lstItems.Items.Clear();
            IEnumerable<String> ittr;

            switch (cboSort.SelectedIndex)
            {
                case 0: ittr = CurImpl.ListIn(TreeOrder.PreOrder); break;
                case 1: ittr = CurImpl.ListIn(TreeOrder.InOrder); break;
                case 2: ittr = CurImpl.ListIn(TreeOrder.PostOrder); break;
                case 3: ittr = CurImpl.ListInLevelOrder(); break;
                default: throw new Exception();
            }

            foreach (string s in ittr) lstItems.Items.Add(s);

            SelectInView(selection);
            //gboList.Refresh();
        }

        /// <summary>
        /// Inserts items into the curently active tree, and then updates
        /// the apropriate views to effect the change.
        /// </summary>
        /// <param name="items">A string of items to insert</param>
        /// <returns>The last item inserted</returns>
        internal string InsertItems(string items)
        {
            if (items == null || items == String.Empty) return null;

            //detemins what seperator to use (comma, space, or simicolan)
            char seperator = ' ';
            if (items.Contains(';')) seperator = ';';
            else if (items.Contains(',')) seperator = ',';
            else seperator = ' ';

            //inserts the items one by one
            string[] l_items = items.Split(seperator);
            foreach(string s in l_items) 
            {
                foreach (var tree in ListImpls())
                    tree.Add(s.Trim());
            }

            UpdateTreeView();
            UpdateListView();
            return l_items[l_items.Length - 1];
        }

        /// <summary>
        /// Clears all trees of data and updates the view.
        /// </summary>
        internal void ClearAll()
        {
            foreach (var tree in ListImpls()) tree.Clear();
            selection = null;
            UpdateTreeView();
            UpdateListView();           
        }

        /// <summary>
        /// Uptates the list and tree views to point to the named item.
        /// </summary>
        /// <param name="name">Item to select</param>
        private void SelectInView(string name)
        {
            //consider using a dictionary to map the names to there locations
            //in both the treeview and list view.
            user_action = false;
            selection = name;

            //finds the item we need in the list box
            for (int i = 0; i < lstItems.Items.Count; i++)
            {
                string temp = (string)lstItems.Items[i];
                if (temp == name)
                { lstItems.SelectedIndex = i; break; }
            }

            //finds the item we need in the tree view
            foreach (TreeNode tnode in tv_nodes)
            {
                if (tnode.Text == name)
                {
                    treeMainView.SelectedNode = tnode;
                    break;
                }
            }

            gboList.Refresh();
            gboTree.Refresh();
            user_action = true;
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            string items = txtInsert.Text;
            items = InsertItems(items);

            txtInsert.Clear();
            txtInsert.Refresh();
            if (items != null) SelectInView(items);
        }

        private void rdoAny_Click(object sender, EventArgs e)
        {
            if (sender == rdoSplay) btnRetrieve.Enabled = true;
            else btnRetrieve.Enabled = false;

            UpdateTreeView();
            UpdateListView();
        }

        private void cboSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (user_action) UpdateListView();
        }

        private void treeMainView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (user_action) SelectInView(e.Node.Text);
        }

        private void lstItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (user_action) SelectInView((string)lstItems.SelectedItem);
        }

        private void btnMax_Click(object sender, EventArgs e)
        {
            if (CurImpl.Empty) return;
            string s = CurImpl.GetMinMax(true);
            UpdateTreeView();
            UpdateListView();
            SelectInView(s);
        }

        private void btnGetMin_Click(object sender, EventArgs e)
        {
            if (CurImpl.Empty) return;
            string s = CurImpl.GetMinMax(false);
            UpdateTreeView();
            UpdateListView();
            SelectInView(s);
        }

        private void btnRetrieve_Click(object sender, EventArgs e)
        {
            if (selection != null) CurImpl.Retreve(selection);
            UpdateTreeView();
            UpdateListView();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selection != null) CurImpl.Remove(selection);
            UpdateTreeView();
            UpdateListView();
        }
        


    }
}
