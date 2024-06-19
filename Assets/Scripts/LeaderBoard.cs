using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Record
{
    public string name;
    public string gr;
    public string level;
}

public class LeaderBoard : Singleton<LeaderBoard>
{
    public Text[] playerNames;
    public Text[] playerGRs;
    public Text[] playerLVs;
    private string filePath;
    private List<Record> records = new();

    void Start()
    {
        MockRecord();
        LoadRecord();
        SortRecords();

        int index = 0;
        foreach (Record record in records)
        {
            if (playerNames != null && playerNames.Length != 0) { playerNames[index].text = record.name; }
            if (playerGRs != null && playerGRs.Length != 0) { playerGRs[index].text = record.gr; }
            if (playerLVs != null && playerLVs.Length != 0) { playerLVs[index].text = record.level; }
            index++;
            if (index >= 5) return;
        }
    }

    void MockRecord()
    {

        Record record = new()
        {
            name = "khanh",
            gr = "150",
            level = "1"
        };
        Record record2 = new()
        {
            name = "Thomas",
            gr = "200",
            level = "2"
        };
        Record record3 = new()
        {
            name = "Xuka",
            gr = "300",
            level = "3"
        };
        records.Add(record);
        records.Add(record2);
        records.Add(record3);
    }

    void LoadRecord()
    {
        List<string> fileRecords = ReadRecordsFromFile();

        foreach (string fileRecord in fileRecords)
        {
            string[] data = fileRecord.Split(";");
            Record record = new()
            {
                name = data[0],
                gr = data[1],
                level = data[2]
            };
            records.Add(record);
        }
    }

    void SortRecords()
    {
        records = records.OrderBy(record => float.Parse(record.gr))
                                    .ThenBy(record => float.Parse(record.level))
                                    .ThenBy(record => record.name)
                                    .ToList();
    }

    List<string> ReadRecordsFromFile()
    {
        filePath = Application.persistentDataPath + "/data.txt";
        List<string> records = new();

        if (File.Exists(filePath))
        {
            using StreamReader reader = new(filePath);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                records.Add(line);
            }
        }
        else
        {
            Debug.LogWarning("File không tồn tại.");
        }

        return records;
    }

    public void WriteRecordToFile(Record record)
    {
        filePath = Application.persistentDataPath + "/data.txt";
        string fileRecord = $"{record.name};{record.gr};{record.level}";
        using StreamWriter writer = new(filePath, true);
        writer.WriteLine(fileRecord);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
