using System.Text.RegularExpressions;

namespace NewsWebAPI.Parameters
{
    public class NewsSelectParamter
    {
        public string type { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public Guid InsertEmployeeId { get; set; }
        public Guid UpdateEmployeeId { get; set; }
        public DateTime? InsertDateTime { get; set; }
        public int? minClick { get; set; }
        public int? maxClick { get; set; }

        private string _click;
        public string Click
        {
            get { return _click; }
            set
            {
                //2-3
                Regex regex = new Regex(@"^\d*-\d*$");
                if (regex.Match(value).Success)
                {
                    minClick = Int32.Parse(value.Split('-')[0]);
                    maxClick = Int32.Parse(value.Split('-')[1]);
                }
                _click = value;
            }
        }
    }
}
