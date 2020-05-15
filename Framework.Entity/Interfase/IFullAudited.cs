namespace Framework.Entity
{
    public interface IFullAudited<T> : IBaseEntity<T>, ICreationAudited,IHasCreationTime,IModificationAudited,IHasModificationTime,IPassivable, ISoftDelete
    {
    }
}
