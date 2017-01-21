namespace Service
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.IO;
    using System.Linq;
    using Database;
    using DataContracts;
    using DataContracts.Requests;
    using DataContracts.Responses;
    using EntityFramework.BulkInsert.Extensions;
    using NLog;

    public class LineService : ILineService
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public Response UploadFile(UploadedFile file)
        {
            try
            {
                Logger.Info("UploadFile called");
                using (var streamReader = new StreamReader(file.FileByteStream))
                using (var context = new LineContext())
                {
                    UploadFile_Strategy2(context, streamReader);
                }
                
                return new Response { Success = true };
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex.ToString);
                return new Response {Success = false };
            }
        }

        public SearchResponse FindLines(string substring, int pageNo, int pageSize)
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

                    return new SearchResponse
                    {
                        Success = true,
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
                return new SearchResponse { Success = false };
            }
        }

        private static void UploadFile_Strategy2(LineContext context, StreamReader streamReader)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    context.Configuration.AutoDetectChangesEnabled = false;
                    context.Configuration.ValidateOnSaveEnabled = false;

                    string line;
                    var lines = new List<string>();
                    var i = 0;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        lines.Add(line);
                        if (++i % 10000 != 0) continue;

                        context.BulkInsert(lines.Select(o => new Line { Value = o }), transaction.UnderlyingTransaction);
                        lines.Clear();
                    }

                    context.BulkInsert(lines.Select(o => new Line { Value = o }), transaction.UnderlyingTransaction);
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction?.Rollback();
                    throw;
                }
            }
        }
    }
}
