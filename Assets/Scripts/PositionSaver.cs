﻿using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Profiling;

namespace DefaultNamespace
{
    [Serializable]
    public class PositionSaver : MonoBehaviour
	{
		[Serializable]
		public struct Data
		{
			public Vector3 Position;
            public float Time;
        }

		[SerializeField, ReadOnly, Tooltip("Для заполнения этого поля нужно воспользоваться контекстным меню в инспекторе и командой \"Create File\"")]
		private TextAsset _json;

		[field: SerializeField, HideInInspector]
		public List<Data> Records { get; private set; }

        public List<Data> RecordsJson { get; set; }

        private void Awake()
		{
            //todo comment: Что будет, если в теле этого условия не сделать выход из метода?
            //Если не будет выполнен выход из метода, то программа продолжит выполнение следующих строк кода. Это может привести к ошибкам, так как ожидается наличие корректного объекта TextAsset
            if (_json == null)
			{
				gameObject.SetActive(false);
				Debug.LogError("Please, create TextAsset and add in field _json");
				return;
			}
			
			  JsonUtility.FromJsonOverwrite(_json.text, this);
            //todo comment: Для чего нужна эта проверка (что она позволяет избежать)?
            //Проверка позволяет избежать ситуации, когда список Records не был инициализирован
            if (Records == null)
				Records = new List<Data>(10);

			RecordsJson = new List<Data>(10);
		}

        private void OnDrawGizmos()
		{
            //todo comment: Зачем нужны эти проверки (что они позволляют избежать)?
            //Проверки позволяют избежать рисования графики для списка записей, если список пустой или не был инициализирован
            if (Records == null || Records.Count == 0) return;
			var data = Records;
			var prev = data[0].Position;
			Gizmos.color = Color.green;
			Gizmos.DrawWireSphere(prev, 0.3f);
            //todo comment: Почему итерация начинается не с нулевого элемента?
            //Итерация начинается со второго элемента потому что первая позиция уже была использована для отрисовки и уже является предыдущей
            for (int i = 1; i < data.Count; i++)
			{
				var curr = data[i].Position;
				Gizmos.DrawWireSphere(curr, 0.3f);
				Gizmos.DrawLine(prev, curr);
				prev = curr;
			}
		}
		
#if UNITY_EDITOR
		[ContextMenu("Create File")]
		private void CreateFile()
		{
            //todo comment: Что происходит в этой строке?
            //Создается новый файл "Path.txt". После создания файла, объект stream будет содержать поток для работы с этим файлом.
            var stream = File.Create(Path.Combine(Application.dataPath, "Path.txt"));
            //todo comment: Подумайте для чего нужна эта строка? (а потом проверьте догадку, закомментировав) 
            //Эта строка кода вызывает метод для освобождения ресурсов, связанных с потоком stream
            stream.Dispose();
			UnityEditor.AssetDatabase.Refresh();
			//В Unity можно искать объекты по их типу, для этого используется префикс "t:"
			//После нахождения, Юнити возвращает массив гуидов (которые в мета-файлах задаются, например)
			var guids = UnityEditor.AssetDatabase.FindAssets("t:TextAsset");
			foreach (var guid in guids)
			{
				//Этой командой можно получить путь к ассету через его гуид
				var path = UnityEditor.AssetDatabase.GUIDToAssetPath(guid);
				//Этой командой можно загрузить сам ассет
				var asset = UnityEditor.AssetDatabase.LoadAssetAtPath<TextAsset>(path);
                //todo comment: Для чего нужны эти проверки?
                //Проверяет, что загруженный ассет не равен null и совпадение имени ассета с "Path"
                if (asset != null && asset.name == "Path")
				{
					_json = asset;
					UnityEditor.EditorUtility.SetDirty(this);
					UnityEditor.AssetDatabase.SaveAssets();
					UnityEditor.AssetDatabase.Refresh();
                    //todo comment: Почему мы здесь выходим, а не продолжаем итерироваться?
                    //После успешного нахождения и загрузки ассета с именем в переменную , дальнейший поиск не требуется. Это сделано для оптимизации и предотвращения лишних итераций в цикле
                    return;
				}
			}
		}

        private void OnDestroy()
        {
            if (_json == null) return;

            var path = AssetDatabase.GetAssetPath(_json);
            var text = JsonUtility.ToJson(this, true);

            path = Path.Combine(Application.dataPath.Replace("Assets", ""), path);

            File.WriteAllText(path, text);

        }
#endif
    }
}