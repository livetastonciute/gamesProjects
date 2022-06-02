using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SaveToFile : MonoBehaviour
{
    int currentScore = 0;

    bool check = true;

    [SerializeField]
    private Text scoreText;


    [SerializeField]
    private string coinTag = "Coin";

    private void Start()
    {
        string destination = "C:/Users/stonc/Documents/Multimedijos/3 kursas/2 semestras/Zaidimu kurimo pagrindai/studying/save.txt";
        using (StreamReader sr = File.OpenText(destination))
        {
            string line = "";
            line = sr.ReadLine();
            currentScore = int.Parse(line);

        }
    }
    // Start is called before the first frame update
    void Update()
    {

            if (Input.GetKeyDown(KeyCode.L))
            {
                SaveFile();
                LoadFile();
            }
    }

    public void SaveFile()
    {
        string destination = "C:/Users/stonc/Documents/Multimedijos/3 kursas/2 semestras/Zaidimu kurimo pagrindai/studying/save.txt";


        if (!File.Exists(destination))
        {
            using (StreamWriter sw = File.CreateText(destination))
            {
                //rašo vis naujoj eilutėj
                //sw.WriteLine(currentScore);
                sw.Write(currentScore);
            }
        }
        else
        {
            //jei noriu atskiroj eilutėj - File.AppendText(destination))
            using (StreamWriter sw = File.CreateText(destination))
            {
                //rašo vis naujoj eilutėj
                //sw.WriteLine(currentScore);
                sw.Write(currentScore);
            }

        }
    }


    private void OnTriggerEnter(Collider other)
    {
        var otherGameObject = other.gameObject;


        if (other.gameObject.CompareTag("Hp"))
        {
            currentScore += 20;

        }

    }

    public void LoadFile()
    {
        string destination = "C:/Users/stonc/Documents/Multimedijos/3 kursas/2 semestras/Zaidimu kurimo pagrindai/studying/save.txt";

        //išsaugo visus ir parodo visus

        //using (StreamReader sr = File.OpenText(destination))
        //{
        //    string line = "";
        //    while ((line = sr.ReadLine()) != null)
        //    {
        //        Debug.Log("save " + line);
        //        scoreText.text += $"Score: {line} \n";
        //    }
        //}


        //išsaugo vieną ir parodo vieną
        using (StreamReader sr = File.OpenText(destination))
        {
            string line = "";
            line = sr.ReadLine();
            scoreText.text = "Score: " + line;
            
        }
    }
}
