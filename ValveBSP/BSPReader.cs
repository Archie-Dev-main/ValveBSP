namespace ValveBSP;

public abstract class BSPReader<T>(LumpItemArchetype[] lumpArchetypes, LumpReader standardReader, Dictionary<int, LumpReader> exceptions)
{
    protected readonly LumpItemArchetype[] _lumpArchetypes = lumpArchetypes;
    protected readonly LumpReader _standardReader = standardReader;
    protected readonly Dictionary<int, LumpReader> _exceptions = exceptions;

    public abstract T Read(string name, Stream stream);
}
