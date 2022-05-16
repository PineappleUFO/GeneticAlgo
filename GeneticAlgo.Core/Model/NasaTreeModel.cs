using System.Collections.Generic;

namespace GeneticAlgo.Core.Model
{
    public class NasaTreeModel
    {
        //L - в килостроках
        public double L { get; set; }
        //Ef - Стоимость в человеко-месяцах
        public double Ef { get; set; }
        public double L_Test { get; set; }
        public double Ef_Test { get; set; }

        public List<NasaTreeModel> Childrens { get; set; } = new List<NasaTreeModel>();
    }
}
