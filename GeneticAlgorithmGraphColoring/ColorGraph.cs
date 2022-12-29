using GeneticSharp;

namespace GeneticAlgorithmGraphColoring
{
    public class ColoringRequest
    {
        public ColoringRequest(int[] startValues, int populationSize, Graph graph)
        {
            StartValues = startValues;
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

            var chromosome = new MyChromosome(coloringRequest.StartValues.Length, coloringRequest.StartValues, coloringRequest.PopulationSize);

            var population = new Population(coloringRequest.PopulationSize, coloringRequest.PopulationSize, chromosome);

            var selection = new EliteSelection();

            //var mutation = new UniformMutation(true);

            var crossover = new OnePointCrossover(0);

            var mutation = new MyMutation(graph);

            //var crossover = new MyCrossover(0);

            var fitness = new MyFitness(graph);

            var geneticAlgorithm = new GeneticAlgorithm(population, fitness, selection, crossover, mutation);
            geneticAlgorithm.MutationProbability = 0.1f;
            geneticAlgorithm.CrossoverProbability = 0.75f;


            //geneticAlgorithm.Termination = new GenerationNumberTermination(100000000);
            //geneticAlgorithm.Termination = new FitnessStagnationTermination(50);
            geneticAlgorithm.Termination = new FitnessThresholdTermination(-4);


            //geneticAlgorithm.CrossoverProbability = 0.8f;

            int latestFitness = int.MinValue;

            int bestFitness = 0;

            MyChromosome? bestChromosome = null;

            geneticAlgorithm.GenerationRan += (s, e) =>
            {
                bestChromosome = (MyChromosome)geneticAlgorithm.BestChromosome;

                bestFitness = (int)-bestChromosome.Fitness.Value;

                if (bestFitness == latestFitness) return;

                Console.WriteLine("wartość funkcji fitness:{0}", bestFitness);

                latestFitness = bestFitness;

            };
            geneticAlgorithm.TerminationReached += (sender, e) => { Console.WriteLine("This is the end of generations"); };
            geneticAlgorithm.Start();
            Console.WriteLine("ilosc generacji pod koniec {0}", geneticAlgorithm.GenerationsNumber);

            Console.WriteLine("czas {0}", geneticAlgorithm.TimeEvolving.TotalMilliseconds);

            Console.WriteLine("Koniec algorytmu");

            if (bestFitness > graph.Vertexes.Count) Console.WriteLine("fitness>liczba wierzchołków");

            //var best = geneticAlgorithm.BestChromosome as MyChromosome;
            //int[] genotype = best.GetValues();
            int[] genotype = bestChromosome.GetValues();

            //Console.WriteLine("najlepszy fitness: {0}", best.Fitness);

            Console.WriteLine("genotyp dlugosc:{0}", genotype.Distinct().Count());

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