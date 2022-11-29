using GeneticSharp;

namespace GeneticAlgorithmGraphColoring
{
    public class GraphColoringRequest
    {
        public GraphColoringRequest(int[] startValues, int population, int populationSize, Graph graph)
        {
            StartValues = startValues;
            Population = population;
            PopulationSize = populationSize;
            Graph = graph;
        }

        public int[] StartValues { get; }
        public int Population { get; }
        public int PopulationSize { get; }
        public Graph Graph { get; }
    }

    public class GraphColoringResponse
    {
        public GraphColoringResponse(ColoredGraph coloredGraph)
        {
            ColoredGraph = coloredGraph;
            Success = true;
        }

        public bool Success { get; }
        public ColoredGraph ColoredGraph { get; }
    }
    public class ColorGraph
    {

        public GraphColoringResponse ColorGraphWithGeneticAlgorithm(GraphColoringRequest graphColoringRequest)
        {
            Graph graph = graphColoringRequest.Graph;

            MyChromosome chromosome = new MyChromosome(graphColoringRequest.StartValues.Length, graphColoringRequest.StartValues, graphColoringRequest.Population);

            Population population = new Population(graphColoringRequest.PopulationSize, graphColoringRequest.PopulationSize, chromosome);

            MyFitness fitness = new MyFitness(graph);

            EliteSelection selection = new EliteSelection();

            OnePointCrossover crossover = new OnePointCrossover();

            MyMutation mutation = new MyMutation(graph);

            //ilosc generacji
            FitnessStagnationTermination termination = new FitnessStagnationTermination(10);

            GeneticAlgorithm ga = new GeneticAlgorithm(population, fitness, selection, crossover, mutation)
            { Termination = termination, MutationProbability = 0.1f };
            int latestFitness = int.MinValue;
            int bestFitness = 0;
            MyChromosome? bestChromosome = null;

            ga.GenerationRan += (sender, e) =>
            {
                bestChromosome = (MyChromosome)ga.BestChromosome;
                bestFitness = (int)-bestChromosome.Fitness.Value;

                if (bestFitness == latestFitness) return;
                latestFitness = bestFitness;
                bestChromosome.GetValues();
                Console.WriteLine("fitness: {0}", bestFitness);
            };
            ga.TerminationReached += (sender, e) => { Console.WriteLine("Koniec generacji"); };
            ga.Start();

            Console.WriteLine("Koniec");

            if (bestFitness > graph.Vertexes.Count) Console.WriteLine("fitness>ilosc wierzchołków");//return new GraphColoringResponse();

            int[] resultGenType = bestChromosome.GetValues();

            IDictionary<int, int> coloredVertex = new Dictionary<int, int>(graph.Vertexes.Count);

            foreach (int vertex in graph.Vertexes)
            {
                coloredVertex.Add(vertex, resultGenType[vertex - 1]);
            }
            ColoredGraph coloredGraph = new ColoredGraph(graph, coloredVertex);

            var response = new GraphColoringResponse(coloredGraph);

            return response;
        }
    }
}