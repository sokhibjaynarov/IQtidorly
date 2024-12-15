using IQtidorly.Api.ViewModels.Base;

namespace IQtidorly.Api.Response
{
    public class PaginationResponse
    {
        public PaginationResponse(object list, int skip, int take, int count)
        {
            List = list;
            Pagination = new Pagination()
            {
                Count = count,
                Skip = skip,
                Take = take
            };
        }

        public object List { get; set; }
        public Pagination Pagination { get; set; }
    }
}
