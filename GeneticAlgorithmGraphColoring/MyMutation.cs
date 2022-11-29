using GeneticSharp;

namespace GeneticAlgorithmGraphColoring
{
    public class MyMutation : MutationBase, IMutation
    {
        private readonly IRandomization m_rnd;
        private readonly Graph graph;

        public MyMutation(Graph graph)
        {
            m_rnd = RandomizationProvider.Current;
            this.graph = graph;
        }
        protected override void PerformMutate(IChromosome chromosome, float probability)
        {
            try
            {
                MyChromosome myChromosome = (MyChromosome)chromosome;

                double rand = m_rnd.GetDouble();
                if (!(rand <= probability)) return;

                int[] genes = myChromosome.GetValues();

                //tu byl graf
                foreach (int vertex in graph.Vertexes)
                {
                    IList<int> p = graph.NeighborsList(vertex);
                    if (p.Any(z => genes[z - 1] == genes[vertex - 1]))
                        genes[vertex - 1] = m_rnd.GetInt(0, chromosome.Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}