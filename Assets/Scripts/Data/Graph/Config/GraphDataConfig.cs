using System;
using System.Collections.Generic;

using Mamont.Data.Graph.Builder;

namespace Mamont.Data.Graph.Config
{
	[Serializable]
	public class VertexPosition
	{
		public float X;
		public float Y;
		public float Z;
	}
	[Serializable]
	public class EdgeData
	{
		public int NameIndex;
		public int Vertex1;
		public int Vertex2;
		public int Weight;
	}
	[Serializable]
	public class VertexData
	{
		public int NameIndex;
		public VertexPosition Position;
		public GraphVertexType VertexType;
		public float Value;

	}
	[Serializable]
	public class GraphDataConfig
	{
		public List<EdgeData> EdgeData = new();
		public List<VertexData> VertexData = new();
	}
}
