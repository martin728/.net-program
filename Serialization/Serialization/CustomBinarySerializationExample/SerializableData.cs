using System;
using System.Runtime.Serialization;

namespace CustomBinarySerializationExample
{
    [Serializable]
    public class SerializableData: ISerializable
    {
        public int Number { get; set; }
        public string Text { get; set; }
        
        public SerializableData() { }
        
        // Serialization constructor
        protected SerializableData(SerializationInfo info, StreamingContext context)
        {
            Number = info.GetInt32("Number");
            Text = info.GetString("Text");
        }

        // Custom binary serialization
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Number", Number);
            info.AddValue("Text", Text);
        }
    }
}