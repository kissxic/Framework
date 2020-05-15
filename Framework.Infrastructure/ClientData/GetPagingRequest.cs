namespace Framework.Infrastructure
{
    public class GetPagingRequest
    {
        public GetPagingRequest(int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
        }
        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; private set; }
        /// <summary>
        /// 每页的记录数
        /// </summary>
        public int PageSize { get; private set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; } = false;
    }
}
