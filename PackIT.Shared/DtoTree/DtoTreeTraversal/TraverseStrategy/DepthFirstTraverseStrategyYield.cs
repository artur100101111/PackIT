using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackIT.Shared.DtoTree.DtoTreeTraversal.TraverseStrategy
{
    public class DepthFirstTraverseStrategyYield<TEntity, TId> : ITreeTraverseStrategy<TEntity, TId> where TEntity : class, ITreeNode<TEntity, TId>
      where TId : struct
    {
        public IEnumerable<TEntity> Traverse(TEntity root)
        {
            if (root == null)
                yield break;

            yield return root;

            foreach (var node in root.Children)
                foreach (var n in Traverse(node))
                    yield return n;
        }
    }
}
