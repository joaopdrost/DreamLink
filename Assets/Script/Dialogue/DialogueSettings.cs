using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CreateAssetMenu(fileName = "New Dialogue", menuName = "New Dialogue/Dialogue" )]
public class DialogueSettings : ScriptableObject
{
    [Header("Settings")]
    public GameObject actor;

    [Header("Dialogue")]
    public Sprite speakeSprite;
    public string sentence;

    public List<Sentence> dialogues = new List<Sentence>();
}

[System.Serializable]
public class Sentence
{
    public string actorName;
    public Sprite profile;
    public Languages setence;
}

[System.Serializable]
public class Languages
{
    public string portugese;
    public string english;
    public string spanish;
}

#if UNITY_EDITOR
[CustomEditor(typeof(DialogueSettings))]
public class BulderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        DialogueSettings ds = (DialogueSettings)target;
        Languages l = new Languages();
        l.portugese = ds.sentence;

        Sentence s = new Sentence();
        s.profile = ds.speakeSprite;
        s.setence = l;

        if (GUILayout.Button("Create Dialogue"))
        {
            if (ds.sentence != "")
            {
                ds.dialogues.Add(s);

                ds.speakeSprite = null;
                ds.sentence = "";
            }
        }
    }
}

#endif