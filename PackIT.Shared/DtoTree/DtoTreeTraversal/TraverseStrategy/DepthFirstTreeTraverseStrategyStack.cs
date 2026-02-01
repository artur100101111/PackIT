using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackIT.Shared.DtoTree.DtoTreeTraversal.TraverseStrategy
{
    public class DepthFirstTreeTraverseStrategyStack<TEntity, TId> : ITreeTraverseStrategy<TEntity, TId> where TEntity : class, ITreeNode<TEntity, TId>
    where TId : struct
    {
        public IEnumerable<TEntity> Traverse(TEntity root)
        {
            if (root == null)
                yield break;

            var stack = new Stack<TEntity>();
            stack.Push(root);

            while (stack.Count > 0)
            {
                var node = stack.Pop();
                yield return node;

                for (var i = node.Children.Count() - 1; i >= 0; i--)
                {
                    var children = node.Children.ToList();
                    stack.Push(children[i]);
                }
            }
        }
    }
}
