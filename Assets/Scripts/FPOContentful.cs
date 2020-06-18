using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System;
using System.Collections.Generic;
using System.IO;

[Serializable]
public class RootJSON
{
    public List<Item> items;

    [Serializable]
    public class Item
    {
        public Field fields;
    }

    [Serializable]
    public class Field
    {
        public string waypointId;
        public string title;
        public Content content;
    }

    [Serializable]
    public class Content
    {
        [NonSerialized]
        public List<Content> content;
        public string nodeType;
        public string value;
    }

    public static RootJSON CreateFromJSON(string jsonStr)
    {
        return JsonUtility.FromJson<RootJSON>(jsonStr);
    }
}

//contentful nodeTypes; document, paragraph, text

public class FPOContentful : MonoBehaviour
{
    private const string URL_CONTENTFUL_ENTRIES = "https://cdn.contentful.com/spaces/gl7f1fzb2efs/environments/master/entries?access_token=1oBHjQBuqLUVnNI2SOZYlpBeFwDaWUIf51KJeHUWJRU";
    private List<RootJSON.Item> JSONList;
    private const string FILENAME = "waypoint-content.json";
    private string LOCALPERSISTENTPATH ;

    // Start is called before the first frame update
    void Start()
    {
        LOCALPERSISTENTPATH = Application.persistentDataPath + "/" + FILENAME;
        StartCoroutine(DownloadJSON());
    }

    /// <summary>
    /// set our local file - always overwrite with whatever we pulled from the server
    /// </summary>
    /// <param name="resultBytes"></param>
    private void SetLocalFile(byte[] resultBytes)
    {
        Debug.Log("setting local file");
        File.WriteAllBytes(LOCALPERSISTENTPATH, resultBytes);
    }

    private void UseLocalFile()
    {
        if (!File.Exists(LOCALPERSISTENTPATH))
        {
            throw new System.Exception("The local file that you are trying to retrieve does not exist: " + LOCALPERSISTENTPATH);
        }
        else
        {
        
            string jsonStr = File.ReadAllText(LOCALPERSISTENTPATH);


            Debug.Log(LOCALPERSISTENTPATH);
            Debug.Log("jsonStr");
            Debug.Log(jsonStr);
            ProcessJSON(jsonStr);
        }
    }

    private IEnumerator DownloadJSON()
    {
        UnityWebRequest www = UnityWebRequest.Get(URL_CONTENTFUL_ENTRIES);
        yield return www.SendWebRequest();

        //if (www.isNetworkError || www.isHttpError)
        if(!www.isNetworkError)
        {
            Debug.Log(www.error);
            //see if we can pull our local file instead
            UseLocalFile();
        }
        else
        {
            string jsonStr = www.downloadHandler.text;
            byte[] resultBytes = www.downloadHandler.data;
            //set our local file with whatever we just downloaded
            SetLocalFile(resultBytes);
            ProcessJSON(jsonStr);
        }
    }

    private void ProcessJSON(string jsonStr)
    {
        JSONList = RootJSON.CreateFromJSON(jsonStr).items;
        RootJSON.Field curField = GetWaypointInfo("waypoint2");
        if (curField != null)
        {
            Debug.Log(curField.title);
            Debug.Log(curField.content);
        }
    }

    /// <summary>
    /// based on a waypoint ID, pull our JSON info from a specific node
    /// </summary>
    /// <param name="waypointId"></param>
    private RootJSON.Field GetWaypointInfo(string waypointId)
    {
        foreach(RootJSON.Item curItem in JSONList)
        {
            RootJSON.Field curField = curItem.fields;
            if (waypointId == curField.waypointId)
                return curField;
        }
        Debug.LogWarning("you are trying to access a waypointId which does not exist: " + waypointId);
        return null;
    }


}

