using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneManagerExtensions 
{
    private static Dictionary<string, string> parameters;
    private static Stack<string> sceneStack = new Stack<string>();

    public static void LoadSceneWithHistory(string sceneName)
    {
        SceneLoadingManager.NameScene = sceneName;
        sceneStack.Push(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("Loading");
    }

    public static void LoadSceneWithHistory(string sceneName, Dictionary<string, string> parameters = null)
    {
        SceneManagerExtensions.parameters = parameters;
        SceneLoadingManager.NameScene = sceneName;
        sceneStack.Push(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("Loading");
    }

    public static void GoBack()
    {
        if (sceneStack.Count < 1)
        {
            return;
        }

        string sceneName = sceneStack.Peek();
        SceneManager.LoadScene(sceneName);
    }

    public static Dictionary<string, string> GetSceneParameters()
    {
        return parameters;
    }

    public static string GetParam(string paramKey)
    {
        if (parameters == null || !parameters.ContainsKey(paramKey))
        {
            return "";
        }
        return parameters[paramKey];
    }

    public static bool IsParamSet(string paramKey)
    {
        if (parameters == null)
        {
            return false;
        }
        return parameters.ContainsKey(paramKey);
    }

    public static void SetParam(string paramKey, string paramValue)
    {
        if (parameters == null)
        {
            parameters = new Dictionary<string, string>();
        }
        parameters.Add(paramKey, paramValue);
    }
}
