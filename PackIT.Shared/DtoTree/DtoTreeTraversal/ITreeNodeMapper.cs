namespace PackIT.Shared.DtoTree.DtoTreeTraversal
{
    public interface ITreeNodeMapper<out TResult, TEntity> where TEntity : class
    {
        TResult Map(TEntity entity);
    }
}