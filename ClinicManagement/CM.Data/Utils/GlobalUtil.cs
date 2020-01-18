using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Data.Utils
{
    public static class GlobalUtil
    {
        public static string EncodePasswordToBase64(string password)
        {
            try
            {
                byte[] encDataByte = new byte[password.Length];
                encDataByte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encDataByte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }

        public static void LogException(Exception exception)
        {

        }
    }
}
