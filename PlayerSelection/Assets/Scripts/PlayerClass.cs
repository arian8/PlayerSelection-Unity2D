using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


[System.Serializable]
public class PlayerClass {
    

    public string name;
    public int age;
    public float strength;
    public int speed;
    // public List<Combo> combos;

    public void setName(string _n)
    { name = _n; }

    public PlayerClass(string _n, int a, int s, int sp)
    { name = _n; age = a; strength = s; speed = sp; }


    /* LA GALERE 
    
    public static PlayerClass CreateFromJSON(string jsonString)
    {
        PlayerClass pl = JsonUtility.FromJson<PlayerClass>(jsonString);  
        return pl;
    }
    
    public void CreateNewCharacter()
    {
        // au moment de créer personnage, le mettre aussi en json
        PlayerClass myObject = new PlayerClass("Ariane", 21, 666, 0);
        string json = JsonUtility.ToJson(myObject); //from Object to string in Json format
   //     JsonUtility.FromJson<PlayerClass>(json); //from json format to object PlayerClass

    }
    void Start () {
        CreateNewCharacter();
    }
	

	void Update () {
		
	}
}

public class Combo {
    public string name_combo;
    public List<string> movements;  
    // plutôt liste de Inputs ? 
    public Combo(string _n, List<string> _m)
    {
        name_combo = _n;
        movements = _m;
    }
    */
}

[System.Serializable]
public class PlayersDictionnary
{/*
    public string[] items;*/
}