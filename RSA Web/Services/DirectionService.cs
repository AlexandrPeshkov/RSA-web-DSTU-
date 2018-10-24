using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RSA_Web.Models;
using RSA_Web.Interfaces;

namespace RSA_Web.Services
{
    /// <summary>
    /// Управление направлениями
    /// </summary>
    public class DirectionService : IServiceDirection
    {
        #region Constants
        private const double MaximumDirectionValue = 0.9999999;
        private const double MinimumDirectionValue = -0.9999999;
        #endregion

        #region Properties
        public int CurrentDirectionIndex { get; private set; }

        /// <summary>
        /// Список направлений поиска
        /// </summary>
        /// <value>
        /// Нормально распределенные величины в диапазоне [0;1]
        /// </value>
        /// <remarks>
        /// Расширяемое singletone множество коэффициентов для изменения шага поиска
        /// </remarks>
        public List<Direction> Directions { get; private set; }

        /// <summary>
        /// Текущий размер множества направлений
        /// </summary>
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
        #endregion

        #region Methods
        /// <summary>
        /// Создает пустой список, ставит текущий индекс направлений = 0
        /// </summary>
        public DirectionService()
        {
            this.Directions = new List<Direction>();
            CurrentDirectionIndex = 0;
        }

        public void InitialDirections(int StartIndex, int EndIndex)
        {
            for (int i = StartIndex; i < EndIndex; i++)
            {
                var Value = new Random().NextDouble() * (MaximumDirectionValue - MinimumDirectionValue) + MinimumDirectionValue;
                Directions.Add(new Direction
                {
                    direction = Value,
                    index = i
                });
            }
        }

        private void Resize(int NewSize)
        {
            if (Size == 0)
            {
                Directions = new List<Direction>();
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
                        Directions = Directions.Take(NewSize).ToList<Direction>();
                    }
                }
            }
        }

        /// <summary>
        /// Возвращает объект текущего направления
        /// </summary>
        public Direction CurrentDirection
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

        /// <summary>
        /// Смещает указатель текущего направления на +1
        /// </summary>
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
        #endregion
    }
}
