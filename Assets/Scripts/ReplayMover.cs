using System;
using UnityEngine;

namespace DefaultNamespace
{
	[RequireComponent(typeof(PositionSaver))]
	public class ReplayMover : MonoBehaviour
	{
		private PositionSaver _save;

		private int _index;
		private PositionSaver.Data _prev;
		private float _duration;

		private void Start()
		{
            ////todo comment: зачем нужны эти проверки?
            //Проверка, удалось ли найти компонент PositionSaver на текущем GameObject и имеет ли он записи. Если условие не выполняется, то выводится сообщение об ошибке в консоль
            if (!TryGetComponent(out _save) || _save.Records.Count == 0)
			{
				Debug.LogError("Records incorrect value", this);
                //todo comment: Для чего выключается этот компонент?
                //Отключаем компонент ReplayMover в случае несоответствия условиям, чтобы остановить его работу
                enabled = false;
			}
		}

		private void Update()
		{
			var curr = _save.Records[_index];
            //todo comment: Что проверяет это условие (с какой целью)? 
            //Проверка, прошло ли уже достаточно времени для воспроизведения следующей записи из списка Record
            if (Time.time > curr.Time)
			{
				_prev = curr;
				_index++;
                //todo comment: Для чего нужна эта проверка?
                //Проверка, достигли ли мы конца списка записей. Если текущий индекс _index становится больше или равен количеству записей, то компонент отключается
                if (_index >= _save.Records.Count)
				{
					enabled = false;
					Debug.Log($"<b>{name}</b> finished", this);
				}
			}
            //todo comment: Для чего производятся эти вычисления (как в дальнейшем они применяются)?
            //Вычисление delta происходит для интерполяции двух позиций объекта между двумя кадрами записи
            var delta = (Time.time - _prev.Time) / (curr.Time - _prev.Time);
            //todo comment: Зачем нужна эта проверка?
            //Проверка нужна для обработки случаев, когда данное значение delta является не числом. Если это происходит, то delta принимает значение 0f, чтобы избежать ошибок при вычислении позиции объекта
            if (float.IsNaN(delta)) delta = 0f;
            //todo comment: Опишите, что происходит в этой строчке так подробно, насколько это возможно
            //В данной строке кода происходит интерполяция между предыдущей и текущей позициями объекта с использованием значения delta. Функция Vector3.Lerp выполняет
			//линейную интерполяцию между двумя векторами с учетом коэффициента delta
            transform.position = Vector3.Lerp(_prev.Position, curr.Position, delta);
		}
	}
}