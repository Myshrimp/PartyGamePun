using System;

namespace ShrimpFPS.Interfaces
{
    public interface ISubject
    {
        public void RegisterEvent(SubjectEvents evt,Action act);
        public void PublishEvent(SubjectEvents evt);
    }
}