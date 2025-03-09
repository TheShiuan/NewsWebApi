namespace News.Dtos
{
    public class ReturnJson
    {
        public dynamic Data { get; set; }
        public int HttpCode { get; set; }
        public dynamic ErrorMsg { get; set; }
    }
}
