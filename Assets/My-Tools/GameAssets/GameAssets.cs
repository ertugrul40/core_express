using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gokboerue.Tools
{
    [System.Serializable]
    public class GameAssets : MonoBehaviour
    {
        #region Prefab Manager
        private static GameAssets _i;
        public static GameAssets i_GameAssets
        {
            get
            {
                if (_i == null) _i = Instantiate(Resources.Load<GameAssets>("GameAssets"));
                return _i;
            }
        }

        public void Initialize()
        {
            if (_i == null)
            {
                _i = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        [System.Serializable]
        public class PrefabAssetPool
        {
            public string tag;
            public GameObject prefab;
            public bool isPool;
        }

        [Header("Prefabs")]
        public string AssetFileName = "AssetData";
        public List<PrefabAssetPool> prefabAssets = new List<PrefabAssetPool>();
        #endregion

        #region Editor        
        public void GenerateAssetData()
        {
            if (prefabAssets.Count != prefabAssets.Select(x => x.tag).Distinct().Count())
            {
                Debug.LogError("PrefabAssets has not unique tags");
                return;
            }

            string path = UnityEditor.EditorUtility.SaveFilePanel("Save Asset Data", "Assets/Scripts/GeneratedCodes", AssetFileName, "cs");
            if (!string.IsNullOrEmpty(path))
            {
                string assetData = "using System.Collections.Generic;\r\n";
                assetData += "\r\n";
                assetData += "namespace Gokboerue.GeneratedCodes\n{\n\tpublic class " + AssetFileName + "\n\t{\n";

                foreach (PrefabAssetPool prefabAsset in prefabAssets)
                {
                    string variableName = prefabAsset.prefab.name.Replace(" ", "");
                    assetData += "\t\tpublic const string " + variableName + " = \"" + prefabAsset.prefab.name + "\";\n";
                }

                assetData += "\n";
                assetData += "\t\tpublic static List<string> tags = new List<string>()\n\t\t{\n";

                foreach (PrefabAssetPool prefabAsset in prefabAssets)
                {
                    assetData += "\t\t\t" + prefabAsset.prefab.name + ",\n";
                }

                assetData += "\t\t};\n";

                assetData += "\t}\n}";
                System.IO.File.WriteAllText(path, assetData);
                UnityEditor.AssetDatabase.Refresh();

                Debug.Log(AssetFileName + " Created");
            }
            else
            {
                Debug.LogWarning(AssetFileName + " Not Created Because of Path is Empty");
            }
        }
        #endregion

        #region Unity Methods
        private void Awake()
        {
            Initialize();
        }
        #endregion

        #region Pooler Manager
        public static Dictionary<string, Queue<GameObject>> pool = new Dictionary<string, Queue<GameObject>>();
        public static GameObject GetObjectFromPool(string name, Vector3 position, Quaternion rotation)
        {
            var asset = Gokboerue.GetPrefabAsset(name);
            if (asset.isPool)
            {
                if (!pool.ContainsKey(name))
                {
                    pool.Add(name, new Queue<GameObject>());
                }

                if (pool[name].Count == 0)
                {
                    GameObject obj = Instantiate(Gokboerue.GetPrefabAsset(name).prefab.gameObject, position, rotation);

                    SetTransformForPool(obj.transform, name);

                    obj.name = name;
                    return obj;
                }
                else
                {
                    GameObject obj = pool[name].Dequeue();
                    obj.transform.position = position;
                    obj.transform.rotation = rotation;
                    obj.SetActive(true);
                    return obj;
                }
            }
            else
            {
                GameObject obj = Instantiate(Gokboerue.GetPrefabAsset(name).prefab.gameObject, position, rotation);
                obj.name = name;
                return obj;
            }
        }

        public static void ReturnObjectToPool(GameObject obj)
        {
            if (Gokboerue.GetPrefabAsset(obj.name).isPool)
            {
                obj.SetActive(false);
                pool[obj.name].Enqueue(obj);
            }
            else
            {
                Destroy(obj);
            }
        }

        public static void ClearPool()
        {
            pool.Clear();
        }

        private static void SetTransformForPool(Transform obj, string name)
        {
            if (_i.transform.Find(name) != null)
            {
                obj.transform.SetParent(_i.transform.Find(name));
            }
            else
            {
                GameObject parent = new GameObject(name);
                parent.transform.SetParent(_i.transform);
                obj.transform.SetParent(parent.transform);
            }
        }
        #endregion
    }
}