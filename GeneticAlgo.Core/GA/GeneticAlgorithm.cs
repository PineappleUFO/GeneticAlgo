using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgo.Core.GA
{
    public class GeneticAlgorithm<T>
    {
        public List<DNA<T>> Population { get; private set; }
        public int Generation { get; private set; }

        public int Elitism { get; set; }
        public float BestFitness { get; private set; }
        public T[] BestGenes { get; private set; }

        public float MutationRate { get; set; }

        private Random random;
        private float fitnessSum;

        List<DNA<T>> newPopulation;

        public GeneticAlgorithm(int populationSize, int dnaSize, Random random, Func<T> getRandomGene, Func<int, float> fitnessFunction, int Elitism, float mutationRate = 0.01f)
        {
            Generation = 1;
            this.Elitism = Elitism;
            MutationRate = mutationRate;
            Population = new List<DNA<T>>(populationSize);
            this.random = random;
            newPopulation = new List<DNA<T>>(populationSize);
            BestGenes = new T[dnaSize];

            for (int i = 0; i < populationSize; i++)
            {
                Population.Add(new DNA<T>(dnaSize, random, getRandomGene, fitnessFunction, true));
            }
        }

        public void NewGeneration()
        {
            if (Population.Count <= 0)
                return;

            CalculateFitness();
            Population.Sort(CompareDNA);

            newPopulation.Clear();

            for (int i = 0; i < Population.Count; i++)
            {
                if (i < Elitism)
                {
                    newPopulation.Add(Population[i]);
                }
                else
                {
                    DNA<T> parent1 = ChooseParent();
                    DNA<T> parent2 = ChooseParent();

                    if (parent1 == null || parent2 == null)
                        continue;

                    DNA<T> child = parent1.Crossover(parent2);

                    child.Mutate(MutationRate);

                    newPopulation.Add(child);
                }


            }

            List<DNA<T>> tmpList = Population;
            Population = newPopulation;
            newPopulation = tmpList;
            Generation++;

        }

        public int CompareDNA(DNA<T> a, DNA<T> b)
        {
            if (a.Fitness > b.Fitness)
            {
                return -1;
            }
            else if (a.Fitness < b.Fitness)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        private void CalculateFitness()
        {
            fitnessSum = 0;
            DNA<T> best = Population[0];

            for (int i = 0; i < Population.Count; i++)
            {
                fitnessSum += Population[i].CalculateFitness(i);

                if (Population[i].Fitness > best.Fitness)
                {
                    best = Population[i];
                }

            }

            BestFitness = best.Fitness;
            best.Genes.CopyTo(BestGenes, 0);

        }

        private DNA<T> ChooseParent()
        {
            double randomNumber = random.NextDouble() * fitnessSum;

            for (int i = 0; i < Population.Count; i++)
            {
                if (randomNumber < Population[i].Fitness)
                {
                    return Population[i];
                }
                randomNumber -= Population[i].Fitness;
            }

            return null;
        }
    }
}
