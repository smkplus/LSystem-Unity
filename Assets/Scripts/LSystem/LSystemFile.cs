using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LSystem", menuName = "LSystem/LSystemObj")]
public class LSystemFile : ScriptableObject {
public string axiom = "";
public float length = 10,angle = 30;

public int NumberOfGeneration = 3;



[System.Serializable]
public class Rules{
    public char character;
    public string pattern;
}

[System.Serializable]
public class Statement{
    public char character;
    public enum Action{Move,RotateLeft,RotateRight,Push,Pop}
    public Action action;
}

public List<Statement> statements = new List<Statement>();

public List<Rules> rules = new List<Rules>();

}
