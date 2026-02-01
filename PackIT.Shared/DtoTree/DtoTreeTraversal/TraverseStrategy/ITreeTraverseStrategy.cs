using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackIT.Shared.DtoTree.DtoTreeTraversal.TraverseStrategy
{
    internal interface ITreeTraverseStrategy<TEntity, TId> where TEntity : class, ITreeNode<TEntity,TId> where TId : struct
    {
        IEnumerable<TEntity> Traverse(TEntity root);
    }
}
