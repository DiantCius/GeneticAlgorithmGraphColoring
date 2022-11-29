using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace GeneticAlgorithmGraphColoring
{
    public class Graph
    {
        public Graph(string filePath)
        {
            var fileContent = File.ReadAllText(filePath);

            var fileLines = fileContent.Split("\n", StringSplitOptions.RemoveEmptyEntries);

            QuantityOfEdges = int.Parse(fileLines[0].Split('>')[0].Substring(1));

            string[] separatingStrings = { "<", ">" };

            List<Edge> edges = new List<Edge>();
            for (int i = 1; i < fileLines.Length; i++)
            {
                var edgeLine = fileLines[i].Split("\n");

                var edgeParameters = edgeLine[0].Split(separatingStrings, StringSplitOptions.RemoveEmptyEntries);

                var edge = new Edge(int.Parse(edgeParameters[0]), int.Parse(edgeParameters[1]), int.Parse(edgeParameters[2]));

                edges.Add(edge);
            }

            Edges = edges;
            Vertexes = edges.Select(x => x.SourceVertex).Union(edges.Select(y => y.TargetVertex)).Distinct().ToList();
        }

        public int QuantityOfEdges { get; }
        public List<Edge> Edges { get; }
        public List<int> Vertexes { get; }

        public IList<int> NeighborsList(int vertex)
        {
            IEnumerable<int> vertexFromStarting = Edges.Where(x => x.SourceVertex == vertex).Select(x => x.TargetVertex);
            IEnumerable<int> vertexFromEnding = Edges.Where(x => x.TargetVertex == vertex).Select(x => x.SourceVertex);
            return vertexFromEnding.Union(vertexFromStarting).Distinct().ToList();
        }

        public string PrintGraph() => Edges.Aggregate($"<{QuantityOfEdges}>", (current, edge) => $"{current}{edge.PrintEdgeInOriginalForm}");
    }
}
