using GeneticSharp;

namespace GeneticAlgorithmGraphColoring
{
    public class ColoringRequest
    {
        public ColoringRequest(int[] startValues, int population, int populationSize, Graph graph)
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

    public class ColoringResponse
    {
        public ColoringResponse(ColoredGraph coloredGraph)
        {
            ColoredGraph = coloredGraph;
            Success = true;
        }

        public bool Success { get; }
        public ColoredGraph ColoredGraph { get; }
    }
    public class ColorGraph
    {

        public ColoringResponse ColorGraphWithGeneticAlgorithm(ColoringRequest coloringRequest)
        {
            var graph = coloringRequest.Graph;

            var chromosome = new MyChromosome(coloringRequest.StartValues.Length, coloringRequest.StartValues, coloringRequest.Population);

            var population = new Population(coloringRequest.PopulationSize, coloringRequest.PopulationSize, chromosome);

            var selection = new EliteSelection();

            var mutation = new UniformMutation();

            var crossover = new OnePointCrossover();

            var fitness = new MyFitness(graph);

            var geneticAlgorithm = new GeneticAlgorithm(population, fitness, selection, crossover, mutation);

            geneticAlgorithm.Termination = new GenerationNumberTermination(10);

            int latestFitness = int.MinValue;

            int bestFitness = 0;

            MyChromosome? bestChromosome = null;

            geneticAlgorithm.GenerationRan += (s,e) =>
            {
                bestChromosome = (MyChromosome)geneticAlgorithm.BestChromosome;

                bestFitness = (int)-bestChromosome.Fitness.Value;

                if (bestFitness == latestFitness) return;

                Console.WriteLine("wartość funkcji fitness:{0}", bestFitness);

                latestFitness = bestFitness;

            };
            geneticAlgorithm.Start();

            Console.WriteLine("Koniec algorytmu");

            if (bestFitness > graph.Vertexes.Count) Console.WriteLine("fitness>liczba wierzchołków");

            int[] genotype = bestChromosome.GetValues();

            var coloredVertex = new Dictionary<int, int>(graph.Vertexes.Count);

            foreach (int vertex in graph.Vertexes)
            {
                coloredVertex.Add(vertex, genotype[vertex - 1]);
                Console.WriteLine(genotype[vertex - 1]);
            }

            var coloredGraph = new ColoredGraph(graph, coloredVertex);

            var response = new ColoringResponse(coloredGraph);

            return response;
        }
    }
}