namespace PackIT.Domain.Common
{
    public interface IEntity<T> where T :  IEquatable<T>
    {
        public T Id { get; }
      //  public bool IsDeleted { get; }
    }
}
