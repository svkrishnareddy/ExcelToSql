﻿using System.IO;
using System.Text;

namespace ExcelToSqlScript
{
    public class TableScriptGenerator
    {
        readonly IQueryMaker _queryMaker;

        public TableScriptGenerator(IQueryMaker queryMaker)
        {
            _queryMaker = queryMaker;
        }

        public Script GenerateTableScript(Table table)
        {
            MemoryStream memoryStream = new MemoryStream();

            using (StreamWriter streamWriter = new StreamWriter(memoryStream, Encoding.Unicode, 4096, true))
            {
                streamWriter.AutoFlush = true;
                foreach (var tableRecord in table.Records)
                {
                    streamWriter.Write(_queryMaker.GenerateQuery(tableRecord));
                }
            }

            memoryStream.Position = 0;
            return new Script(table.Name, memoryStream);
        }
    }
}