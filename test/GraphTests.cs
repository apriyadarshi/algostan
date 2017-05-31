using System;
using algostan;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;

namespace test
{
    [TestFixture]
    public class TestUGraph
    {
        private UGraph GetGraphObject()
        {
            List<Vertex> vertices = new List<Vertex>() { new Vertex(1, 1, new List<int>() { 2, 3, 4 }),
                                                            new Vertex(2, 2, new List<int>() { 1, 3, 4 }),
                                                            new Vertex(3, 3, new List<int>() { 1, 2, 4, 6 }),
                                                            new Vertex(4, 4, new List<int>() { 1, 2, 3, 5 }),
                                                            new Vertex(5, 5, new List<int>() { 4, 6, 7, 8 }),
                                                            new Vertex(6, 6, new List<int>() { 5, 3, 7, 8 }),
                                                            new Vertex(7, 7, new List<int>() { 5, 6, 8 }),
                                                            new Vertex(8, 8, new List<int>() { 5, 6, 7 })
            };

            return new UGraph(vertices);
        }

        [Test]
        public void TestGraphConstructor()
        {
            UGraph graph = GetGraphObject();

            List<Edge> correctEdgeList = new List<Edge>()
            {
                new Edge(graph.Vertices[1-1], graph.Vertices[2-1]),
                new Edge(graph.Vertices[1-1], graph.Vertices[3-1]),
                new Edge(graph.Vertices[1-1], graph.Vertices[4-1]),
                new Edge(graph.Vertices[2-1], graph.Vertices[3-1]),
                new Edge(graph.Vertices[2-1], graph.Vertices[4-1]),
                new Edge(graph.Vertices[4-1], graph.Vertices[3-1]),


                new Edge(graph.Vertices[4-1], graph.Vertices[5-1]),
                new Edge(graph.Vertices[3-1], graph.Vertices[6-1]),

                new Edge(graph.Vertices[5-1], graph.Vertices[6-1]),
                new Edge(graph.Vertices[5-1], graph.Vertices[8-1]),
                new Edge(graph.Vertices[6-1], graph.Vertices[7-1]),
                new Edge(graph.Vertices[5-1], graph.Vertices[7-1]),
                new Edge(graph.Vertices[6-1], graph.Vertices[8-1]),
                new Edge(graph.Vertices[7-1], graph.Vertices[8-1]),

            };

            bool areEdgesCreatedProperly = true; 
            if (graph.Edges.Count == 14)
            {
                foreach (Edge e in correctEdgeList)
                {
                    List<Edge> matchingEdgesInGraph = graph.Edges.FindAll(x => (x.Head == e.Head && x.Tail == e.Tail)
                                                                            || (x.Tail == e.Head && x.Head == e.Tail));
                    if (matchingEdgesInGraph.Count != 1)

                    {
                        areEdgesCreatedProperly = false;
                        break;
                    }
                }                
            }
            else
            {
                areEdgesCreatedProperly = false;
            }

            Assert.AreEqual(true, areEdgesCreatedProperly, graph.Edges.Count.ToString());
        } 

        [Test]
        public void TestGetCrossingEdgesForMinCut()
        {
            UGraph graph = GetGraphObject();
            LinkedList<Edge> crossingEdgesForMinCut = graph.GetCrossingEdgesForMinCut();

            bool result = false;
            if (crossingEdgesForMinCut.Count == 2)
                result = true;
            //else
            //{
            //    int e1HeadLbl = crossingEdgesForMinCut.First.Value.Head.Label;
            //    int e1TailLbl = crossingEdgesForMinCut.First.Value.Tail.Label;
            //    int e2HeadLbl = crossingEdgesForMinCut.Last.Value.Head.Label;
            //    int e2TailLbl = crossingEdgesForMinCut.Last.Value.Tail.Label;

                
            //    if (((e1HeadLbl == 4 && e1TailLbl == 5 || (e1HeadLbl == 5 && e1TailLbl == 4))
            //            && (e2HeadLbl == 3 && e2TailLbl == 6 || (e2HeadLbl == 6 && e2TailLbl == 3)))
            //        || ((e1HeadLbl == 3 && e1TailLbl == 6 || (e1HeadLbl == 6 && e1TailLbl == 3))
            //            && (e2HeadLbl == 4 && e2TailLbl == 5 || (e2HeadLbl == 5 && e2TailLbl == 4)))) 
            //        result = true;
            //}

            Assert.AreEqual(true, result, crossingEdgesForMinCut.Count.ToString());

        }
    }

    [TestFixture]
    public class TestContractibleGraph
    {
        private UGraph GetGraphObject()
        {
            List<Vertex> vertices = new List<Vertex>() { new Vertex(1, 1, new List<int>() { 2, 3, 4 }),
                                                            new Vertex(2, 2, new List<int>() { 1, 3, 4 }),
                                                            new Vertex(3, 3, new List<int>() { 1, 2, 4, 6 }),
                                                            new Vertex(4, 4, new List<int>() { 1, 2, 3, 5 }),
                                                            new Vertex(5, 5, new List<int>() { 4, 6, 7, 8 }),
                                                            new Vertex(6, 6, new List<int>() { 5, 3, 7, 8 }),
                                                            new Vertex(7, 7, new List<int>() { 5, 6, 8 }),
                                                            new Vertex(8, 8, new List<int>() { 5, 6, 7 })
            };

            return new UGraph(vertices);
        }

