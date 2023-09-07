using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        string url = "http://localhost:8888/";
        HttpListener listener = new HttpListener();
        listener.Prefixes.Add(url);

        Console.WriteLine("Listening for incoming requests...");
        listener.Start();

        while (true)
        {
            HttpListenerContext context = listener.GetContext();
            ThreadPool.QueueUserWorkItem((ctx) =>
            {
                HandleRequest(ctx as HttpListenerContext);
            }, context);
        }
    }

    static void HandleRequest(HttpListenerContext context)
    {
        HttpListenerRequest request = context.Request;
        HttpListenerResponse response = context.Response;

        string resourcePath = request.Url.Segments[1].TrimEnd('/');

        switch (resourcePath)
        {
            case "MyName":
                SendResponse(response, HttpStatusCode.OK, "Diana");
                break;
            case "Information":
                SendResponse(response, HttpStatusCode.Continue, "Information Response");
                break;
            case "Success":
                SendResponse(response, HttpStatusCode.OK, "Success Response");
                break;
            case "Redirection":
                SendResponse(response, HttpStatusCode.Redirect, "Redirection Response");
                break;
            case "ClientError":
                SendResponse(response, HttpStatusCode.BadRequest, "Client Error Response");
                break;
            case "ServerError":
                SendResponse(response, HttpStatusCode.InternalServerError, "Server Error Response");
                break;
            case "MyNameByHeader":
                GetMyNameByHeader(response);
                break;
            case "MyNameByCookies":
                GetMyNameByCookies(response);
                break;
            default:
                SendResponse(response, HttpStatusCode.NotFound, "Resource not found.");
                break;
        }

        response.Close();
    }

    static void SendResponse(HttpListenerResponse response, HttpStatusCode statusCode, string message)
    {
        response.StatusCode = (int)statusCode;
        response.ContentType = "text/plain";
        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(message);
        response.ContentLength64 = buffer.Length;
        response.OutputStream.Write(buffer, 0, buffer.Length);
    }

    static void GetMyNameByHeader(HttpListenerResponse response)
    {
        response.Headers.Add("X-MyName", "Diana");
        SendResponse(response, HttpStatusCode.OK, "Your Name (via Header)");
    }

    static void GetMyNameByCookies(HttpListenerResponse response)
    {
        response.Cookies.Add(new Cookie("MyName", "Diana"));
        SendResponse(response, HttpStatusCode.OK, "Your Name (via Cookies)");
    }
}
