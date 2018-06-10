using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Cb.Web
{
    public class Security
    {
        private const String SECRET_KEY = "28008ef181b3419f950b41c7cad361f1d9caa18f5c91487aaadb83a9fc44581318e3e330bdef4e2da86c666712fb80f672931f2b3acd47b38cc99ad59dc43656d71e918c6965452681a2ac6752873d6ba0543374d7f146ea8adf69584729143163ac69a49b3c4411a5cb09465d4f2b76b44aea3e59ec4beca49c9726b48f6bd3";

        public static String sign(IDictionary<string, string> paramsArray)
        {
            return sign(buildDataToSign(paramsArray), SECRET_KEY);
        }

        private static String sign(String data, String secretKey)
        {
            UTF8Encoding encoding = new System.Text.UTF8Encoding();
            byte[] keyByte = encoding.GetBytes(secretKey);

            HMACSHA256 hmacsha256 = new HMACSHA256(keyByte);
            byte[] messageBytes = encoding.GetBytes(data);
            return Convert.ToBase64String(hmacsha256.ComputeHash(messageBytes));
        }

        private static String buildDataToSign(IDictionary<string, string> paramsArray)
        {
            String[] signedFieldNames = paramsArray["signed_field_names"].Split(',');
            IList<string> dataToSign = new List<string>();

            foreach (String signedFieldName in signedFieldNames)
            {
                dataToSign.Add(signedFieldName + "=" + paramsArray[signedFieldName]);
            }

            return commaSeparate(dataToSign);
        }

        private static String commaSeparate(IList<string> dataToSign)
        {
            return String.Join(",", dataToSign);
        }
    }
}