﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace NetStandard
{
    public class StreamResultGenerator
    {
        private StreamTask _streamTask;

        public async Task StartStreamAsync(string url, Action<string> jsonReceived)
        {
            _streamTask = new StreamTask(url, jsonReceived);

            await Task.Run(async () =>
            {
                await _streamTask.Start();
            }).ConfigureAwait(false);
        }

        public void StopStream()
        {
            _streamTask.Stop();
        }
    }
}