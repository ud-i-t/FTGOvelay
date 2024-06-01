using System.Net;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

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
                    ".json" => "application/json",
                    _ => string.Empty
                };

                if (response.ContentType == null || response.ContentType == string.Empty)
                {
                    throw new FileNotFoundException();
                }

                string contentsDir = Path.GetFullPath($"{AppDomain.CurrentDomain.BaseDirectory}/contents/");
                string path = Path.GetFullPath(contentsDir + request.Url.LocalPath);

                if (!path.StartsWith(contentsDir, StringComparison.OrdinalIgnoreCase))
                {
                    throw new Exception("invalid path.");
                }

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
                response.StatusCode = 404;
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