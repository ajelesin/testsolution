namespace Service
{
    using System;
    using System.Linq;
    using Database;
    using NLog;

    public class LineService : ILineService
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public bool SaveLines(string[] lines)
        {
            try
            {
                Logger.Info("SaveLines called");
                using (var context = new LineContext())
                {
                    context.Lines.AddRange(lines.Select(o => new Line {Value = o}));
                    context.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex.ToString);
                return false;
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
