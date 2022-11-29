using GeneticSharp;

namespace GeneticAlgorithmGraphColoring
{
    public class MyFitness : IFitness
    {

        private readonly Graph graph;

        public MyFitness(Graph graph)
        {
            this.graph = graph;
        }

        public double Evaluate(IChromosome chromosome)
        {
            try
            {
                MyChromosome localChromosome = (MyChromosome)chromosome;

                if (localChromosome == null) throw new InvalidProgramException("Chromosome cannot be null");
                int[] chromosomeValues = localChromosome.GetValues();

                //tu byl graf
                bool hasAnyNeighborSameColor = false;
                double countOfBadColoring = 0;
                foreach (int vertex in graph.Vertexes)
                {
                    IList<int> neighbors = graph.NeighborsList(vertex).ToList();
                    int colorOfCurrentVertex = chromosomeValues[vertex - 1];
                    hasAnyNeighborSameColor = hasAnyNeighborSameColor ||
                                              neighbors.Any(x => chromosomeValues[x - 1] == colorOfCurrentVertex);
                    countOfBadColoring += (double)(neighbors.Count(x => chromosomeValues[x - 1] == colorOfCurrentVertex) - 1) /
                                          neighbors.Count;
                }

                if (hasAnyNeighborSameColor)
                {
                    return -(countOfBadColoring + graph.Vertexes.Count + 1);
                }

                return -(chromosomeValues.ToList().Distinct().Count());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return int.MinValue;
            }
        }
    }
}