namespace CodePruner.Examples.BackgroundTask.Hangfire
{
    public class SlowFileProcessor
    {
        public async Task ProcessFileAsync(int fileId)
        {
            Console.WriteLine("[{0:HH:mm:ss}] Processing file {1}...", DateTime.Now, fileId);
            await Task.Delay(10000);
            Console.WriteLine("[{0:HH:mm:ss}] Processed file {1} ", DateTime.Now, fileId);

        }
    }
}
