using System;
using System.Threading.Tasks;

namespace NetStandard
{
    public class SampleStream
    {

        private StreamResultGenerator _streamResultGenerator;

        public event EventHandler<GenericEventArgs<string>> LineReceived;

        public SampleStream()
        {
            _streamResultGenerator = new StreamResultGenerator();
        }

        public async Task StartStreamAsync()
        {
            await _streamResultGenerator.StartStreamAsync("http://localhost:3000/stream", json =>
            {
                if (LineReceived != null)
                {
                    LineReceived.Invoke(this, new GenericEventArgs<string>(json));
                }
            }).ConfigureAwait(false);
        }

        public void StopStream()
        {
            _streamResultGenerator.StopStream();
        }
    }
}