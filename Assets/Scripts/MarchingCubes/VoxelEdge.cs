public struct VoxelEdge
{
	public VoxelVertex Start { get; set; }
	public VoxelVertex End { get; set; }

	public VoxelEdge(VoxelVertex start, VoxelVertex end)
	{
		Start = start;
		End = end;
	}
}
