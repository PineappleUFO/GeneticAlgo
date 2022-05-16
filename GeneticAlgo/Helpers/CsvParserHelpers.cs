using GeneticAlgo.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyCsvParser;

namespace GeneticAlgo.Helpers
{
    public class CsvParserHelpers
    {
        public List<NasaTreeModel> GetModelsFromCSV(string path)
        {
            CsvParserOptions csvParserOptions = new CsvParserOptions(true,';');
            CsvToModelMapping csvToModelMapping = new CsvToModelMapping();
            CsvParser<NasaTreeModel> csvParser = new CsvParser<NasaTreeModel>(csvParserOptions, csvToModelMapping);

            var result = csvParser.ReadFromFile(path, Encoding.ASCII).Select(x=>x.Result).ToList();

            return result;
        }
    }
}
