using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Data
{
    public static class DataImporter
    {
        public static string DataDirectoryName = "/Data";
        public static string DataExtentionString = ".json";

        public static void Import()
        {
        }

        private static void LoadData(string fileName)
        {
            string dirName = Application.persistentDataPath + DataDirectoryName;

            if (Directory.Exists(dirName) && File.Exists(dirName + "/" + fileName + DataExtentionString))
            {
                LoadFromHD(dirName + "/" + fileName + DataExtentionString);
            }
            else
            {
                Directory.CreateDirectory(dirName);
                File.WriteAllText(dirName + "/" + fileName + DataExtentionString, LoadFromAssets(fileName));
            }
        }

        private static void LoadFromHD(string filePath)
        {
            
        }

        private static string LoadFromAssets(string fileName)
        {
            TextAsset ta = Resources.Load(fileName) as TextAsset;

            string s = ta.text;

            return s;
        }
    }
}