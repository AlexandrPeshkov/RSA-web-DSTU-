using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RSA_Web.Models;
using RSA_Web.Interfaces;

namespace RSA_Web.Services
{
    public class DirectionService : IDirection
    {
        private const double MaximumDirectionValue = 0.9999999;
        private const double MinimumDirectionValue = 0.0000001;

        public List<DirectionModel> Directions { get; set; }

        private int CurrentDirectionIndex { get; set; }

        public int Size
        {
            get
            {
                return Directions.Count;
            }
            set
            {
                Resize(value);
            }
        }

        public DirectionService()
        {
            this.Directions = new List<DirectionModel>();
        }

        public DirectionModel Direction
        {

            get
            {
                if (CurrentDirectionIndex >= 0 && CurrentDirectionIndex < Size)
                {
                    return Directions[CurrentDirectionIndex];
                }
                else
                {
                    return null;
                }
            }
        }

        public void SetNextDirection()
        {
            if (CurrentDirectionIndex < Size)
            {
                CurrentDirectionIndex++;
            }
            else
            {
                throw new IndexOutOfRangeException($"Нельзя выбрать следующее направление, CurrIndex = {CurrentDirectionIndex} MaxIndex={Size - 1}");
            }
        }

        private void InitialDirections(int StartIndex, int EndIndex)
        {
            for (int i = StartIndex; i < EndIndex; i++)
            {
                var Value = new Random().NextDouble() * (MaximumDirectionValue - MinimumDirectionValue) + MinimumDirectionValue;
                Directions.Add(new DirectionModel
                {
                    Direction = Value
                });
            }
        }

        private void Resize(int NewSize)
        {
            if (Size == 0)
            {
                Directions = new List<DirectionModel>();
                InitialDirections(0, NewSize);
                this.Size = NewSize;
            }
            else
            {
                if(NewSize > Size)
                {
                    InitialDirections(Size, NewSize);
                }
                else
                {
                    if(NewSize < Size)
                    {
                        Directions = Directions.Take(NewSize).ToList<DirectionModel>();
                    }
                }
            }
            if (Directions.Count > 0)
            {
                CurrentDirectionIndex = 0;
            }
            else
            {
                throw new IndexOutOfRangeException("Не удалось инициализировать множество направлений");
            }
        }
    }
}
