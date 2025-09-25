namespace ValveBSP;

public class DataDescription
{
    private Type _type = typeof(Type);
    private readonly List<LumpItemField> _fields = [];

    public DataDescription Begin(Type type)
    {
        _type = type;
        return this;
    }

    public DataDescription Add(LumpItemField field)
    {
        _fields.Add(field);
        return this;
    }

    public LumpItemArchetype End()
    {
        LumpItemArchetype archetype = new(_type, [.. _fields]);

        _fields.Clear();

        return archetype;
    }
}
