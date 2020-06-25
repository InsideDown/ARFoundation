using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System;
using System.Collections.Generic;
using System.IO;

//contentful nodeTypes; document, paragraph, text
public class DataLoader : Singleton<DataLoader>
{
    private const string URL_CONTENTFUL_ENTRIES = "https://cdn.contentful.com/spaces/gl7f1fzb2efs/environments/master/entries?access_token=1oBHjQBuqLUVnNI2SOZYlpBeFwDaWUIf51KJeHUWJRU";
    private List<JSONDataModel.Item> JSONList;
    private const string FILENAME = "waypoint-content.json";
    private string LOCALPERSISTENTPATH;

    protected DataLoader() { }

    /// <summary>
    /// LoadJSON kicks off our download - this file should retain our data
    /// </summary>
    public void LoadJSON()
    {
        Debug.Log("Load JSON called");
        LOCALPERSISTENTPATH = Application.persistentDataPath + "/" + FILENAME;
        StartCoroutine(DownloadJSON());
    }

    /// <summary>
    /// Download our JSON from the server
    /// </summary>
    /// <returns></returns>
    private IEnumerator DownloadJSON()
    {
        UnityWebRequest www = UnityWebRequest.Get(URL_CONTENTFUL_ENTRIES);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogWarning(www.error);
            EventManager.Instance.MainDataLoadError();
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

    private void UseLocalFile()
    {
        if (!File.Exists(LOCALPERSISTENTPATH))
        {
            EventManager.Instance.MainDataLoadError();
            throw new System.Exception("The local file that you are trying to retrieve does not exist: " + LOCALPERSISTENTPATH);
        }
        else
        {
            string jsonStr = File.ReadAllText(LOCALPERSISTENTPATH);
            ProcessJSON(jsonStr);
        }
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

    /// <summary>
    /// Process our downloaded or local JSON file
    /// </summary>
    /// <param name="jsonStr"></param>
    private void ProcessJSON(string jsonStr)
    {
        JSONList = JSONDataModel.CreateFromJSON(jsonStr).items;
        Debug.Log("DATA LOADED and JSONList set in DataLoader");

        //TODO: below is just testing to ensure we can get data
        JSONDataModel.Field curField = GetWaypointInfo("waypoint2");
        if (curField != null)
        {
            //Debug.Log(curField.title);
            //Debug.Log("Content ----");
            //TODO: need to loop through the content to make sure we're capturing paragraphs
            //Debug.Log(curField.content.content[0].content[0].value);
            //Debug.Log(curField.mediaReferences.Count);
            if(curField.mediaReferences != null && curField.mediaReferences.Count > 0)
            {
 
                foreach(JSONDataModel.MediaReference mediaReference in curField.mediaReferences)
                {
                    if(mediaReference.sys.id != null)
                        GetMedia(mediaReference.sys.id);
                }
            }
        }
        EventManager.Instance.MainDataLoaded();
    }

    private void GetMedia(string mediaID)
    {
        Debug.Log("mediaID: " + mediaID);
        foreach (JSONDataModel.Item curItem in JSONList)
        {
            JSONDataModel.Sys curSys = curItem.sys;
            if(curSys.id == mediaID)
            {
                //TODO: break our loop here and do stuff
                break;
            }
            Debug.Log("id: " + curSys.id);
        }
    }

    /// <summary>
    /// based on a waypoint ID, pull our JSON info from a specific node
    /// </summary>
    /// <param name="waypointId"></param>
    private JSONDataModel.Field GetWaypointInfo(string waypointId)
    {
        foreach (JSONDataModel.Item curItem in JSONList)
        {
            JSONDataModel.Field curField = curItem.fields;
            if (waypointId == curField.waypointId)
                return curField;
        }
        Debug.LogWarning("you are trying to access a waypointId which does not exist: " + waypointId);
        return null;
    }


}
