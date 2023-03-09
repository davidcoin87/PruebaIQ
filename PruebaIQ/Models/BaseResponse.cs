namespace PruebaIQ.Models
{
    public class BaseResponse
    {
        public bool response { get; set; }
        public string message { get; set; }
        public string token { get; set; }
        public dynamic result { get; set; }

        public BaseResponse()
        {
            response = false;
            message = "Ocurrió un error inesperado";
        }
    }
}
