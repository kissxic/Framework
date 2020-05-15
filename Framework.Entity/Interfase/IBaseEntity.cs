namespace Framework.Entity
{
    public interface IBaseEntity<T>
    {
        /// <summary>
        /// 主键id
        /// </summary>
         T Id { get; set; }
    }
}
