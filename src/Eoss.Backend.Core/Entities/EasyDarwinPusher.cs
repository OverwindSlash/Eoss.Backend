namespace Eoss.Backend.Entities
{
    public class EasyDarwinGetPusherResponse
    {
        public int total { get; set; }
        public EasyDarwinPusher[] rows { get; set; }
    }

    public class EasyDarwinPusher
    {
        public string id { get; set; }
        public long inBytes { get; set; }
        public int onlines { get; set; }
        public long outBytes { get; set; }
        public string path { get; set; }
        public string source { get; set; }
        public string startAt { get; set; }
        public string transType { get; set; }
        public string url { get; set; }
    }

}
