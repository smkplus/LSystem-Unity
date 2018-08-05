 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LSystemDebug : MonoBehaviour {
private string axiom = "F";
private string currentString;
private Dictionary<char,string> rules = new Dictionary<char,string>();
public float length = 10,angle = 30;


public Stack<TransformInfo> transformStack = new Stack<TransformInfo>(); 

void Start(){
rules.Add('F',"FF+[+F-F-F]-[-F+F+F]");
currentString = axiom;


StartCoroutine(Generate());
}

public class TransformInfo{
    public Vector3 position;
    public Quaternion rotation;

}

IEnumerator Generate(){
    length /= 2.0f;
    string newString = "";
    for (int i = 0; i < currentString.Length; i++)
    {
        if(rules.ContainsKey(currentString[i])){

        newString += rules[currentString[i]];
        }else{
        newString += currentString[i];  
        }
    }
    currentString = newString;
    Debug.Log(currentString);
    for (int i = 0; i < currentString.Length; i++)
    {
        yield return null;

        if(currentString[i] == 'F'){
            Vector3 InitialPos = transform.position;
            transform.Translate(Vector3.up * length);
            Debug.DrawLine(InitialPos,transform.position,Color.white,10000f,false);
        }else if(currentString[i] == '+'){
            transform.Rotate(Vector3.forward * angle);
        }else if(currentString[i] == '-'){
            transform.Rotate(Vector3.forward * -angle);
        }else if(currentString[i] == '['){
           TransformInfo ti = new TransformInfo();
           ti.rotation = transform.rotation;
           ti.position = transform.position;
           transformStack.Push(ti);
        }else if(currentString[i] == ']'){
            TransformInfo ti = transformStack.Pop();
            transform.position = ti.position;
            transform.rotation = ti.rotation;
        }
    }
    StartCoroutine(Generate());
}

}
