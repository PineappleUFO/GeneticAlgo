using GeneticAlgo.Core.GA;
using GeneticAlgo.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GeneticAlgo
{

    public partial class MainWindow : Window
    {
        private string testString = "To be, or not to be, that is the question.";
        string validCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ,.|!#$%&/()=? ";
        int populationSize = 200;
        float mutationRate = 0.01f;
        int elitism =20;

        private GeneticAlgorithm<char> ga;
        private Random random;

        public MainWindow()
        {
            InitializeComponent();

            CsvParserHelpers csvParserHelpers = new CsvParserHelpers();
            var result = csvParserHelpers.GetModelsFromCSV(@"D:\Labs\!7 семестр\Эволюция\Курсач\Data.csv");
            random = new Random();
            ga = new GeneticAlgorithm<char>(populationSize, testString.Length, random, GetRandomGen, FitnessFunction,elitism,mutationRate);

            while (ga.BestFitness != 1)
            {
                ga.NewGeneration();
                txtBestGenes.Text = new string(ga.BestGenes);
                txtBestFitness.Text = ga.BestFitness.ToString();
                txtGeneration.Text = ga.Generation.ToString();
                txtPopulation.Text = ga.Population.Count.ToString();


                Debug.WriteLine(ga.BestFitness);
            }

        }

        private char GetRandomGen()
        {
            int i = random.Next(validCharacters.Length);
            return validCharacters[i];
        }

        private float FitnessFunction(int index)
        {
            float score = 0;
            DNA<char> dna = ga.Population[index];

            for (int i = 0; i < dna.Genes.Length; i++)
            {
                if (dna.Genes[i] == testString[i])
                {
                    score += 1;
                }
            }

            score /= testString.Length;
            return score;
        }
    }
}
