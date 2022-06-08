using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace ExcelToSqlScript
{
        
    public class App
    {
        private readonly Options _options;

        public App(Options options)
        {
            _options = options;
        }
        

        public int Insert()
        {
            return Process("insert");
        }


        public int Update()
        {
            return Process("update");
        }


        public int Merge()
        {
            return Process("merge");
        }

        private int Process(string mode)
        {
            try
            {
                Directory.CreateDirectory(_options.OutputDirectory);

                ExcelReader excelReader = new ExcelReader(_options.ReadEmptyRecords, _options.WorksheetsToRead?.ToArray());

                ValueRenderer valueRenderer = new ValueRenderer(_options.NullReplacements?.ToArray());
            
                IQueryMaker queryMaker = QueryMakerFactory.Create(mode, valueRenderer);

                TableScriptGenerator tableScriptGenerator = new TableScriptGenerator(queryMaker);

                IEnumerable<Table> tables = excelReader.Read(_options.InputFile);

                foreach (Table table in tables)
                {
                    string filePath = Path.Combine(_options.OutputDirectory, table.Name + ".sql");
                    Console.Write($"writing {filePath} ...");

                    if (table.Records.Any())
                    {
                        using (Script script = tableScriptGenerator.GenerateTableScript(table))
                        {
                            using (FileStream fileStream = File.Create(filePath))
                            {
                                script.Content.CopyTo(fileStream);
                                Console.WriteLine(" done");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine(" empty (skipped)");
                    }
                }

                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.GetType().Name}");
                Console.WriteLine($"Error: {ex.Message}");

                Console.WriteLine(ex.StackTrace);

                return 1;
            }
        }
    }
}