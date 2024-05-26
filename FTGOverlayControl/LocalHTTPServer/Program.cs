using System.Net;
using System.Text;

try
{
    HttpListener listener = new HttpListener();

    listener.Prefixes.Clear();
    listener.Prefixes.Add(@"http://+:8080/");

    listener.Start();

    while (true)
    {
        HttpListenerContext context = listener.GetContext();
        HttpListenerRequest request = context.Request;

        HttpListenerResponse response = context.Response;

        // HTMLを表示する
        if (request != null)
        {
            try
            {
                response.ContentType = Path.GetExtension(request.Url.LocalPath) switch
                {
                    ".css" => "text/css",
                    ".html" => "text/html",
                    ".png" => "image/png",
                    ".js" => "text/javascript",
                    _ => "text/plain"
                };

                string path = $"{AppDomain.CurrentDomain.BaseDirectory}{request.Url.LocalPath}";
                using (Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    int count = -1; byte[] buffer = new byte[4096];
                    for (; (count = stream.Read(buffer, 0, buffer.Length)) != 0;)
                        response.OutputStream.Write(buffer, 0, count);
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
        }
        else
        {
            response.StatusCode = 404;
        }
        response.Close();
    }

}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}