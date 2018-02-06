using System;
using System.Diagnostics;

public class DSTest
{
    public static void TestPerformance()
    {
        var dataStorage = new DataStorage();
        var data = ProtoTest.TestDeserialize();
        dataStorage.Init(data);
        Stopwatch s = new Stopwatch();
        s.Start();
        for (int i = 0; i < 100000; ++i)
        {
            dataStorage.Data.Player.Id.Set("testId");
        }
        s.Stop();
        Console.WriteLine("ds set = " + s.Elapsed);
        s.Reset();
        s.Start();
        for (int i = 0; i < 100000; ++i)
        {
            data.Player.Id = "testId";
        }
        s.Stop();
        Console.WriteLine("default set = " + s.Elapsed);
    }

    public static void Test()
    {
        var dataStorage = new DataStorage();
        var data = ProtoTest.TestDeserialize();
        dataStorage.Init(data);
        Console.WriteLine(data);
        var player = dataStorage.Data.Player.Get();
        Console.WriteLine(player);
    }
}