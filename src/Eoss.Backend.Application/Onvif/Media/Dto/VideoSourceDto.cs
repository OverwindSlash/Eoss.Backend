namespace Eoss.Backend.Onvif.Dto
{
    public class VideoSourceDto
    {
        public string Token { get; set; }
        public string StreamUri { get; set; }
        public string ServerStreamUri { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int VideoWidth { get; set; }
        public int VideoHeight { get; set; }
        public float Framerate { get; set; }
    }
}
