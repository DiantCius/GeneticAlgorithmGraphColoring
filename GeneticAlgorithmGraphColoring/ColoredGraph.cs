using System.Text;

namespace GeneticAlgorithmGraphColoring
{
    public class ColoredGraph
    {
        public ColoredGraph(Graph graph, Dictionary<int, int> vertexesWithColor)
        {
            VertexesWithColor = vertexesWithColor;
        }
        public Dictionary<int, int> VertexesWithColor { get; }

        public string PrintGraphColors()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (KeyValuePair<int, int> vertex in VertexesWithColor)
            {
                stringBuilder.AppendLine($"Wierzchołek : {vertex.Key} - Kolor : {vertex.Value}");
            }
            return stringBuilder.ToString();
        }
    }
}