using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LSystem : MonoBehaviour {

public LSystemFile lSystemFile;
private string currentString;

private float length,angle;




public Stack<TransformInfo> transformStack = new Stack<TransformInfo>(); 

void Start(){
currentString = lSystemFile.rules[0].character.ToString();

length = lSystemFile.length;
angle = lSystemFile.angle;

int GenNum = lSystemFile.NumberOfGeneration;

for (int i = 0; i < GenNum; i++)
{
Generate();
}

}

public class TransformInfo{
    public Vector3 position;
    public Quaternion rotation;

}

void Generate(){
    length /= 2.0f;
    string newString = "";

for (int j = 0; j < lSystemFile.rules.Count; j++)
{
    for (int i = 0; i < currentString.Length; i++)
    {
        if(lSystemFile.rules[j].character == currentString[i]){
        newString += lSystemFile.rules[j].pattern;
        }else{
        newString += currentString[i];  
        }
    }
    }
    
    currentString = newString;
    Debug.Log(currentString);
    
        
    for (int i = 0; i < currentString.Length; i++)
    {
    for (int j = 0; j < lSystemFile.statements.Count; j++)
    {   

        if(currentString[i] == lSystemFile.statements[j].character){
           DoAction (lSystemFile.statements[j].action);
        }
    }
    }
}

    private void DoAction(LSystemFile.Statement.Action action)
    {
        switch (action)
        {
           case LSystemFile.Statement.Action.Move :
            Vector3 InitialPos = transform.position;
            transform.Translate(Vector3.up * length);
            Debug.DrawLine(InitialPos,transform.position,Color.white,10000f,false);
            break;
           case LSystemFile.Statement.Action.RotateLeft :
            transform.Rotate(Vector3.forward * -angle);
            break;
           case LSystemFile.Statement.Action.RotateRight :
            transform.Rotate(Vector3.forward * angle);
            break;
           case LSystemFile.Statement.Action.Push :
            TransformInfo ti = new TransformInfo();
           ti.rotation = transform.rotation;
           ti.position = transform.position;
           transformStack.Push(ti);
            break;
           case LSystemFile.Statement.Action.Pop :
           TransformInfo tipop = transformStack.Pop();
            transform.position = tipop.position;
            transform.rotation = tipop.rotation;
            break;
        }
    }
}
