using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackIT.Shared.DtoTree.DtoTreeTraversal.TraverseStrategy
{
    internal class BreadthFirstTraverseStrategy<TEntity, TId> : ITreeTraverseStrategy<TEntity, TId> where TEntity : class, ITreeNode<TEntity, TId>
     where TId : struct
    {
        public IEnumerable<TEntity> Traverse(TEntity root)
        {
            if (root == null)
                yield break;

            var queue = new Queue<TEntity>();
            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                yield return node;

                foreach (var n in node.Children)
                    queue.Enqueue(n);
            }

        }
    }
}
