// See https://aka.ms/new-console-template for more information
using GeneticAlgorithmGraphColoring;

Console.WriteLine("Hello, World!");


var filePath = @"C:\\Users\\misio\\Desktop\\graph.txt.txt";

if (File.Exists(filePath))
{
    var graph = new Graph(filePath);

    var startValues = graph.Vertexes.ToArray();

    var request = new ColoringRequest(startValues, 50, 50, graph);

    var coloringService = new ColorGraph();

    var response = coloringService.ColorGraphWithGeneticAlgorithm(request);

    var coloredGraph = response.ColoredGraph;

    Console.WriteLine(coloredGraph.PrintGraphColors());
    Console.WriteLine(graph.PrintGraph());
}



