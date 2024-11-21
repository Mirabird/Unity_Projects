namespace Final_Game
{
    public abstract class CasinoGameBase
    {
        public event Action OnWin;
        public event Action OnLoose;
        public event Action OnDraw;

        protected virtual void OnWinInvoke()
        {
            OnWin?.Invoke();
        }

        protected virtual void OnLooseInvoke()
        {
            OnLoose?.Invoke();
        }

        protected virtual void OnDrawInvoke()
        {
            OnDraw?.Invoke();
        }

        public abstract void PlayGame();

        protected abstract void FactoryMethod();

        protected abstract void PrintResults();
        
        protected void CheckInputParametersValidity(object[] parameters)
        {
            foreach (var parameter in parameters)
            {
                if (parameter == null)
                {
                    throw new ArgumentNullException(nameof(parameters), "Входные параметры конструктора не могут быть null.");
                }
            }
        }
    }
}
