using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CustomBinarySerializationExample
{
    internal class Program
    {
        public static void Main(string[] args)
        {SerializableData obj = new SerializableData { Number = 66, Text = "Custom binary serialization!" };

            // Custom binary serialization
            using (FileStream stream = new FileStream("serializableData.bin", FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, obj);
            }

            // Custom binary deserialization
            SerializableData deserializedObj;
            using (FileStream stream = new FileStream("serializableData.bin", FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                deserializedObj = (SerializableData)formatter.Deserialize(stream);
            }

            // Display deserialized data
            Console.WriteLine("Deserialized SimpleClass:");
            Console.WriteLine($"Number: {deserializedObj.Number}");
            Console.WriteLine($"Text: {deserializedObj.Text}");
        }
    }
}