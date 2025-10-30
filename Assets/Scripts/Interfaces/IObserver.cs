namespace ShrimpFPS.Interfaces
{
    public interface IObserver
    {
        public void OnNotify(SubjectEvents evt);
        public void RegisterSubject(Subject subject);
    }
}