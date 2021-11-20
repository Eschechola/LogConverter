using Bogus.DataSets;

namespace LogConverter.Tests.Fakers
{
    public static class LogUrlFaker
    {
        public static string GetValidLogUrl()
            => "https://s3.amazonaws.com/uux-itaas-static/minha-cdn-logs/input-01.txt";

        public static string GetInvalidLogUrl()
            => "https://" + new Lorem().Word() + ".com";
    }
}
