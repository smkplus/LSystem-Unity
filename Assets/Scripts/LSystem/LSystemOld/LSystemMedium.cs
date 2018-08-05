using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LSystemMedium : MonoBehaviour {

public string axiom = "F";
public float length = 10,angle = 30;
public string currentString;


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


public Stack<TransformInfo> transformStack = new Stack<TransformInfo>(); 

void Start(){
currentString = rules[0].character.ToString();

Generate();
Generate();
Generate();
}

public class TransformInfo{
    public Vector3 position;
    public Quaternion rotation;

}

void Generate(){
    length /= 2.0f;
    string newString = "";

for (int j = 0; j < rules.Count; j++)
{
    for (int i = 0; i < currentString.Length; i++)
    {
        if(rules[j].character == currentString[i]){
        newString += rules[j].pattern;
        }else{
        newString += currentString[i];  
        }
    }
    }
    
    currentString = newString;
    Debug.Log(currentString);
    
        
    for (int i = 0; i < currentString.Length; i++)
    {
    for (int j = 0; j < statements.Count; j++)
    {

        if(currentString[i] == statements[j].character){
           DoAction (statements[j].action);
           Debug.Log(statements[j].action);
        }
    }
    }
}

    private void DoAction(Statement.Action action)
    {
        switch (action)
        {
           case Statement.Action.Move :
            Vector3 InitialPos = transform.position;
            transform.Translate(Vector3.up * length);
            Debug.DrawLine(InitialPos,transform.position,Color.white,10000f,false);
            break;
           case Statement.Action.RotateLeft :
            transform.Rotate(Vector3.forward * angle);
            break;
           case Statement.Action.RotateRight :
            transform.Rotate(Vector3.forward * -angle);
            break;
           case Statement.Action.Push :
            TransformInfo ti = new TransformInfo();
           ti.rotation = transform.rotation;
           ti.position = transform.position;
           transformStack.Push(ti);
            break;
           case Statement.Action.Pop :
           TransformInfo tipop = transformStack.Pop();
            transform.position = tipop.position;
            transform.rotation = tipop.rotation;
            break;
        }
    }
}
