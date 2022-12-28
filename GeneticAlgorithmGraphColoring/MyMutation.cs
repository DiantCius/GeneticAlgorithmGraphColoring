
using GeneticAlgorithmGraphColoring;
using System.ComponentModel;
using System.Linq;

namespace GeneticSharp
{
    public class MyMutation : MutationBase
    {


        private readonly Graph _graph;


        public MyMutation(Graph graph)
        {
            _graph = graph;
        }

        protected override void PerformMutate(IChromosome chromosome, float probability)
        {
            var myChromosome = chromosome as MyChromosome;
            /*Console.WriteLine("przed mutacja:");
            Console.WriteLine(string.Join("\n", myChromosome.GetValues()));*/
            int length = myChromosome.Length;
            int[] genes = myChromosome.GetValues();
            foreach(int vertex in _graph.Vertexes)
            {
                genes[vertex-1]= RandomizationProvider.Current.GetInt( 0, length);
            }
            /*Console.WriteLine("po mutacja:");
            Console.WriteLine(string.Join("\n", myChromosome.GetValues()));*/

        }

   
    }
}
