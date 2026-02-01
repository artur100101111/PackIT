namespace PackIT.Shared.DtoTree.DtoTreeBuilder
{

    /// <summary>
    /// From ITreeNode<TEntity> flat list of hierarchical data it makes Tree Root ITreeNode with children.
    /// List can have Only One Root- marked by ParentId=null or IsRoot=true property.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TId"></typeparam>
    public abstract class DtoTreeBuilder<TEntity, TId> : IDtoTreeBuilder<TEntity> where TEntity : class, ITreeNode<TEntity, TId> where TId : struct
    {

        protected DtoTreeBuilder()
        {
        }

        public TEntity BuildTree(IEnumerable<TEntity> nodes)
        {
            var index = nodes.ToDictionary(k => k.Id);

            TEntity rootNode = null;

            foreach (var node in nodes)
            {
                if (node.ParentId == null)
                {
                    if (rootNode != null)
                        throw new TwoRootsInTheNodeListException($"There is a root with Id:{rootNode.Id} already in the tree, instead of Id:{node.Id}. There can be only one.");
                    rootNode = node;
                }
                else
                {
                    index[node.ParentId.Value].AddChild(node);
                }
            }
            return rootNode!;
        }
    }
}
