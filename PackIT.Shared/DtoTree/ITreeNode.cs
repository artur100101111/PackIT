namespace PackIT.Shared.DtoTree
{
    public interface ITreeNode<TEntity, TId> where TEntity : class where TId : struct
    {
        TId Id { get; set; }
        TId? ParentId { get; set; }

        List<TEntity> Children { get; }
        void AddChild(TEntity entity);
    }
}
