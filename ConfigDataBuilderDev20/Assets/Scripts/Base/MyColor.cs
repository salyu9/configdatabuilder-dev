using System;
using System.IO;
using UnityEngine;
using Untitled.ConfigDataBuilder.Base;

namespace ConfigDataBuilderDev.Base
{
    [ConfigValueConverter(typeof(MyColorConverter), "my-color")]
    public class MyColor
    {
        public Color Color { get; }

        public MyColor(Color color)
        {
            Color = color;
        }

        public static implicit operator Color(MyColor myColor) => myColor.Color;
        public static implicit operator MyColor(Color color) => new MyColor(color);
    }

    // Converter type for MyColor, must implement IConfigValueConverter<MyColor>
    public class MyColorConverter : IConfigValueConverter<MyColor>
    {
        // Parse MyColor from config string
        public MyColor Parse(string value)
        {
            return value switch {
                "red"   => Color.red,
                "green" => Color.green,
                "blue"  => Color.blue,
                _       => throw new ArgumentOutOfRangeException(nameof(value), value, null)
            };
        }

        // Write MyColor value to exported config data
        public void WriteTo(BinaryWriter writer, MyColor value)
        {
            var color32 = (Color32)value.Color;
            writer.Write(color32.r);
            writer.Write(color32.g);
            writer.Write(color32.b);
        }

        // Read MyColor value from exported config data
        public MyColor ReadFrom(BinaryReader reader)
        {
            var r = reader.ReadByte();
            var g = reader.ReadByte();
            var b = reader.ReadByte();
            return new MyColor(new Color32(r, g, b, 1));
        }

        // Get a string that represent the value.
        // Used in config class' ToString() method.
        public string ToString(MyColor value)
        {
            return ColorUtility.ToHtmlStringRGB(value.Color);
        }
    }
}
