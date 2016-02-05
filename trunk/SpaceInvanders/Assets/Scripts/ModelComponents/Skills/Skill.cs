using System;
using JetBrains.Annotations;

namespace Assets.Scripts.ModelComponents.Skills
{
    // ReSharper disable once InconsistentNaming
    public class SKILLS
    {
        public const string HEALTH = "health";
        public const string SPEED = "speed";
    }

    // ReSharper disable once InconsistentNaming
    public class DAMAGE
    {
        public const string HIT = "hit";
        public const string POISON = "poison";
    }

    public class Skill
    {
        public string Name { get; private set; }
        public double DefaultValue { get; private set; }

        private double _maxValue;
        public double MaxValue
        {
            get { return _maxValue; }
            set { _maxValue = value; }
        }

        private double _minValue;
        public double MinValue
        {
            get { return _minValue; }
            set { _minValue = value; }
        }

        private double _value;
        public double Value
        {
            get { return _value; }
        }

        private event Action MinValueEvent;
        private event Action MaxValueEvent;

        public Skill (string name_, double value_, double maxValue_ = double.MaxValue, double minValue_ = double.MinValue)
        {
            Name = name_;
            MaxValue = maxValue_;
            MinValue = minValue_;
            SetValue(value_);
            DefaultValue = Value;
        }

        /// <summary>
        /// Change skill value and returns _remainder
        /// </summary>
        public double ChangeValue(double amount_)
        {
            return SetValue(_value + amount_);
        }

        /// <summary>
        /// Set skill value and returns remainder
        /// </summary>
        public double SetValue(double newValue_)
        {
            double remainder = 0;

            if (newValue_ > MaxValue)
            {
                _value = MaxValue;
                OnMaxValueEvent();

                remainder = newValue_ - MaxValue;
                return remainder;
            }

            if (newValue_ <= MinValue)
            {
                _value = MinValue;
                OnMinValueEvent();

                remainder = newValue_ - MinValue; 
                return remainder;
            }

            return remainder;
        }

        public Skill MinValueCallback(Action callback_)
        {
            MinValueEvent += callback_;
            return this;
        }

        public Skill MaxValueCallback(Action callback_)
        {
            MaxValueEvent += callback_;
            return this;
        }

        protected virtual void OnMinValueEvent()
        {
            if (MinValueEvent != null) MinValueEvent.Invoke();
        }

        protected virtual void OnMaxValueEvent()
        {
            if (MaxValueEvent != null) MaxValueEvent.Invoke();
        }

    }
}