namespace IQtidorly.Api.Response
{
    public static class JsonResponseExtentions
    {
        public static JsonResponse ToPagination(this JsonResponse data, int skip, int take, int count)
        {
            return new JsonResponse()
            {
                Success = (data.Data != null),
                Data = new PaginationResponse(data.Data, skip, take, count),
                Code = (data.Data != null) ? "success" : "error"
            };
        }
    }
}
