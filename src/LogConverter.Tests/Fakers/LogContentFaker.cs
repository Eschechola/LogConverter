﻿using Bogus.DataSets;

namespace LogConverter.Tests.Fakers
{
    public static class LogContentFaker
    {
        public static string GetValidContent()
            => "312|200|HIT|\"GET /robots.txt HTTP/1.1\"|100.2" +
                "101|200|MISS|\"POST /myImages HTTP/1.1\"|319.4" +
                "199|404|MISS|\"GET /not-found HTTP/1.1\"|142.9" +
                "312|200|INVALIDATE|\"GET /robots.txt HTTP/1.1\"|245.1";

        public static string GetInvalidContent()
            => new Lorem().Paragraph(1);
    }
}
