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
                int[] genes = localChromosome.GetValues();
                int numberOfEdges = graph.QuantityOfEdges;
                bool hasAnyNeighborSameColor = false;
                /*Console.WriteLine("chromosom do ewaluacji");
                Console.WriteLine(string.Join("\n", localChromosome.GetValues())); */

               int fitness = 0;

                foreach (Edge edge in graph.Edges)
                {
                    var colorOfStartVertex = genes[edge.SourceVertex - 1];
                    var colorOEndVertex = genes[edge.EndVertex - 1];
                    hasAnyNeighborSameColor = hasAnyNeighborSameColor ||
                                              colorOfStartVertex == colorOEndVertex;
                    if (colorOfStartVertex == colorOEndVertex)
                    {
                        fitness++;
                    }
                    //Console.WriteLine("Liczba zlych kolorowan {0}", fitness);
                }

                if (hasAnyNeighborSameColor)
                {
                    return -(fitness+graph.Vertexes.Count);
                }
                //Console.WriteLine("liczba chromatyczna: {0}", genes.Distinct().Count());

                return -(genes.ToList().Distinct().Count());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return int.MinValue;
            }
        }
    }
}