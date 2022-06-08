using System.Collections.Generic;
using System.IO;

namespace ExcelToSqlScript
{
    public class Options 
    {
        public string InputFile { get; set; }

        public string OutputDirectory { get; set; } = "./sql-scripts/";

        public List<int> WorksheetsToRead { get; set; }
        public bool ReadEmptyRecords { get; set; }
        
        public List<string> NullReplacements { get; set; }
    }

    //public class OptionsValidator : AbstractValidator<Options>
    //{
    //    public OptionsValidator()
    //    {
    //        CascadeMode = CascadeMode.StopOnFirstFailure;
            
    //        RuleFor(x => x.InputFile).NotEmpty().WithMessage("Please provide a path to excel file")
    //            .Must(File.Exists).WithMessage(o => $"file not found:  {o.InputFile}");
    //    }
    //}
}