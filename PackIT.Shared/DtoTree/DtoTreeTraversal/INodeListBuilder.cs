namespace PackIT.Shared.DtoTree.DtoTreeTraversal
{
    public interface INodeListBuilder<TResult>
    {
        public IEnumerable<TResult> Build();
    }
}
