using BDSA2017.Lecture08.Models.Animals;
using System;

namespace BDSA2017.Lecture08.Models.Animals
{
    public class AnimalService : IAnimalService
    {
        private readonly IAnimal _animal;

        public AnimalService(IAnimal animal)
        {
            _animal = animal;
        }

        public void Speak()
        {
            _animal.Hello();
        }

        private bool disposed = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    (_animal as IDisposable)?.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