        [Test]
        public void TestAreSameSortedMergedVertex()
        {
            UGraph graph = GetGraphObject();
            ContractibleGraph cGraph = new ContractibleGraph(graph);

            LinkedList<Vertex> vertices1 = new LinkedList<Vertex>();
            vertices1.AddLast(new LinkedListNode<Vertex>(graph.Vertices[0]));
            vertices1.AddLast(new LinkedListNode<Vertex>(graph.Vertices[1]));
            vertices1.AddLast(new LinkedListNode<Vertex>(graph.Vertices[2]));

            LinkedList<Vertex> vertices2 = new LinkedList<Vertex>();
            vertices2.AddLast(new LinkedListNode<Vertex>(graph.Vertices[0]));
            vertices2.AddLast(new LinkedListNode<Vertex>(graph.Vertices[1]));

            Assert.AreEqual(false, cGraph.AreSameSortedMergedVertex(vertices1, vertices2));            
        }

        [Test]
        public void TestContractEdge()
        {
            UGraph graph = GetGraphObject();
            ContractibleGraph cGraph = new ContractibleGraph(graph);

            ContractibleEdge cEdge = new ContractibleEdge();
            cEdge.Head = new LinkedList<Vertex>();
            cEdge.Head.AddLast(graph.Vertices[0]);
            cEdge.Head.AddLast(graph.Vertices[2]);
            cEdge.Head.AddLast(graph.Vertices[4]);

            cEdge.Tail = new LinkedList<Vertex>();
            cEdge.Tail.AddLast(graph.Vertices[1]);
            cEdge.Tail.AddLast(graph.Vertices[3]);
            cEdge.Tail.AddLast(graph.Vertices[5]);

            bool result = true;
            LinkedList<Vertex> sortedMergedVertex = cGraph.GetSortedMergedVertex(cEdge);
            if (sortedMergedVertex.Count == 6)
            {
                int prevVertexLabel = sortedMergedVertex.First.Value.Label;
                foreach (Vertex v in sortedMergedVertex)
                {
                    if (v.Label < prevVertexLabel)
                    {
                        result = false;
                        break;
                    }
                    prevVertexLabel = v.Label;
                }
            }
            else
            {
                result = false;
            }

            Assert.AreEqual(true, result);
        }

        [Test]
        public void TestContractEdgeAndDeleteSelfLoops()
        {
            UGraph graph = GetGraphObject();
            ContractibleGraph cGraph = new ContractibleGraph(graph);

            Edge originalEdgeContracted = cGraph.ContractibleEdges.First.Value.OriginalEdge;

            //Test after 1 contraction
            cGraph.ContractEdgeAndDeleteSelfLoops(cGraph.ContractibleEdges.First.Value);

            

            bool result = true;
            if (cGraph.ContractibleEdges.Count != 13)
                result = false;
            else
            {
                int count = 0;
                foreach (ContractibleEdge ce in cGraph.ContractibleEdges)
                {
                    //fails if edge being contracted is still present
                    if (ce.OriginalEdge == originalEdgeContracted)
                    {
                        result = false;
                        break;
                    }
                    
                    //Check no self loops - Note that both head and tails are sorted
                    if (ce.Head.Count == ce.Tail.Count)
                    {
                        LinkedListNode<Vertex> tailCurrNode = ce.Tail.First, headCurrNode = ce.Head.First;
                        bool areEndsDiff = false;
                        for (int i=0; i<ce.Tail.Count; i++)
                        {
                            if (tailCurrNode.Value.Label != headCurrNode.Value.Label)
                            {
                                areEndsDiff = true;
                                break;
                            }
                        }
                        if (!areEndsDiff)
                        {
                            result = false;
                            break;
                        }
                    }

                    //Check for edges [(1,2),3],[(1,2),4], [1,2
                    //Check for (1,2),3 - 2 in no
                    
                    if ((ce.OriginalEdge.Head.Label == 1 && ce.OriginalEdge.Tail.Label == 3)
                        || (ce.OriginalEdge.Head.Label == 3 && ce.OriginalEdge.Tail.Label == 1)
                        || (ce.OriginalEdge.Head.Label == 3 && ce.OriginalEdge.Tail.Label == 2)
                        || (ce.OriginalEdge.Head.Label == 2 && ce.OriginalEdge.Tail.Label == 3))
                    {
                        if ((ce.Head.Count == 2
                                && ce.Head.Contains(cGraph.OriginalGraph.Vertices[0])
                                && ce.Head.Contains(cGraph.OriginalGraph.Vertices[1])
                                && ce.Tail.Count == 1
                                && ce.Tail.Contains(cGraph.OriginalGraph.Vertices[2]))
                            || (ce.Tail.Count == 2
                                && ce.Tail.Contains(cGraph.OriginalGraph.Vertices[0])
                                && ce.Tail.Contains(cGraph.OriginalGraph.Vertices[1])
                                && ce.Head.Count == 1
                                && ce.Head.Contains(cGraph.OriginalGraph.Vertices[2])))
                        {
                            count = count + 1;
                        }
                    }
                }
                if (result == false || (result == true && count != 2))
                    result = false;
            }

            Assert.AreEqual(true, result);

        }
    }
}
