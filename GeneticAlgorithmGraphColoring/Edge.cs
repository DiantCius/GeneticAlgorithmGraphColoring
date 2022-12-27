using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmGraphColoring
{
    public class Edge
    {
        public Edge(int vertexNumber, int sourceVertex, int endVertex)
        {
            VertexNumber = vertexNumber;
            SourceVertex = sourceVertex;
            EndVertex = endVertex;
        }
        public int VertexNumber { get; }
        public int SourceVertex { get; }
        public int EndVertex { get; }

        public string PrintEdge => $"\r\n<{VertexNumber}><{SourceVertex}><{EndVertex}>";
    }
}
