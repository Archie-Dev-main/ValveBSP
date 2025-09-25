namespace ValveBSP;

public enum ByteUnits
{
    Bytes = 1,
    Kilobytes = 1024,
    Megabytes = 1_048_576,
    Gigabytes = 1_073_741_824
}

public abstract class BSP
{
    public abstract string Info(ByteUnits unitType = ByteUnits.Bytes);
}
