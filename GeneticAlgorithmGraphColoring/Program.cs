// See https://aka.ms/new-console-template for more information
using GeneticAlgorithmGraphColoring;

Console.WriteLine("Hello, World!");

var dupa = "<10>";


Console.WriteLine(dupa.Split('>')[0].Substring(1));

var filePath = @"C:\\Users\\misio\\Desktop\\graph.txt.txt";

if(File.Exists(filePath))
{
    var graph = new Graph(filePath);

    Console.WriteLine(graph.QuantityOfEdges);

    Console.WriteLine(graph.PrintGraph());
    foreach(int vertex in graph.Vertexes)
    {
        Console.WriteLine(vertex);
    }
}



