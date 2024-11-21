using UnityEngine;

namespace DefaultNamespace
{
	
	[RequireComponent(typeof(PositionSaver))]
	public class EditorMover : MonoBehaviour
	{
		private PositionSaver _save;
		private float _currentDelay;

		//todo comment: Что произойдёт, если _delay > _duration?
		//Это может привести к тому, что задержка между сохранением позиций объекта будет дольше, чем общая длительность работы компонента
		[SerializeField, Range(0.2f,1.0f)]        
		private float _delay = 0.5f;
		[SerializeField, Min(0.2f)]
		private float _duration = 5f;

		private void Start()
		{
            //todo comment: Почему этот поиск производится здесь, а не в начале метода Update?
            //Для гарантии того, что он будет выполнен только один раз при старте объекта
            _save = GetComponent<PositionSaver>();
			_save.Records.Clear();

			if (_duration <= _delay ) 
			{
				_duration = _delay * 5f;
			}
		}

		private void Update()
		{
			_duration -= Time.deltaTime;
			if (_duration <= 0f)
			{
				enabled = false;
				Debug.Log($"<b>{name}</b> finished", this);
				return;
			}

            //todo comment: Почему не написать (_delay -= Time.deltaTime;) по аналогии с полем _duration?
            //Значение _duration постепенно уменьшается по мере прохождения времени, что позволяет отслеживать общую длительность работы компонента.
			//В то время как _delay в данном случае задает интервал между сохранением позиций объекта
            _currentDelay -= Time.deltaTime;
			if (_currentDelay <= 0f)
			{
				_currentDelay = _delay;
				_save.Records.Add(new PositionSaver.Data
				{
					Position = transform.position,
                    //todo comment: Для чего сохраняется значение игрового времени?
                    //Для воспроизведения и визуализации пути объекта во времени (?)
                    Time = Time.time,
				});
			}
		}
	}
}