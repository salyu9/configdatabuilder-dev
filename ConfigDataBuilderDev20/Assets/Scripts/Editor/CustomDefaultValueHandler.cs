using System;
using System.Numerics;
using Untitled.ConfigDataBuilder.Editor;
using Vector3 = UnityEngine.Vector3;

namespace ConfigDataBuilderDev.Editor
{
    [FlagHandler("dir-default")]
    public class Vector3DefaultValueHandler : IFlagHandlerWithArgument
    {
        public void HandleColumn(ColumnInfo columnInfo, string arg)
        {
            // Set DefaultValue will override RawDefaultValue set by 'default:<value>'
            columnInfo.DefaultValue = arg switch {
                "up"      => Vector3.up,
                "down"    => Vector3.down,
                "forward" => Vector3.forward,
                "back"    => Vector3.back,
                "left"    => Vector3.left,
                "right"   => Vector3.right,
                _         => throw new ArgumentOutOfRangeException(nameof(arg), arg, null)
            };
        }
    }
}
