using Newtonsoft.Json;
using System.Text;

namespace ServiceHub.DataAccess.Helpers
{
    public static class Serialization
    {
        //warning SYSLIB0011: 'BinaryFormatter.Serialize(Stream, object)' is obsolete
        //
        //public static byte[] ToByteArray(this object obj)
        //{
        //    if (obj == null)
        //    {
        //        return null;
        //    }
        //    BinaryFormatter binaryFormatter = new BinaryFormatter();
        //    using (MemoryStream memoryStream = new MemoryStream())
        //    {
        //        binaryFormatter.Serialize(memoryStream, obj);
        //        return memoryStream.ToArray();
        //    }
        //}

        //public static T FromByteArray<T>(this byte[] byteArray) where T : class
        //{
        //    if (byteArray == null)
        //    {
        //        return default(T);
        //    }
        //    BinaryFormatter binaryFormatter = new BinaryFormatter();
        //    using (MemoryStream memoryStream = new MemoryStream(byteArray))
        //    {
        //        return binaryFormatter.Deserialize(memoryStream) as T;
        //    }
        //}

        public static byte[]? ToByteArray(this object obj)
        {
            if (obj == null)
            {
                return null;
            }

            string jsonData = JsonConvert.SerializeObject(obj);
            return Encoding.UTF8.GetBytes(jsonData);
        }

        public static T? FromByteArray<T>(this byte[] byteArray) where T : class
        {
            if (byteArray == null)
            {
                return default;
            }

            string bytesAsString = Encoding.UTF8.GetString(byteArray);
            return JsonConvert.DeserializeObject<T>(bytesAsString);
        }
    }
}