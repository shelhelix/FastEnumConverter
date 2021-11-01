using System;
using System.Collections.Generic;

namespace Benchmark {
	public class FastEnumConverter<T> where T : struct, Enum  {
		static Dictionary<T, string> _enumToStringConverter = new Dictionary<T, string>();
		static Dictionary<string, T> _stringToEnumConverter = new Dictionary<string, T>();

		public static string ConvertToString(T value) {
			if ( _enumToStringConverter.TryGetValue(value, out var res) ) {
				return res;
			}
			var str = value.ToString();
			if ( string.IsNullOrEmpty(str) ) {
				return str;
			}
			_enumToStringConverter.Add(value, str);
			if ( !_stringToEnumConverter.ContainsKey(str) ) {
				_stringToEnumConverter.Add(str, value);
			}
			return str;
		}

		public static T ConvertToEnum(string str) {
			if ( _stringToEnumConverter.TryGetValue(str, out var value) ) {
				return value;
			}
			if (Enum.TryParse(str, out T res)) {
				_stringToEnumConverter.Add(str, res);
				if ( !_enumToStringConverter.ContainsKey(res) ) {
					_enumToStringConverter.Add(res, str);
				}
				return res;
			}
			return default;
		}
		
	}
}