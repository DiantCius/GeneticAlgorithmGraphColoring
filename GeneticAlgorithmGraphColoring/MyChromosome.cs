using GeneticSharp;

namespace GeneticAlgorithmGraphColoring
{
    public class MyChromosome : ChromosomeBase
    {
        private readonly int[] gene_values;
        private readonly int length;
        private int population;

        public MyChromosome(int length, int[] geneValues, int population) : base(length)
        {
            this.length = length;
            this.gene_values = geneValues;
            this.population = population;
            CreateGenes();
        }

        private int[] RandomizeGenes(int[] geneValues)
        {
            Random rnd = new Random();
            for (int i = 0; i < geneValues.Length; i++)
            {
                int vertexColor = rnd.Next(0, geneValues.Length);
                geneValues[i] = vertexColor;
            }
            return geneValues;
        }

        public override Gene GenerateGene(int geneIndex)
        {
            return new Gene(gene_values[geneIndex]);
        }

        public override IChromosome CreateNew()
        {
            int[] geneValues;
            if (population > 0)
            {
                geneValues = (int[])RandomizeGenes(gene_values).Clone();
                population--;
            }
            else
            {
                geneValues = (int[])gene_values.Clone();
            }
            return new MyChromosome(length, geneValues, population);
        }

        public int[] GetValues()
        {
            return gene_values;
        }
    }

}