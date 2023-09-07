using System;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        string[] urls = {
            "http://localhost:8888/MyName/",
            "http://localhost:8888/Information/",
            "http://localhost:8888/Success/",
            "http://localhost:8888/Redirection/",
            "http://localhost:8888/ClientError/",
            "http://localhost:8888/ServerError/",
            "http://localhost:8888/MyNameByHeader/",
            "http://localhost:8888/MyNameByCookies/"
        };

        using (HttpClient client = new HttpClient(new HttpClientHandler() { UseCookies = false }))
        {
            foreach (string url in urls)
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"URL: {url}, Status Code: {(int)response.StatusCode}");
                        if (url.EndsWith("/MyNameByHeader/"))
                        {
                            if (response.Headers.TryGetValues("X-MyName", out var headerValues))
                            {
                                string nameFromHeader = headerValues.GetEnumerator().Current;
                                Console.WriteLine($"Name is: {nameFromHeader}");
                            }
                        }
                        else if (url.EndsWith("/MyNameByCookies/"))
                        {
                            string cookieHeader = response.Headers.GetValues("Set-Cookie").ToString();
                            string[] cookieParts = cookieHeader.Split(';');
                            foreach (string part in cookieParts)
                            {
                                if (part.Trim().StartsWith("MyName="))
                                {
                                    string[] nameParts = part.Trim().Split('=');
                                    if (nameParts.Length == 2)
                                    {
                                        Console.WriteLine($"Name is: {nameParts[1]}");
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine($"URL: {url}, Error: {(int)response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"URL: {url}, Error: {ex.Message}");
                }
            }
        }
    }
}
