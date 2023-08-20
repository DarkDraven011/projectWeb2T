namespace Web2T.ModelViews
{
    public class DemoVM
    {
        public string name { get; set; }
        public int code { get; set; }
        public string division_type { get; set; }
        public string codename { get; set; }
        public int province_code { get; set; }
        public int phone_code { get; set; }
        public List<object> wards { get; set; }
    }
}
