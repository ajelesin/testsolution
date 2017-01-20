namespace Service
{
    using System;
    using System.IO;
    using System.Linq;
    using Database;
    using DataContracts;
    using NLog;

    public class LineService : ILineService
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public Result UploadFile(UploadedFile file)
        {
            try
            {
                Logger.Info("UploadFile called");
                using (var streamReader = new StreamReader(file.FileByteStream))
                {
                    using (var context = new LineContext())
                    {
                        string line;
                        while ((line = streamReader.ReadLine()) != null)
                        {
                            context.Lines.Add(new Line {Value = line});
                        }
                        context.SaveChanges();
                    }
                }

                return new Result { Value = true };
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex.ToString);
                return new Result {Value = false };
            }
        }

        public string[] FindLines(string substring)
        {
            try
            {
                Logger.Info("FindLines called");
                using (var context = new LineContext())
                {
                    var foundLines = context.Lines
                        .Where(o => o.Value.Contains(substring))
                        .Select(o => o.Value)
                        .ToArray();

                    return foundLines;
                }
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex.ToString);
                return null;
            }
        }
    }
}
