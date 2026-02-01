namespace PackIT.Shared.DtoTree.DtoTreeBuilder
{
    public interface IDtoTreeBuilder<TEntity>
    {
        public TEntity BuildTree(IEnumerable<TEntity> entities);
    }
}