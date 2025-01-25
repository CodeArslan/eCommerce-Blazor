namespace eCommerce.Services
{
    public class SharedStateService
    {
        public event Action OnChange;
        public int _TotalCartCount;
        public int TotalCartCount
        {
            get { return _TotalCartCount; }
            set
            {
                _TotalCartCount = value;
                NotifyStateChanged();
            }
        }
        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
