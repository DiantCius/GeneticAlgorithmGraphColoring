using System.Text;

namespace GeneticAlgorithmGraphColoring
{
    public class ColoredGraph
    {
        public ColoredGraph(Graph graph, IDictionary<int, int> vertexesWithColor)
        {
            VertexesWithColor = vertexesWithColor;
        }
        public IDictionary<int, int> VertexesWithColor { get; }

        public string PrintGraphColors()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (KeyValuePair<int, int> item in VertexesWithColor)
            {
                stringBuilder.AppendLine($"Wierzchołek :{item.Key} - Kolor : {item.Value}");
            }
            return stringBuilder.ToString();
        }
    }
}