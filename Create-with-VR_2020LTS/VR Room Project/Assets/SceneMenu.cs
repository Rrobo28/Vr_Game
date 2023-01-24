using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEditor;
public class SceneMenu
{
    [MenuItem("Scenes/Ship")]
    public static void OpenShip() 
    {
        OpenScene("Create_with_VR_Starter_Scene");
    }
    [MenuItem("Scenes/Training")]
    public static void OpenTraining()
    {
        OpenScene("Training");
    }

    public static void OpenScene(string name)
    {
        EditorSceneManager.OpenScene("Assets/Scenes/" + name + ".unity", OpenSceneMode.Single);
        EditorSceneManager.OpenScene("Assets/Scenes/Persistent.unity", OpenSceneMode.Additive);
        
    }



}
    
