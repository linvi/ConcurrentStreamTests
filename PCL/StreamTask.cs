using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PCL
{
    public class StreamTask
    {
        private readonly string _url;
        private readonly Action<string> _jsonReceived;
        private HttpClient _httpClient;
        private HttpResponseMessage _response;
        private Stream _stream;
        private StreamReader _reader;

        public StreamTask(string url, Action<string> jsonReceived)
        {
            _url = url;
            _jsonReceived = jsonReceived;
        }

        public async Task Start()
        {
            Debug.WriteLine("StreamTask.Start");

            _httpClient = GetHttpClient();
            _reader = await GetStreamReader(_httpClient, _url);

            string json = null;

            do
            {
                try
                {
                    json = await _reader.ReadLineAsync();

                    if (_jsonReceived != null)
                    {
                        _jsonReceived.Invoke(json);
                    }
                }
                catch (Exception ex)
                {
                    if (ex is ObjectDisposedException || ex is IOException)
                    {
                        json = null;
                    }
                    else
                    {
                        throw;
                    }
                }
            } while (json != null);

            Debug.WriteLine("StreamTask.Start Completed");
        }

        private HttpClient GetHttpClient()
        {
            var httpClient = new HttpClient(new StreamHttpHandler());
            httpClient.Timeout = Timeout.InfiniteTimeSpan;

            return httpClient;
        }

        private async Task<StreamReader> GetStreamReader(HttpClient client, string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            _response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            _stream = await _response.Content.ReadAsStreamAsync();

            return new StreamReader(_stream, Encoding.GetEncoding("utf-8"));
        }

        public void Stop()
        {
            _httpClient?.Dispose();
            _response?.Dispose();
            _stream?.Dispose();
            _reader?.Dispose();

            _httpClient = null;
            _response = null;
            _stream = null;
            _reader = null;
        }
    }
}