// See https://aka.ms/new-console-template for more information
using GeneticAlgorithmGraphColoring;

Console.WriteLine("Hello, World!");

//var dupa = "<10>";


//Console.WriteLine(dupa.Split('>')[0].Substring(1));

var filePath = "/home/kiciakocia/Desktop/graph.txt";

if (File.Exists(filePath))
{
    var graph = new Graph(filePath);

    var startValues = graph.Vertexes.ToArray();

    GraphColoringRequest request = new GraphColoringRequest(startValues, 100, 200, graph);

    var coloringService = new ColorGraph();

    var response = coloringService.ColorGraphWithGeneticAlgorithm(request);

    var coloredGraph = response.ColoredGraph;

    Console.WriteLine(coloredGraph.PrintGraphColors());

    // Console.WriteLine(graph.QuantityOfEdges);

    // Console.WriteLine(graph.PrintGraph());
    // foreach (int vertex in graph.Vertexes)
    // {
    //     Console.WriteLine(vertex);
    // }
}



