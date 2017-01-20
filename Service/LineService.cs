namespace Service
{
    using System;
    using System.IO;
    using System.Linq;
    using Database;
    using DataContracts;
    using EntityFramework.BulkInsert.Extensions;
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
                        var content = streamReader.ReadToEnd()
                            .Split('\n');

                        context.Configuration.AutoDetectChangesEnabled = false;
                        context.Configuration.ValidateOnSaveEnabled = false;

                        context.BulkInsert(content.Select(o => new Line {Value = o}));
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

        public SearchResult FindLines(string substring, int pageNo, int pageSize)
        {
            try
            {
                Logger.Info("FindLines called");
                using (var context = new LineContext())
                {
                    var foundLines = context.Lines
                        .AsNoTracking()
                        .Where(o => o.Value.Contains(substring))
                        .Select(o => o.Value)
                        .ToList();

                    var pageAmount = (int) Math.Floor((double) foundLines.Count / (double) pageSize);

                    var displayedLines = foundLines
                        .Skip((pageNo - 1)*pageSize)
                        .Take(pageSize)
                        .ToList();

                    return new SearchResult
                    {
                        Lines = displayedLines,
                        PageNo = pageNo,
                        PageSize = pageSize,
                        PageAmount = pageAmount
                    };
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
