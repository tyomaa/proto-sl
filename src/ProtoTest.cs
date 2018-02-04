using System;
using Google.Protobuf;
using Sl;
using System.IO;

public static class ProtoTest
{
    private static string _binaryDataPath = "C:/Work/data.bytes";

    public static void Test()
    {
        // TestSerialize();
        TestDeserialize();
    }

    public static Data TestDeserialize()
    {
        using (var stream = new CodedInputStream(
            new FileStream(
                _binaryDataPath, 
                FileMode.Open, 
                FileAccess.Read)))
        {
            var data = Data.Parser.ParseFrom(stream);
            Console.WriteLine(data);
            return data;
        }
    }

    private static void TestSerialize()
    {
        var data = CreateData();
        using (var s = new CodedOutputStream(
            new FileStream(
                _binaryDataPath, 
                FileMode.OpenOrCreate, 
                FileAccess.ReadWrite)))
        {
            data.WriteTo(s);
        }
    }

    private static Data CreateData()
    {
        var player = new Player();
        player.Id = "2";
        player.Name = "username";
        var data = new Data();
        data.Level = 5;
        data.Player = player;
        return data;
    }
}
