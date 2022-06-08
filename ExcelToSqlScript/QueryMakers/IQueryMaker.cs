using System;

namespace ExcelToSqlScript
{
    public interface IQueryMaker
    {
        string GenerateQuery(Record record);
    }
}