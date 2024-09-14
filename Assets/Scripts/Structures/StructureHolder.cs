using System.Collections.Generic;

public class StructureHolder : IPausable
{
    private readonly HashSet<Structure> _structures = new HashSet<Structure>();

    public void AddStructure(Structure structure)
    {
        _structures.Add(structure);
    }

    public void RemoveStructure(Structure structure)
    {
        _structures.Remove(structure);
    }

    public HashSet<Structure> GetAllStructures()
    {
        return _structures;
    }

    public void Pause()
    {
        foreach (var structure in _structures)
        {
            structure.Pause();
        }
    }

    public void Resume()
    {
        foreach (var structure in _structures)
        {
            structure.Resume();
        }
    }
}
