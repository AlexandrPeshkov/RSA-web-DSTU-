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
        public List<Direction> Directions { get; set; }

        /// <summary>
        /// Текущий размер множества направлений
        /// </summary>
        public uint Size
        {
            get
            {
                return (uint)Directions.Count;
            }
            set
            {
                Directions = new List<Direction>();
                for (var i =0; i<value; i++)
                {
                    Directions.Add(
                        new Direction()
                        {
                            Index = i,
                            Value = 0
                        }
                    );
                }
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
                    return new Direction()
                    {
                        Value = 0,
                        Index = -1
                    };
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
                throw new IndexOutOfRangeException($"Нельзя выбрать следующее направление, CurrIndex = {CurrentDirectionIndex} Size={Size}");
            }
        }

        public void ResetDirectionsPointer()
        {
            this.CurrentDirectionIndex = 0;
        }

        /// <summary>
        /// Генератор случайных направлений
        /// </summary>
        /// <returns>Список направлений размера Size</returns>
        public List<Direction> GenerateDirections()
        {
            var NewDirections = new List<Direction>();
            for (int i = 0; i < Size; i++)
            {
                var Value = new Random().NextDouble() * (MaximumDirectionValue - MinimumDirectionValue) + MinimumDirectionValue;
                NewDirections.Add(new Direction
                {
                    Value = Value,
                    Index = i
                });
            }
            Directions = NewDirections;
            return Directions;
        }
        #endregion
    }
}
