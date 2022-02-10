using System;
using System.IO;
using Untitled.ConfigDataBuilder.Base;

namespace ConfigDataBuilderDev.Base
{
    [ConfigValueConverter(typeof(MyPointConverter))]
    public class MyPoint
    {
        public readonly int X;
        public readonly int Y;

        public MyPoint(int x = 0, int y = 0)
        {
            X = x;
            Y = y;
        }
    }

    // Converter type for MyPoint, must implement IConfigValueConverter<MyPoint>
    public class MyPointConverter : IConfigValueConverter<MyPoint>
    {
        // Parse MyPoint from config string
        public MyPoint Parse(string value)
        {
            var segs = value.Split(',');
            if (segs.Length != 2)
            {
                throw new ArgumentException($"Cannot parse '{value}' to {nameof(MyPoint)}.");
            }
            return new MyPoint(int.Parse(segs[0]), int.Parse(segs[1]));
        }

        // Write MyPoint value to exported config data
        public void WriteTo(BinaryWriter writer, MyPoint value)
        {
            writer.Write(value.X);
            writer.Write(value.Y);
        }

        // Read MyPoint value from exported config data
        public MyPoint ReadFrom(BinaryReader reader)
        {
            var x = reader.ReadInt32();
            var y = reader.ReadInt32();
            return new MyPoint(x, y);
        }

        // Get a string that represent the value.
        // Used in config class' ToString() method.
        public string ToString(MyPoint value)
        {
            return $"<{nameof(MyPoint)} X={value.X}, Y={value.Y}>";
        }

        // Cannot be used in arrays/dictionaries due to separator conflict,
        // so IsScalar should return false.
        public bool IsScalar => false;
    }
}
