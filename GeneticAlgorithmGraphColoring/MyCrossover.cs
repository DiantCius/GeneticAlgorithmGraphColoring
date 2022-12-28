
using GeneticAlgorithmGraphColoring;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace GeneticSharp
{
    [DisplayName("One-Point")]
    public class MyCrossover : CrossoverBase
    {
        public int SwapPointIndex { get; set; }

        public MyCrossover(int swapPointIndex)
            : base(2, 2)
        {
            SwapPointIndex = swapPointIndex;
        }

        public MyCrossover()
            : this(0)
        {
        }

        protected override IList<IChromosome> PerformCross(IList<IChromosome> parents)
        {
            var firstParent = parents[0] as MyChromosome;
            var secondParent = parents[1] as MyChromosome;
            /*Console.WriteLine("parents:");
            Console.WriteLine(string.Join("\n", firstParent.GetValues()));
            Console.WriteLine("drugi:");
            Console.WriteLine(string.Join("\n", secondParent.GetValues()));*/
            int num = firstParent.Length - 1;
            if (SwapPointIndex >= num)
            {
                throw new ArgumentOutOfRangeException("parents", "The swap point index is {0}, but there is only {1} genes. The swap should result at least one gene to each side.".With(SwapPointIndex, firstParent.Length));
            }
            var children = CreateChildren(firstParent, secondParent);
            /*Console.WriteLine("po krzyzowaniu");
            foreach (MyChromosome child in children)
            {
                Console.WriteLine("////");
                Console.WriteLine(string.Join("\n", child.GetValues()));
                Console.WriteLine("////");
            }*/
            return children;
        }

        protected IList<IChromosome> CreateChildren(IChromosome firstParent, IChromosome secondParent)
        {
            IChromosome item = CreateChild(firstParent, secondParent);
            IChromosome item2 = CreateChild(secondParent, firstParent);
            return new List<IChromosome> { item, item2 };
        }

        protected virtual IChromosome CreateChild(IChromosome leftParent, IChromosome rightParent)
        {
            int num = SwapPointIndex + 1;
            IChromosome chromosome = leftParent.CreateNew();
            chromosome.ReplaceGenes(0, leftParent.GetGenes().Take(num).ToArray());
            chromosome.ReplaceGenes(num, rightParent.GetGenes().Skip(num).ToArray());
            return chromosome;
        }
    }
}
