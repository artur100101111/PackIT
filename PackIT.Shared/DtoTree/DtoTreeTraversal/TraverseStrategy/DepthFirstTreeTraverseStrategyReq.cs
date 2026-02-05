using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackIT.Shared.DtoTree.DtoTreeTraversal.TraverseStrategy
{
    public class DepthFirstTreeTraverseStrategyReq<TEntity, TId> : ITreeTraverseStrategy<TEntity, TId> where TEntity : class, ITreeNode<TEntity, TId>
 where TId : struct
    {
        public IEnumerable<TEntity> Traverse(TEntity root)
        {
            if (root == null)
                return Enumerable.Empty<TEntity>();

            List<TEntity> nodes = new List<TEntity>();
            TraverseReq(root, nodes);
            return nodes;
        }

        private void TraverseReq(TEntity root, List<TEntity> nodes)
        {
            nodes.Add(root); //za każdym razem dodajemy do listy noda i przechodzimy dalej.
            foreach (var node in root.Children)
            {
                TraverseReq(node, nodes);
            }
        }
    }
}
