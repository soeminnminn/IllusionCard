// Decompiled with JetBrains decompiler
// Type: CharacterHS.YS_Assist
// Assembly: H2PSceneConverter, Version=1.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 1583ABF6-A697-4C49-B1E4-14D2C05BEFFF
// Assembly location: D:\Games\Illusion\PlayHome\Plugins\H2PSceneConverter.dll

using Microsoft.Win32;
using System;
using System.IO;
using System.Net.NetworkInformation;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class YS_Assist
{
    private static readonly string passwordChars36 = "0123456789abcdefghijklmnopqrstuvwxyz";
    private static readonly string passwordChars62 = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private static readonly char[] tbl62 = new char[62]
    {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l',
            'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H',
            'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
    };
    private static readonly byte[] tblRevCode = new byte[128]
    {
            50, 112, 114, 160, 140, 152, 202, 10, 235, 9, 198, 113, 78, 208, 182, 154, 247, 249, 64, 243, 232, 102, 184, 130,
            196, 33, 149, 171, 62, 235, 124, 183, 193, 189, 168, 165, 243, 117, 48, 23, 16, 114, 192, 105, 122, 253, 206, 143,
            240, 183, 150, 127, 115, 117, 19, 135, 140, 187, 73, 133, 254, 231, 48, 92, 205, 127, 122, 237, 250, 212, 27, 92,
            153, 237, 54, 161, 135, 216, 104, 10, 60, 128, 97, 33, 47, 124, 18, 218, 168, 133, 217, 249, 188, 179, 198, 104,
            68, 229, 179, 61, 10, 22, 10, 183, 8, 189, 74, 86, 107, 47, 230, 233, 158, 241, 27, 85, 198, 164, 151, 135, 148,
            4, 24, 172, 122, 214, 18, 140
    };

    public static T DeepCopyWithSerializationSurrogate<T>(T target) where T : ISerializationSurrogate
    {
        using (MemoryStream memoryStream = new MemoryStream())
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            SurrogateSelector surrogateSelector = new SurrogateSelector();
            StreamingContext context = new StreamingContext(StreamingContextStates.All);
            surrogateSelector.AddSurrogate(typeof(T), context, target);
            binaryFormatter.SurrogateSelector = surrogateSelector;
            try
            {
                binaryFormatter.Serialize(memoryStream, target);
                memoryStream.Position = 0L;
                return (T)binaryFormatter.Deserialize(memoryStream);
            }
            finally
            {
                memoryStream.Close();
            }
        }
    }

    public static void GetListString(string text, out string[,] data)
    {
        string[] strArray1 = text.Split('\n');
        int length1 = strArray1.Length;
        if (length1 != 0 && strArray1[length1 - 1].Trim() == string.Empty)
            --length1;
        string[] strArray2 = strArray1[0].Split('\t');
        int length2 = strArray2.Length;
        if (length2 != 0 && strArray2[length2 - 1].Trim() == string.Empty)
            --length2;
        data = new string[length1, length2];
        for (int index1 = 0; index1 < length1; ++index1)
        {
            string[] strArray3 = strArray1[index1].Split('\t');
            for (int index2 = 0; index2 < strArray3.Length && index2 < length2; ++index2)
                data[index1, index2] = strArray3[index2];
            data[index1, strArray3.Length - 1] = data[index1, strArray3.Length - 1].Replace("\r", string.Empty).Replace("\n", string.Empty);
        }
    }

    public static void BitRevBytes(byte[] data, int startPos)
    {
        int index1 = startPos % tblRevCode.Length;
        for (int index2 = 0; index2 < data.Length; ++index2)
        {
            int index3 = index2;
            data[index3] ^= tblRevCode[index1];
            index1 = (index1 + 1) % tblRevCode.Length;
        }
    }

    public static string Convert64StringFromInt32(int num)
    {
        StringBuilder stringBuilder1 = new StringBuilder();
        if (num < 0)
        {
            num *= -1;
            stringBuilder1.Append("0");
        }
        for (; num >= 62; num /= 62)
            stringBuilder1.Append(tbl62[num % 62]);
        stringBuilder1.Append(tbl62[num]);
        StringBuilder stringBuilder2 = new StringBuilder();
        for (int index = stringBuilder1.Length - 1; index >= 0; --index)
            stringBuilder2.Append(stringBuilder1[index]);
        return stringBuilder2.ToString();
    }

    public static string GeneratePassword36(int length)
    {
        StringBuilder stringBuilder = new StringBuilder(length);
        byte[] data = new byte[length];
        new RNGCryptoServiceProvider().GetBytes(data);
        for (int index1 = 0; index1 < length; ++index1)
        {
            int index2 = data[index1] % passwordChars36.Length;
            char ch = passwordChars36[index2];
            stringBuilder.Append(ch);
        }
        return stringBuilder.ToString();
    }

    public static string GeneratePassword62(int length)
    {
        StringBuilder stringBuilder = new StringBuilder(length);
        byte[] data = new byte[length];
        new RNGCryptoServiceProvider().GetBytes(data);
        for (int index1 = 0; index1 < length; ++index1)
        {
            int index2 = data[index1] % passwordChars62.Length;
            char ch = passwordChars62[index2];
            stringBuilder.Append(ch);
        }
        return stringBuilder.ToString();
    }

    public static byte[] CreateSha256(string planeStr, string key)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(planeStr);
        return new HMACSHA256(Encoding.UTF8.GetBytes(key)).ComputeHash(bytes);
    }

    public static byte[] CreateSha256(byte[] data, string key)
    {
        return new HMACSHA256(Encoding.UTF8.GetBytes(key)).ComputeHash(data);
    }

    public static string GetMacAddress()
    {
        string empty = string.Empty;
        NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
        if (networkInterfaces != null)
        {
            foreach (NetworkInterface networkInterface in networkInterfaces)
            {
                PhysicalAddress physicalAddress = networkInterface.GetPhysicalAddress();
                byte[] numArray = null;
                if (physicalAddress != null)
                    numArray = physicalAddress.GetAddressBytes();
                if (numArray != null && numArray.Length == 6)
                {
                    empty += physicalAddress.ToString();
                    break;
                }
            }
        }
        return empty;
    }

    public static string CreateUUID()
    {
        StringBuilder stringBuilder = new StringBuilder(32);
        stringBuilder.Append(GetMacAddress());
        if (string.Empty == stringBuilder.ToString())
            stringBuilder.Append(GeneratePassword36(12));
        stringBuilder.Append(DateTime.Now.ToString("MMddHHmmssfff"));
        stringBuilder.Append(GeneratePassword36(40));
        byte[] bytes = Encoding.UTF8.GetBytes(stringBuilder.ToString());
        BitRevBytes(bytes, 7);
        stringBuilder.Length = 0;
        int num1 = bytes.Length / 4;
        int num2 = bytes.Length % 4;
        if (num2 != 0)
        {
            int length1 = 4 - num2;
            byte[] numArray1 = new byte[length1];
            int length2 = bytes.Length;
            Array.Resize<byte>(ref bytes, length2 + length1);
            byte[] numArray2 = bytes;
            int destinationIndex = length2;
            int length3 = length1;
            Array.Copy(numArray1, 0, numArray2, destinationIndex, length3);
            ++num1;
        }
        for (int index = 0; index < num1; ++index)
            stringBuilder.Append(Convert64StringFromInt32(BitConverter.ToInt32(bytes, index * 4)));
        string str;
        if (stringBuilder.Length < 64)
        {
            int length = 64 - stringBuilder.Length;
            stringBuilder.Append(GeneratePassword62(length));
            str = stringBuilder.ToString();
        }
        else
            str = stringBuilder.ToString().Substring(0, 64);
        return str;
    }

    public static string GetStringRight(string str, int len)
    {
        if (str == null)
            return string.Empty;
        return str.Length <= len ? str : str.Substring(str.Length - len, len);
    }

    public static string GetRemoveStringRight(string str, int len)
    {
        return str == null || str.Length <= len ? string.Empty : str.Substring(0, str.Length - len);
    }

    public static string GetRemoveStringLeft(string str, string find, bool removeFind = true)
    {
        if (str == null)
            return string.Empty;
        int num = str.IndexOf(find);
        if (0 >= num)
            return str;
        int startIndex = num + (!removeFind ? 0 : find.Length);
        return str.Substring(startIndex);
    }

    public static string GetRemoveStringRight(string str, string find, bool removeFind = false)
    {
        if (str == null)
            return string.Empty;
        int num = str.LastIndexOf(find);
        if (0 >= num)
            return str;
        int length = num + (!removeFind ? find.Length : 0);
        return str.Substring(0, length);
    }

    public static byte[] EncryptAES(byte[] srcData, string pw = "illusion", string salt = "unityunity")
    {
        RijndaelManaged rijndaelManaged = new RijndaelManaged();
        rijndaelManaged.KeySize = 128;
        rijndaelManaged.BlockSize = 128;
        byte[] bytes = Encoding.UTF8.GetBytes(salt);
        Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(pw, bytes);
        rfc2898DeriveBytes.IterationCount = 1000;
        rijndaelManaged.Key = rfc2898DeriveBytes.GetBytes(rijndaelManaged.KeySize / 8);
        rijndaelManaged.IV = rfc2898DeriveBytes.GetBytes(rijndaelManaged.BlockSize / 8);
        ICryptoTransform encryptor = rijndaelManaged.CreateEncryptor();
        byte[] numArray = encryptor.TransformFinalBlock(srcData, 0, srcData.Length);
        encryptor.Dispose();
        return numArray;
    }

    public static byte[] DecryptAES(byte[] srcData, string pw = "illusion", string salt = "unityunity")
    {
        RijndaelManaged rijndaelManaged = new RijndaelManaged();
        rijndaelManaged.KeySize = 128;
        rijndaelManaged.BlockSize = 128;
        byte[] bytes = Encoding.UTF8.GetBytes(salt);
        Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(pw, bytes);
        rfc2898DeriveBytes.IterationCount = 1000;
        rijndaelManaged.Key = rfc2898DeriveBytes.GetBytes(rijndaelManaged.KeySize / 8);
        rijndaelManaged.IV = rfc2898DeriveBytes.GetBytes(rijndaelManaged.BlockSize / 8);
        ICryptoTransform decryptor = rijndaelManaged.CreateDecryptor();
        byte[] numArray = decryptor.TransformFinalBlock(srcData, 0, srcData.Length);
        decryptor.Dispose();
        return numArray;
    }

    public string GetRegistryInfoFrom(string keyName, string valueName, string baseKey = "HKEY_CURRENT_USER")
    {
        string empty = string.Empty;
        string str;
        try
        {
            RegistryKey registryKey;
            if (baseKey == "HKEY_CURRENT_USER")
            {
                registryKey = Registry.CurrentUser.OpenSubKey(keyName);
            }
            else
            {
                if (!(baseKey == "HKEY_LOCAL_MACHINE"))
                    return null;
                registryKey = Registry.LocalMachine.OpenSubKey(keyName);
            }
            if (registryKey == null)
                return null;
            str = (string)registryKey.GetValue(valueName);
            registryKey.Close();
        }
        catch (Exception)
        {
            throw;
        }
        return str;
    }

    public static bool CompareFile(string file1, string file2)
    {
        if (file1 == file2)
            return true;
        FileStream fileStream1;
        try
        {
            fileStream1 = new FileStream(file1, FileMode.Open);
        }
        catch (FileNotFoundException)
        {
            Debug.LogError(file1 + " がない");
            return false;
        }
        FileStream fileStream2;
        try
        {
            fileStream2 = new FileStream(file2, FileMode.Open);
        }
        catch (FileNotFoundException)
        {
            fileStream1.Close();
            Debug.LogError(file2 + " がない");
            return false;
        }
        if (fileStream1.Length != fileStream2.Length)
        {
            fileStream1.Close();
            fileStream2.Close();
            return false;
        }
        int num1;
        int num2;
        do
        {
            num1 = fileStream1.ReadByte();
            num2 = fileStream2.ReadByte();
        }
        while (num1 == num2 && num1 != -1);
        fileStream1.Close();
        fileStream2.Close();
        return num1 - num2 == 0;
    }
}
