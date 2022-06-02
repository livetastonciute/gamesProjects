using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;

public class save : MonoBehaviour
{
    int currentScore = 0;

    public ThridPersonMovement tps;
    bool check = true;

    [SerializeField]
    private TextMeshProUGUI scoreText;


    private void Start()
    {
        //SaveFile();
        tps = GetComponent<ThridPersonMovement>();
    }
    [SerializeField]
    private string coinTag = "Coin";
    // Start is called before the first frame update
    void Update()
    {

        if (tps.gameHasEnded)
        {
            
            if (check) {
                check = false;
                SaveFile();
                LoadFile();
            }

        }
        
        
    }

    public void SaveFile()
    {
        string destination = "C:/Users/stonc/Documents/GitHub/GameDev/ProjectAz/save.txt";
        //FileStream file;

        //if (File.Exists(destination)) { 
        //    file = File.OpenWrite(destination);
            
        //}
        //else file = File.Create(destination);
        //int data = currentScore;
        ////GameData data = new GameData(currentScore);
        //BinaryFormatter bf = new BinaryFormatter();
        //bf.Serialize(file, data);
        //file.Close();

        if (!File.Exists(destination))
        {
            using (StreamWriter sw = File.CreateText(destination))
            {
                sw.WriteLine(currentScore);
            }
        }
        else
        {
            using (StreamWriter sw = File.AppendText(destination))
            {
                sw.WriteLine(currentScore);
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        var otherGameObject = other.gameObject;


        if (IsCoin(otherGameObject))
        {
            currentScore += 5;
            
        }
        
    }

    public void LoadFile()
    {
        string destination = "C:/Users/stonc/Documents/GitHub/GameDev/ProjectAz/save.txt"; ;
        FileStream file;

        //if (File.Exists(destination)) file = File.OpenRead(destination);
        //else
        //{
        //    Debug.LogError("File not found");
        //    return;
        //}

        //BinaryFormatter bf = new BinaryFormatter();
        //int data = (int)bf.Deserialize(file);
        //file.Close();

        //currentScore = data;
        using (StreamReader sr = File.OpenText(destination))
        {
            string line = "";
            while ((line = sr.ReadLine()) != null)
            {
                Debug.Log("save " + line);
                scoreText.text += $"Score: {line} \n";
            }
        }
        //Debug.Log("save " + currentScore);
    }

    private bool IsCoin(GameObject obj)
    {
        return obj.CompareTag(coinTag);
    }
}
