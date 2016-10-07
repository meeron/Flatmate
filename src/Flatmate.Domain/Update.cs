using Flatmate.Domain.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flatmate.Domain
{
    public class Update<T>
        where T: ModelBase
    {
        private readonly Dictionary<string, object> _fieldsWithValues;

        private Update()
        {
        }    

        private Update(string fieldName, object value)
        {
            _fieldsWithValues = new Dictionary<string, object>();
            _fieldsWithValues.Add(fieldName, value);
        }

        public Update<T> And(string fieldName, object value)
        {
            if (!_fieldsWithValues.ContainsKey(fieldName))
                _fieldsWithValues.Add(fieldName, null);

            _fieldsWithValues[fieldName] = value;

            return this;
        }

        public static Update<T> Set(string fieldName, object value)
        {
            return new Update<T>(fieldName, value);
        }

        internal Dictionary<string, object> FieldsWithValues {get { return _fieldsWithValues; } }
    }
}
