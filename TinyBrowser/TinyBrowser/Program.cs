using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

public class Program {

    static string FindTextBetweenTags(string original, string start, string end) {
        throw new NotImplementedException();
    }
    
    static void Main(string[] arguments) {
        var host = "acme.com";
        var uri = "/";
        // Here, acme.com is only used for DNS-Resolving and gives us an IP
        var tcpClient = new TcpClient(host, 80);
        var stream = tcpClient.GetStream();
        var streamWriter = new StreamWriter(stream, Encoding.ASCII);
        // Here, acme.com is passed to the Webserver at the IP, so it knows, which website to give us
        // In case, that one computer hosts multiple websites (think of Web-Hosts like wix.com)
        
        /*
         * GET / HTTP.1.1
         * Host: acme.com
         * Content-Length: 7
         *
         * abcdefg
         */
        
        // This is a valid HTTP/1.1-Request to send:
        var request = $"GET {uri} HTTP/1.1\r\nHost: {host}\r\n\r\n";
        streamWriter.Write(request); // add data to the buffer
        streamWriter.Flush(); // actually send the buffered data

        var streamReader = new StreamReader(stream);
        var response = streamReader.ReadToEnd();

        var uriBuilder = new UriBuilder(null, host);
        uriBuilder.Path = uri;
        Console.WriteLine($"Opened {uriBuilder}");
        // ======= FindTextBetweenTags Function START ========
        var titleTag = "<title>";
        // Find the start of the <title>-Tag
        var titleIndex = response.IndexOf(titleTag);
        string title = string.Empty;
        if (titleIndex != -1) {
            // Offset the index by the length of the <title>-Tag, to ommit it
            titleIndex += titleTag.Length;
            // Find the start of the </title>-End-Tag
            var titleEndIndex = response.IndexOf("</title>");
            if (titleEndIndex > titleIndex) {
                // Get the string in between both
                title = response[titleIndex..titleEndIndex];
            }
        }
        // ======= FindTextBetweenTags Function END ========
        Console.WriteLine("Title: "+title);

        var titleText = FindTextBetweenTags(response, "<title>", "</title>");
    }
}