using Gokboerue.GeneratedCodes;
using Gokboerue.Tools;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Gokboerue.Tools.GameAssets;
using static Gokboerue.Tools.GameManager;
using static Gokboerue.Tools.Logger<Gokboerue.Gokboerue>;

namespace Gokboerue
{
    public class Gokboerue : MonoBehaviour
    {
        #region Prefab Manager
        public static PrefabAssetPool GetPrefabAsset(string tag)
        {
            if(!AssetData.tags.Contains(tag))
            {
                throw LogException("Tag not found in AssetData", Tools.LogType.Error);
            }

            return GetGameAssets().prefabAssets.FirstOrDefault(x => x.tag == tag);
        }
        #endregion

        #region Game Manager & Assets
        public static GameManager GetGameManager() => i_GameManager;

        public static GameAssets GetGameAssets() => i_GameAssets;
        #endregion

        #region Randomness
        public static int RandomInt(int min, int max)
        {
            return Random.Range(min, max);
        }

        public static float RandomFloat(float min, float max)
        {
            return Random.Range(min, max);
        }

        public static bool RandomBool()
        {
            return Random.Range(0, 2) == 0;
        }

        public static bool RandomBool(float chance)
        {
            return Random.Range(0f, 1f) < chance;
        }

        public static T RandomEnum<T>()
        {
            System.Array A = System.Enum.GetValues(typeof(T));
            T V = (T)A.GetValue(Random.Range(0, A.Length));
            return V;
        }

        public static List<T> RandomList<T>(List<T> list)
        {
            List<T> newList = new List<T>();
            while (list.Count > 0)
            {
                int index = Random.Range(0, list.Count);
                newList.Add(list[index]);
                list.RemoveAt(index);
            }
            return newList;
        }

        public static List<int> GenerateRandomUniqeList(int min, int max, int count)
        {
            List<int> list = new List<int>();
            while (list.Count < count)
            {
                int random = Random.Range(min, max);
                if (!list.Contains(random))
                {
                    list.Add(random);
                }
            }
            return list;
        }

        public static List<T> GenerateRandomUniqeList<T>(List<T> list, int count)
        {
            List<T> newList = new List<T>();
            while (newList.Count < count)
            {
                int index = Random.Range(0, list.Count);
                if (!newList.Contains(list[index]))
                {
                    newList.Add(list[index]);
                }
            }
            return newList;
        }
        #endregion
    }
}