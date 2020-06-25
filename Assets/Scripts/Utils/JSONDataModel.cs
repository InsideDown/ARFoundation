using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class JSONDataModel
{
    public List<Item> items;

    [Serializable]
    public class Item
    {
        public Field fields;
        public Sys sys;
    }

    [Serializable]
    public class Field
    {
        public string waypointId;
        public string title;
        public Content content;
        public List<MediaReference> mediaReferences;
    }

    [Serializable]
    public class Content
    {
        public List<Content> content;
        public string nodeType;
        public string value;
    }

    [Serializable]
    public class MediaReference
    {
        public Sys sys;
    }

    [Serializable]
    public class Sys
    {
        public string type;
        public string linkType;
        public string id;
    }

    public static JSONDataModel CreateFromJSON(string jsonStr)
    {
        return JsonUtility.FromJson<JSONDataModel>(jsonStr);
    }
}
