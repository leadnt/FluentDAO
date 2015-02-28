﻿using System;
using System.Data;

namespace LeadNT.FluentDAO
{
	internal class DataReader : IDataReader
	{
		public System.Data.IDataReader InnerReader { get; private set; }

		public DataReader(System.Data.IDataReader reader)
		{
			InnerReader = reader;
		}

		public void Close()
		{
			InnerReader.Close();
		}

		private T GetValue<T>(int i)
		{
			var value = InnerReader.GetValue(i);
			if(value == DBNull.Value)
				return default(T);
			return (T)value;
		}

		public int Depth
		{
			get { return InnerReader.Depth; }
		}

		public DataTable GetSchemaTable()
		{
			return InnerReader.GetSchemaTable();
		}

		public bool IsClosed
		{
			get { return InnerReader.IsClosed; }
		}

		public bool NextResult()
		{
			return InnerReader.NextResult();
		}

		public bool Read()
		{
			return InnerReader.Read();
		}

		public int RecordsAffected
		{
			get { return InnerReader.RecordsAffected; }
		}

		public void Dispose()
		{
			InnerReader.Dispose();
		}

		public int FieldCount
		{
			get { return InnerReader.FieldCount; }
		}

		public bool GetBoolean(int i)
		{
			return GetValue<bool>(i);
		}

		public bool GetBoolean(string name)
		{
			return GetBoolean(GetOrdinal(name));
		}

		public byte GetByte(int i)
		{
			return GetValue<byte>(i);
		}

		public byte GetByte(string name)
		{
			return GetByte(GetOrdinal(name));
		}

		public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
		{
			return IsDBNull(i) ? 0 : InnerReader.GetBytes(i, fieldOffset, buffer, bufferoffset, length);
		}

		public long GetBytes(string name, long fieldOffset, byte[] buffer, int bufferoffset, int length)
		{
			return GetBytes(GetOrdinal(name), fieldOffset, buffer, bufferoffset, length);
		}

		public char GetChar(int i)
		{
			return GetValue<char>(i);
		}

		public char GetChar(string name)
		{
			return GetChar(GetOrdinal(name));
		}

		public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
		{
			return IsDBNull(i) ? 0 : InnerReader.GetChars(i, fieldoffset, buffer, bufferoffset, length);
		}

		public long GetChars(string name, long fieldoffset, char[] buffer, int bufferoffset, int length)
		{
			return GetChars(GetOrdinal(name), fieldoffset, buffer, bufferoffset, length);
		}

		public System.Data.IDataReader GetData(int i)
		{
			return InnerReader.GetData(i);
		}

		public System.Data.IDataReader GetData(string name)
		{
			return GetData(GetOrdinal(name));
		}

		public string GetDataTypeName(int i)
		{
			return InnerReader.GetDataTypeName(i);
		}

		public string GetDataTypeName(string name)
		{
			return GetDataTypeName(GetOrdinal(name));
		}

		public DateTime GetDateTime(int i)
		{
			return GetValue<DateTime>(i);
		}

		public DateTime GetDateTime(string name)
		{
			return GetDateTime(GetOrdinal(name));
		}

		public decimal GetDecimal(int i)
		{
			return GetValue<decimal>(i);
		}

		public decimal GetDecimal(string name)
		{
			return GetDecimal(GetOrdinal(name));
		}

		public double GetDouble(int i)
		{
			return GetValue<double>(i);
		}

		public double GetDouble(string name)
		{
			return GetDouble(GetOrdinal(name));
		}

		public Type GetFieldType(int i)
		{
			return InnerReader.GetFieldType(i);
		}

		public Type GetFieldType(string name)
		{
			return GetFieldType(GetOrdinal(name));
		}

		public float GetFloat(int i)
		{
			return GetValue<float>(i);
		}

		public float GetFloat(string name)
		{
			return GetFloat(GetOrdinal(name));
		}

		public Guid GetGuid(int i)
		{
			return GetValue<Guid>(i);
		}

		public Guid GetGuid(string name)
		{
			return GetGuid(GetOrdinal(name));
		}

		public short GetInt16(int i)
		{
			return GetValue<Int16>(i);
		}

		public short GetInt16(string name)
		{
			return GetInt16(GetOrdinal(name));
		}

		public int GetInt32(int i)
		{
			return GetValue<int>(i);
		}

		public int GetInt32(string name)
		{
			return GetInt32(GetOrdinal(name));
		}

		public long GetInt64(int i)
		{
			return GetValue<long>(i);
		}

		public long GetInt64(string name)
		{
			return GetInt64(GetOrdinal(name));
		}

		public string GetName(int i)
		{
			return InnerReader.GetName(i);
		}

		public string GetName(string name)
		{
			return InnerReader.GetName(GetOrdinal(name));
		}

		public int GetOrdinal(string name)
		{
			return InnerReader.GetOrdinal(name);
		}

		public string GetString(int i)
		{
			return GetValue<string>(i);
		}

		public string GetString(string name)
		{
			return GetString(GetOrdinal(name));
		}

		public object GetValue(int i)
		{
			return GetValue<object>(i);
		}

		public object GetValue(string name)
		{
			return GetValue(GetOrdinal(name));
		}

		public int GetValues(object[] values)
		{
			return InnerReader.GetValues(values);
		}

		public bool IsDBNull(int i)
		{
			return InnerReader.IsDBNull(i);
		}

		public bool IsDBNull(string name)
		{
			return IsDBNull(GetOrdinal(name));
		}

		public object this[string name]
		{
			get { return this[GetOrdinal(name)]; }
		}

		public object this[int i]
		{
			get
			{
				return IsDBNull(i) ? null : InnerReader[i];
			}
		}
	}
}
