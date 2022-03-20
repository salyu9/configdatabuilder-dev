using Untitled.ConfigDataBuilder.Editor;

namespace ConfigDataBuilderDev.Editor
{
    [FlagHandler("server-only")]
    public class ServerOnlyFlagHandler : IFlagHandler
    {
        public void HandleColumn(ColumnInfo columnInfo)
        {
            columnInfo.IsIgnored = true;
        }
    }
}
