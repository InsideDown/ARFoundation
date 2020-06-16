using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System;
using System.Collections.Generic;

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
        public string title;
        public Content content;
    }

    [Serializable]
    public class Content
    {
        public List<Content> content;
        public string nodeType;
        public string value;
    }

    public static RootJSON CreateFromJSON(string jsonStr)
    {
        return JsonUtility.FromJson<RootJSON>(jsonStr);
    }
}

public class FPOContentful : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DownloadJSON());
    }

    private IEnumerator DownloadJSON()
    {
        UnityWebRequest www = UnityWebRequest.Get("https://cdn.contentful.com/spaces/gl7f1fzb2efs/environments/master/entries?access_token=1oBHjQBuqLUVnNI2SOZYlpBeFwDaWUIf51KJeHUWJRU");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            string jsonStr = www.downloadHandler.text;
            // Show results as text
            //Debug.Log(jsonStr);

            // Or retrieve results as binary data
            //byte[] results = www.downloadHandler.data;

            //for (int i = 0; i < items.Items.Length; i++)
            //{
            //    Debug.Log(items.Items[i].Text);
            //}
            var MaxResponseObj = RootJSON.CreateFromJSON(jsonStr).items[0].fields.title;
            Debug.Log(MaxResponseObj);
        }
    }


}

