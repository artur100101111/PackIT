using PackIT.Shared.DtoTree.DtoTreeTraversal.TraverseStrategy;

namespace PackIT.Shared.DtoTree.DtoTreeTraversal
{

    /// <summary>
    /// Allows to Map TEntity hierachical tree (parent/child) to list of TResultNodes.
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TId"></typeparam>
    internal abstract class AbstractNodeListBuilder<TResult, TEntity, TId> : INodeListBuilder<TResult> where TEntity : class, ITreeNode<TEntity, TId> where TId : struct
    {
        private TEntity _rootNode;
        private ITreeTraverseStrategy<TEntity, TId> _traverseStrategy;
        private ITreeNodeMapper<TResult, TEntity> _nodeMapper;

        public AbstractNodeListBuilder(TEntity rootNode, ITreeTraverseStrategy<TEntity,TId> traverseStrategy, ITreeNodeMapper<TResult, TEntity> nodeMapper)
        {
            _rootNode = rootNode;
            _traverseStrategy = traverseStrategy;
            _nodeMapper = nodeMapper;
        }
        public virtual IEnumerable<TResult> Build()
        {
            foreach (var node in _traverseStrategy.Traverse(_rootNode))
            {
                yield return _nodeMapper.Map(node);
            }
        }

    }
}
