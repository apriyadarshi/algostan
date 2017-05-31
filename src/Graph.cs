using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace algostan
{
    public class Vertex
    {
        public int Label { get; }
        public Object Data { get; set; }
        public List<int> AdjacentVertices { get; set; } //List of labels of adjacent vertices

        public Vertex(int pLabel, Object pData, List<int> pAdjacentVertices)
        {
            Data = pData;
            Label = pLabel;
            AdjacentVertices = pAdjacentVertices;
        }
    }

    public class Edge
    {
        public Vertex Head { get; }
        public Vertex Tail { get; }
        public Edge(Vertex pHead, Vertex pTail)
        {
            Head = pHead;
            Tail = pTail;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[").Append(Head.Label).Append("]->[").Append(Tail.Label).Append("]");
            return sb.ToString();
        }
    }

    public class ContractibleEdge
    {
        public Edge OriginalEdge { get; set; }
        public LinkedList<Vertex> Head { get; set; }
        public LinkedList<Vertex> Tail { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            foreach (Vertex v in Head)
            {
                sb.Append(v.Label).Append(',');
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append("]->[");
            foreach (Vertex v in Tail)
            {
                sb.Append(v.Label).Append(',');
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append("]");
            return sb.ToString();
        }
    }

    public class UGraph
    {
        //Arraylist used as faster access by label is needed.
        //Vertex Label i is at index i - Better way is to implement via Hash.
        public List<Vertex> Vertices { get; }

        //LinkedList as many additions and deletions are used.
        public List<Edge> Edges { get; }

        //Populates vertices and edges. This constructor can't give parallel edges
        public UGraph(List<Vertex> pVertices)
        {
            Vertices = pVertices;
            Edges = new List<Edge>();
            if (pVertices != null)
            {
                List<int> addedVertices = new List<int>();
                for (int i = 0; i < pVertices.Count; i++)
                {
                    Vertex v = pVertices[i];
                    for (int j = 0; j < v.AdjacentVertices.Count; j++)
                    {
                        int currAdjVertex = v.AdjacentVertices[j];
                        if (!(addedVertices.Contains(currAdjVertex)))
                        {
                            Edge edge = new Edge(v, GetVertexByLabel(v.AdjacentVertices[j]));
                            if (edge.Tail != null)
                                Edges.Add(edge);
                        }
                    }
                    addedVertices.Add(v.Label);
                }
            }
        }

        private Vertex GetVertexByLabel(int pLabel)
        {
            return Vertices[pLabel - 1];
        }

        public LinkedList<Edge> GetCrossingEdgesForMinCut()
        {
            int minCutCrossingEdgeCount = -1;
            LinkedList<Edge> minCutCrossingEdges = null; 
            long noOfTrials = Vertices.Count * Vertices.Count; //n^2

            for (long trialCount = 0; trialCount < noOfTrials; trialCount++)
            {
                ContractibleGraph cGraph = new ContractibleGraph(this);

                int unMergedVertices = Vertices.Count;
                while (unMergedVertices > 2)
                {
                    ContractibleEdge ce = cGraph.GetEdgeToContractUniformlyAtRandom();
                    cGraph.ContractEdgeAndDeleteSelfLoops(ce);
                    unMergedVertices--;
                }

                if (minCutCrossingEdgeCount == -1 || cGraph.ContractibleEdges.Count < minCutCrossingEdgeCount)
                {
                    minCutCrossingEdgeCount = cGraph.ContractibleEdges.Count;
                    minCutCrossingEdges = new LinkedList<Edge>();
                    for (int i = 0; i < minCutCrossingEdgeCount; i++)
                    {
                        minCutCrossingEdges.AddLast(cGraph.ContractibleEdges.First().OriginalEdge);
                        cGraph.ContractibleEdges.RemoveFirst();
                    }
                }
            }
            return minCutCrossingEdges;
        }
    }

    public class ContractibleGraph
    {
        public LinkedList<ContractibleEdge> ContractibleEdges { get; }
        public UGraph OriginalGraph { get; }

        public ContractibleGraph(UGraph pOriginalGraph)
        {
            OriginalGraph = pOriginalGraph;
            ContractibleEdges = GetContractibleEdgesForGraph();
        }

        public void ContractEdgeAndDeleteSelfLoops(ContractibleEdge pCe)
        {
            //Sorted for faster comparison later while replacing component vertices with merged ones
            LinkedList<Vertex> sortedMergedVertex = GetSortedMergedVertex(pCe);

            LinkedListNode<ContractibleEdge> currNode = ContractibleEdges.First;
            while (currNode != null)
            {
                LinkedListNode<ContractibleEdge> nextNode = currNode.Next;
                if (currNode.Value == pCe)
                {
                    ContractibleEdges.Remove(currNode);
                    currNode = nextNode;
                    continue;
                }

                //Replace component vertices at all places with merged vertex
                if (AreSameSortedMergedVertex(pCe.Head, currNode.Value.Head)
                    || AreSameSortedMergedVertex(pCe.Tail, currNode.Value.Head))
                    currNode.Value.Head = sortedMergedVertex;
                if (AreSameSortedMergedVertex(pCe.Tail, currNode.Value.Tail)
                    || AreSameSortedMergedVertex(pCe.Head, currNode.Value.Tail))
                    currNode.Value.Tail = sortedMergedVertex;

                //Delete Self Loops
                if (AreSameSortedMergedVertex(currNode.Value.Head, currNode.Value.Tail))
                {
                    ContractibleEdges.Remove(currNode);
                }

                currNode = nextNode;
            }
        }

        public LinkedList<Vertex> GetSortedMergedVertex(ContractibleEdge pCe)
        {
            LinkedList<Vertex> sortedMergedList = new LinkedList<Vertex>();
            LinkedListNode<Vertex> headCurrNode = pCe.Head.First, tailCurrNode = pCe.Tail.First;
            for (int i = 0; i < pCe.Head.Count + pCe.Tail.Count; i++)
            {
                if (headCurrNode != null && tailCurrNode != null)
                {
                    if (headCurrNode.Value.Label <= tailCurrNode.Value.Label)
                    {
                        sortedMergedList.AddLast(new LinkedListNode<Vertex>(headCurrNode.Value));
                        headCurrNode = headCurrNode.Next;
                    }
                    else
                    {
                        sortedMergedList.AddLast(new LinkedListNode<Vertex>(tailCurrNode.Value));
                        tailCurrNode = tailCurrNode.Next;
                    }
                }
                if (headCurrNode == null && tailCurrNode != null)
                {
                    sortedMergedList.AddLast(new LinkedListNode<Vertex>(tailCurrNode.Value));
                    tailCurrNode = tailCurrNode.Next;
                }
                if (tailCurrNode == null && headCurrNode != null)
                {
                    sortedMergedList.AddLast(new LinkedListNode<Vertex>(headCurrNode.Value));
                    headCurrNode = headCurrNode.Next;
                }
            }
            return sortedMergedList;
        }

        //Sorting completes comparison in 1 pass
        public bool AreSameSortedMergedVertex(LinkedList<Vertex> v1, LinkedList<Vertex> v2)
        {
            if ((v1 == null && v2 == null) || v1.Count == 0 && v2.Count ==0)
                return true;
            else
            {
                if (v1.Count == v2.Count)
                {
                    LinkedListNode<Vertex> currV2Node = v2.First;
                    foreach (Vertex v in v1)
                    {
                        if (!(currV2Node.Value == v)) //References compared
                            return false;
                        currV2Node = currV2Node.Next;
                    }
                    return true;
                }
                return false;
            }
        }

        public ContractibleEdge GetEdgeToContractUniformlyAtRandom()
        {
            Random rand = new Random();
            int randomIndex = rand.Next(0, ContractibleEdges.Count - 1);

            LinkedListNode<ContractibleEdge> currNode = ContractibleEdges.First;
            while (randomIndex > 0)
            {
                currNode = currNode.Next;
                randomIndex--;
            }
            return currNode.Value;
        }

        private LinkedList<ContractibleEdge> GetContractibleEdgesForGraph()
        {
            LinkedList<ContractibleEdge> result = new LinkedList<ContractibleEdge>();
            foreach (Edge e in OriginalGraph.Edges)
            {
                ContractibleEdge cEdge = new ContractibleEdge();

                cEdge.OriginalEdge = e;

                cEdge.Head = new LinkedList<Vertex>();
                cEdge.Head.AddLast(e.Head);

                cEdge.Tail = new LinkedList<Vertex>();
                cEdge.Tail.AddLast(e.Tail);

                result.AddLast(cEdge);
            }
            return result;
        }
    }
}