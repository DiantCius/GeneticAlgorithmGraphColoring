using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmGraphColoring
{
    public class Edge
    {
        public Edge(int vertexNumber, int sourceVertex, int targetVertex)
        {
            VertexNumber = vertexNumber;
            SourceVertex = sourceVertex;
            TargetVertex = targetVertex;
        }
        public int VertexNumber { get; }
        public int SourceVertex { get; }
        public int TargetVertex { get; }

        public string PrintEdgeInOriginalForm => $"\r\n<{VertexNumber}><{SourceVertex}><{TargetVertex}>";
    }
}
