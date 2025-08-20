using System;
using System.Data;
using System.IO;

namespace MDS.Core.Util
{
    public static class FileStreamed
    {
        #region metodos publicos estaticos
        /// <summary>
        /// metodo que devuelve un arreglo de bytes desde un Stream
        /// </summary>
        /// <typeparam name="stream">Tipo de dato que contiene un Stream</typeparam>
        /// <returns>retorna un arreglo de bytes</returns>
        public static byte[] StreamToByteArray(System.IO.Stream stream)
        {
            long originalPosition = stream.Position;

            byte[] readBuffer = new byte[4096];

            int totalBytesRead = 0;
            int bytesRead;

            while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
            {
                totalBytesRead += bytesRead;

                if (totalBytesRead == readBuffer.Length)
                {
                    int nextByte = stream.ReadByte();
                    if (nextByte != -1)
                    {
                        byte[] temp = new byte[readBuffer.Length * 2];
                        Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                        Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                        readBuffer = temp;
                        totalBytesRead++;
                    }
                }
            }
            byte[] buffer = readBuffer;
            if (readBuffer.Length != totalBytesRead)
            {
                buffer = new byte[totalBytesRead];
                Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
            }
            return buffer;
        }
        /// <summary>
        /// metodo que devuelve un arreglo de bytes desde un Stream
        /// </summary>
        /// <typeparam name="stream">Tipo de dato que contiene un Stream</typeparam>
        /// <returns>retorna un arreglo de bytes</returns>
        public static byte[] ReadFullyStreamToByteArray(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        public static void LoadImageDataSet(ref object objDataSet, int index, string FieldName, string FilePath)
        {
            try
            {
                int ImageField;
                FileStream fs = new FileStream(FilePath,
                           System.IO.FileMode.Open, System.IO.FileAccess.Read);
                byte[] Image = new byte[fs.Length];
                fs.Read(Image, 0, Convert.ToInt32(fs.Length));
                fs.Close();
                ((DataSet)objDataSet).Tables[0].Columns.Add(FieldName, typeof(System.Byte[]));
                ImageField = ((DataSet)objDataSet).Tables[0].Columns.IndexOf(FieldName);
                ((DataSet)objDataSet).Tables[0].Rows[index][ImageField] = Image;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
