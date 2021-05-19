using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using static System.Int32;

namespace TinyBrowser {
    public class Program {
        static void Main(string[] arguments) {
            var host = "acme.com";
            var uri = "/";
            var uriHistory = new Stack<string>();

            while (true) {
                var tcpClient = new TcpClient(host, 80);
                var stream = tcpClient.GetStream();
                var streamWriter = new StreamWriter(stream, Encoding.ASCII);
        
                /*
                 * GET / HTTP.1.1
                 * Host: acme.com
                 * Content-Length: 7
                 *
                 * abcdefg
                 */
            
                var request = $"GET {uri} HTTP/1.1\r\nHost: {host}\r\n\r\n";
                streamWriter.Write(request); // add data to the buffer
                streamWriter.Flush(); // actually send the buffered data

                var streamReader = new StreamReader(stream);
                var response = streamReader.ReadToEnd();

                var uriBuilder = new UriBuilder(null, host);
                uriBuilder.Path = uri;
                Console.WriteLine($"Opened {uriBuilder}");

                var titleText = FindTextBetweenTags(response, "title");
                Console.WriteLine("Title: " + titleText);

                var hrefLinks = GetTextBetweenStringsFromString(response, "<a href=\"", "\">");
                var hrefLinkList = hrefLinks.ToList();
                var hrefDescriptionList = new List<string>();

                for (var i = 0; i < hrefLinkList.Count; i++) {
                    var description = GetTextBetweenStringsFromString(response, hrefLinkList[i] + "\">", "</a>");
                    hrefDescriptionList.AddRange(description);

                    Console.WriteLine($"{i}: {hrefDescriptionList[i]} ({hrefLinkList[i]})");
                }
                
                var userInput = 0;
                while (true) {
                    Console.WriteLine("Text commands: 'back', 'home', 'exit'");
                    Console.WriteLine($"Enter a number between 0-{hrefLinkList.Count - 1} to navigate:");
                    var rawInput = Console.ReadLine();

                    if (!rawInput.Any(char.IsDigit)) {
                        switch (rawInput) {
                            case "back":
                                uri = uriHistory.Count > 0 ? uriHistory.Pop() : "/";
                                break;
                            case "home":
                                uri = "/";
                                break;
                            case "exit":
                                userInput = -1;
                                break;
                            default:
                                Console.WriteLine("Unknown command.\n");
                                continue;
                        }
                        break;
                    }
                    
                    var input = TryParse(rawInput, out userInput);
                    if (!input || userInput < 0 || userInput > hrefLinkList.Count - 1) 
                        continue;
                    uriHistory.Push(uri);
                    uri = "/" + hrefLinkList[userInput];
                    break;
                }
                if (userInput == -1) 
                    break;
            }
        }
        
        static string FindTextBetweenTags(string original, string tag) {
            var startTag = $"<{tag}>";
            var tagIndex = original.IndexOf(startTag);
            string result = string.Empty;
            if (tagIndex != -1) {
                tagIndex += startTag.Length;
                var tagEndIndex = original.IndexOf($"</{tag}>");
                if (tagEndIndex > tagIndex) {
                    result = original[tagIndex..tagEndIndex];
                }
            }
            return result;
        }
    
        static IEnumerable<string> GetTextBetweenStringsFromString(string text, string start, string end) {
            int currentIndex = 0;
            while (true) {
                var startIndex = text.IndexOf(start, currentIndex);
                if (startIndex == -1)
                    yield break;
                var endIndex = text.IndexOf(end, startIndex);
                if (endIndex == -1)
                    yield break;

                yield return text[(startIndex + start.Length)..endIndex];
                currentIndex = endIndex;
            }
        }
    }
}