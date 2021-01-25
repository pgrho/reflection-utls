using System;
using System.Globalization;
using System.Threading;

namespace Shipwreck.ReflectionUtils
{
    public abstract class DisplayAttributeBase : Attribute
    {
        private sealed class LocalizedValues : LocalizedValues<(string prompt, string name, string groupName, string description, string shortName)>
        {
            private readonly DisplayAttributeBase _Attribute;

            public LocalizedValues(DisplayAttributeBase attribute)
            {
                _Attribute = attribute;
            }

            protected override (string prompt, string name, string groupName, string description, string shortName) GetValueCore(CultureInfo culture)
            {
                var th = Thread.CurrentThread;
                var c = th.CurrentCulture;
                var ui = th.CurrentUICulture;
                try
                {
                    th.CurrentCulture = th.CurrentUICulture = culture;

                    return (
                        prompt: GetLocalizedValue(_Attribute.Prompt),
                        name: GetLocalizedValue(_Attribute.Name),
                        groupName: GetLocalizedValue(_Attribute.GroupName),
                        description: GetLocalizedValue(_Attribute.Description),
                        shortName: GetLocalizedValue(_Attribute.ShortName));
                }
                finally
                {
                    th.CurrentCulture = c;
                    th.CurrentUICulture = ui;
                }
            }

            private string GetLocalizedValue(string nameOrValue)
                => _Attribute.ResourceType != null && nameOrValue != null
                ? (string)(_Attribute.ResourceType.GetProperty(nameOrValue) ?? throw new InvalidOperationException($"Property {nameOrValue} not defined in {_Attribute.ResourceType}")).GetValue(null)
                : nameOrValue;
        }

        internal DisplayAttributeBase()
        {
        }

        private LocalizedValues _ValueCache;

        private LocalizedValues ValueCache
            => _ValueCache ??= new LocalizedValues(this);

        #region Prompt

        private string _Prompt;

        public string Prompt
        {
            get => _Prompt;
            set
            {
                if (value != _Prompt)
                {
                    _Prompt = value;
                    _ValueCache?.Clear();
                }
            }
        }

        public string GetPrompt()
            => ValueCache.GetValue().prompt;

        #endregion Prompt

        #region Name

        private string _Name;

        public string Name
        {
            get => _Name;
            set
            {
                if (value != _Name)
                {
                    _Name = value;
                    _ValueCache?.Clear();
                }
            }
        }

        public string GetName()
            => ValueCache.GetValue().name;

        #endregion Name

        #region GroupName

        private string _GroupName;

        public string GroupName
        {
            get => _GroupName;
            set
            {
                if (value != _GroupName)
                {
                    _GroupName = value;
                    _ValueCache?.Clear();
                }
            }
        }

        public string GetGroupName()
            => ValueCache.GetValue().groupName;

        #endregion GroupName

        #region Description

        private string _Description;

        public string Description
        {
            get => _Description;
            set
            {
                if (value != _Description)
                {
                    _Description = value;
                    _ValueCache?.Clear();
                }
            }
        }

        public string GetDescription()
            => ValueCache.GetValue().description;

        #endregion Description

        #region ShortName

        private string _ShortName;

        public string ShortName
        {
            get => _ShortName;
            set
            {
                if (value != _ShortName)
                {
                    _ShortName = value;
                    _ValueCache?.Clear();
                }
            }
        }

        public string GetShortName()
            => ValueCache.GetValue().shortName;

        #endregion ShortName

        #region ResourceType

        private Type _ResourceType;

        public Type ResourceType
        {
            get => _ResourceType;
            set
            {
                if (value != _ResourceType)
                {
                    _ResourceType = value;
                    _ValueCache?.Clear();
                }
            }
        }

        #endregion ResourceType
    }
}
