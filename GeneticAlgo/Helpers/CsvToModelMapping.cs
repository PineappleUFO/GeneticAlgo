using GeneticAlgo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyCsvParser.Mapping;

namespace GeneticAlgo.Helpers
{
    public class CsvToModelMapping:CsvMapping<NasaTreeModel>
    {
        public CsvToModelMapping() :base()
        {
            MapProperty(1, x => x.L);
            MapProperty(2, x => x.Ef);
        }
    }
}
