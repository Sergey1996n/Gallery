using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneManagerExtensions 
{
    private static Dictionary<string, Texture2D> parameters;
    private static Stack<string> sceneStack = new Stack<string>();

    public static void LoadSceneWithHistory(string sceneName)
    {
        SceneLoadingManager.NameScene = sceneName;
        sceneStack.Push(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("Loading");
    }

    public static void LoadSceneWithHistory(string sceneName, Dictionary<string, Texture2D> parameters = null)
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
        string sceneName = sceneStack.Pop();
        SceneManager.LoadScene(sceneName);
    }

    public static Dictionary<string, Texture2D> GetSceneParameters()
    {
        return parameters;
    }

    public static Texture2D GetParam(string paramKey)
    {
        if (parameters == null || !parameters.ContainsKey(paramKey))
        {
            return null;
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

    public static void SetParam(string paramKey, Texture2D paramValue)
    {
        if (parameters == null)
        {
            parameters = new Dictionary<string, Texture2D>();
        }
        parameters.Add(paramKey, paramValue);
    }
}
