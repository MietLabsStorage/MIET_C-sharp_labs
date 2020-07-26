using System;

namespace ResearchBase
{
    class ResearchTeamsChangedEventArgs<TKey>: EventArgs
    {
        public string CollectionName { get; set; }
        public Revision Revision { get; set; }
        public string PropertyName { get; set; }
        public int RegisterNumber { get; set; }

        public ResearchTeamsChangedEventArgs(string _collectionName, Revision _revision, string _propertyName, int _registerNumber)
        {
            CollectionName = _collectionName;
            Revision = _revision;
            PropertyName = _propertyName;
            RegisterNumber = _registerNumber;
        }

        public override string ToString()
        {
            return
                "Collection::   " + CollectionName + "\n" +
                "Revision::     " + Revision.ToString() + "\n" +
                "Property::     " + PropertyName + "\n" +
                "Register num:: " + RegisterNumber;
        }
    }
}
