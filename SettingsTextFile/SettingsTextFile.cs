﻿using System;
using System.Collections.Generic;
using System.IO;

namespace SettingsFile
{
    public class SettingsTextFile
    {
        // String Variables
        public Dictionary<string, string> Strings { get; private set; } = new Dictionary<string, string>();
        public Dictionary<string, char> Chars { get; private set; } = new Dictionary<string, char>();
        public Dictionary<string, DateTime> DateTimes { get; private set; } = new Dictionary<string, DateTime>();

        // Signed Variables
        public Dictionary<string, bool> Bools { get; private set; } = new Dictionary<string, bool>();
        public Dictionary<string, sbyte> SBytes { get; private set; } = new Dictionary<string, sbyte>();
        public Dictionary<string, short> Shorts { get; private set; } = new Dictionary<string, short>();
        public Dictionary<string, int> Ints { get; private set; } = new Dictionary<string, int>();
        public Dictionary<string, long> Longs { get; private set; } = new Dictionary<string, long>();

        // Unsigned Variables
        public Dictionary<string, byte> Bytes { get; private set; } = new Dictionary<string, byte>();
        public Dictionary<string, ushort> UShorts { get; private set; } = new Dictionary<string, ushort>();
        public Dictionary<string, uint> UInts { get; private set; } = new Dictionary<string, uint>();
        public Dictionary<string, ulong> ULongs { get; private set; } = new Dictionary<string, ulong>();

        // Decimal Variables
        public Dictionary<string, double> Doubles { get; private set; } = new Dictionary<string, double>();
        public Dictionary<string, float> Floats { get; private set; } = new Dictionary<string, float>();
        public Dictionary<string, decimal> Decimals { get; private set; } = new Dictionary<string, decimal>();

        public enum DataTypes
        {
            String,
            Char,
            DateTime,
            Bool,
            SByte,
            Short,
            Int,
            Long,
            Byte,
            UShort,
            UInt,
            ULong,
            Double,
            Float,
            Decimal,
        }

        public SettingsTextFile()
        {

        }

        public static bool Exists(string fileName)
        {
            return File.Exists(fileName);
        }

        public void Write(string fileName, char delimiter = '=')
        {
            string delimeterString = delimiter.ToString();

            using(StreamWriter sw = new StreamWriter(File.OpenWrite(fileName)))
            {
                // String Variables
                foreach (KeyValuePair<string, string> kvp in Strings)
                {
                    sw.WriteLine(kvp.Key + delimeterString + DataTypes.String.ToString()    + delimeterString + kvp.Value);
                }                                                                                              
                foreach (KeyValuePair<string, char> kvp in Chars)                                              
                {                                                                                              
                    sw.WriteLine(kvp.Key + delimeterString + DataTypes.Char.ToString()      + delimeterString + kvp.Value.ToString());
                }                                                                                              
                foreach (KeyValuePair<string, DateTime> kvp in DateTimes)                                      
                {                                                                                              
                    sw.WriteLine(kvp.Key + delimeterString + DataTypes.DateTime.ToString()  + delimeterString + kvp.Value.ToString());
                }                                                                                              
                                                                                                               
                // Signed Variables                                                                            
                foreach (KeyValuePair<string, bool> kvp in Bools)                                              
                {                                                                                              
                    sw.WriteLine(kvp.Key + delimeterString + DataTypes.Bool.ToString()      + delimeterString + kvp.Value.ToString());
                }                                                                                              
                foreach (KeyValuePair<string, sbyte> kvp in SBytes)                                            
                {                                                                                              
                    sw.WriteLine(kvp.Key + delimeterString + DataTypes.SByte.ToString()     + delimeterString + kvp.Value.ToString());
                }                                                                                              
                foreach (KeyValuePair<string, short> kvp in Shorts)                                            
                {                                                                                              
                    sw.WriteLine(kvp.Key + delimeterString + DataTypes.Short.ToString()     + delimeterString + kvp.Value.ToString());
                }                                                                                              
                foreach (KeyValuePair<string, int> kvp in Ints)                                                
                {                                                                                              
                    sw.WriteLine(kvp.Key + delimeterString + DataTypes.Int.ToString()       + delimeterString + kvp.Value.ToString());
                }                                                                                              
                foreach (KeyValuePair<string, long> kvp in Longs)                                              
                {                                                                                              
                    sw.WriteLine(kvp.Key + delimeterString + DataTypes.Long.ToString()      + delimeterString + kvp.Value.ToString());
                }                                                                                              
                                                                                                               
                // Unsigned Variables                                                                          
                foreach (KeyValuePair<string, byte> kvp in Bytes)                                              
                {                                                                                              
                    sw.WriteLine(kvp.Key + delimeterString + DataTypes.Byte.ToString()      + delimeterString + kvp.Value.ToString());
                }                                                                                              
                foreach (KeyValuePair<string, ushort> kvp in UShorts)                                          
                {                                                                                              
                    sw.WriteLine(kvp.Key + delimeterString + DataTypes.UShort.ToString()    + delimeterString + kvp.Value.ToString());
                }                                                                                              
                foreach (KeyValuePair<string, uint> kvp in UInts)                                              
                {                                                                                              
                    sw.WriteLine(kvp.Key + delimeterString + DataTypes.UInt.ToString()      + delimeterString + kvp.Value.ToString());
                }                                                                                              
                foreach (KeyValuePair<string, ulong> kvp in ULongs)                                            
                {                                                                                              
                    sw.WriteLine(kvp.Key + delimeterString + DataTypes.ULong.ToString()     + delimeterString + kvp.Value.ToString());
                }                                                                                              
                                                                                                               
                // Decimal Variables                                                                           
                foreach (KeyValuePair<string, double> kvp in Doubles)                                          
                {                                                                                              
                    sw.WriteLine(kvp.Key + delimeterString + DataTypes.Double.ToString()    + delimeterString + kvp.Value.ToString());
                }                                                                                              
                foreach (KeyValuePair<string, float> kvp in Floats)                                            
                {                                                                                              
                    sw.WriteLine(kvp.Key + delimeterString + DataTypes.Float.ToString()     + delimeterString + kvp.Value.ToString());
                }                                                                                              
                foreach (KeyValuePair<string, decimal> kvp in Decimals)                                        
                {                                                                                              
                    sw.WriteLine(kvp.Key + delimeterString + DataTypes.Decimal.ToString()   + delimeterString + kvp.Value.ToString());
                }
            }
        }

        public static SettingsTextFile Read(string fileName, char delimeter = '=')
        {
            // Check if file exists
            if (!Exists(fileName))
            {
                return null;
            }

            // Read the file
            SettingsTextFile settingsTextFile = new SettingsTextFile();
            List<string> lines = new List<string>(File.ReadAllLines(fileName));
            string[] settingsFileEntry;
            DataTypes dataType;

            // Parse each line
            foreach(string line in lines)
            {
                settingsFileEntry = line.Split(delimeter);

                if(settingsFileEntry.Length != 3)
                {
                    throw new InvalidDataException();
                }

                // Allow enum parse exception to be thrown to user
                dataType = (DataTypes)Enum.Parse(typeof(DataTypes), settingsFileEntry[1]);

                // Add entry based on data type
                // String Variables
                if(dataType == DataTypes.String)
                {
                    settingsTextFile.Add(settingsFileEntry[0], settingsFileEntry[2]);
                }
                else if(dataType == DataTypes.Char)
                {
                    settingsTextFile.Add(settingsFileEntry[0], char.Parse(settingsFileEntry[2]));
                }
                else if (dataType == DataTypes.DateTime)
                {
                    settingsTextFile.Add(settingsFileEntry[0], DateTime.Parse(settingsFileEntry[2]));
                }

                // Signed Variables
                else if (dataType == DataTypes.Bool)
                {
                    settingsTextFile.Add(settingsFileEntry[0], bool.Parse(settingsFileEntry[2]));
                }
                else if (dataType == DataTypes.SByte)
                {
                    settingsTextFile.Add(settingsFileEntry[0], sbyte.Parse(settingsFileEntry[2]));
                }
                else if (dataType == DataTypes.Short)
                {
                    settingsTextFile.Add(settingsFileEntry[0], short.Parse(settingsFileEntry[2]));
                }
                else if (dataType == DataTypes.Int)
                {
                    settingsTextFile.Add(settingsFileEntry[0], int.Parse(settingsFileEntry[2]));
                }
                else if (dataType == DataTypes.Long)
                {
                    settingsTextFile.Add(settingsFileEntry[0], long.Parse(settingsFileEntry[2]));
                }

                // Unsigned Variables
                else if (dataType == DataTypes.Byte)
                {
                    settingsTextFile.Add(settingsFileEntry[0], byte.Parse(settingsFileEntry[2]));
                }
                else if (dataType == DataTypes.UShort)
                {
                    settingsTextFile.Add(settingsFileEntry[0], ushort.Parse(settingsFileEntry[2]));
                }
                else if (dataType == DataTypes.UInt)
                {
                    settingsTextFile.Add(settingsFileEntry[0], uint.Parse(settingsFileEntry[2]));
                }
                else if (dataType == DataTypes.ULong)
                {
                    settingsTextFile.Add(settingsFileEntry[0], ulong.Parse(settingsFileEntry[2]));
                }

                // Decimal Variables
                else if (dataType == DataTypes.Double)
                {
                    settingsTextFile.Add(settingsFileEntry[0], double.Parse(settingsFileEntry[2]));
                }
                else if (dataType == DataTypes.Float)
                {
                    settingsTextFile.Add(settingsFileEntry[0], float.Parse(settingsFileEntry[2]));
                }
                else if (dataType == DataTypes.Decimal)
                {
                    settingsTextFile.Add(settingsFileEntry[0], decimal.Parse(settingsFileEntry[2]));
                }
            }

            return settingsTextFile;
        }

        #region Add

        // String Variables
        public void Add(string name, string value)
        {
            Strings.Add(name, value);
        }
        public void Add(string name, char value)
        {
            Chars.Add(name, value);
        }
        public void Add(string name, DateTime value)
        {
            DateTimes.Add(name, value);
        }

        // Signed Variables
        public void Add(string name, bool value)
        {
            Bools.Add(name, value);
        }
        public void Add(string name, sbyte value)
        {
            SBytes.Add(name, value);
        }
        public void Add(string name, short value)
        {
            Shorts.Add(name, value);
        }
        public void Add(string name, int value)
        {
            Ints.Add(name, value);
        }
        public void Add(string name, long value)
        {
            Longs.Add(name, value);
        }

        // Unsigned Variables
        public void Add(string name, byte value)
        {
            Bytes.Add(name, value);
        }
        public void Add(string name, ushort value)
        {
            UShorts.Add(name, value);
        }
        public void Add(string name, uint value)
        {
            UInts.Add(name, value);
        }
        public void Add(string name, ulong value)
        {
            ULongs.Add(name, value);
        }

        // Decimal Variables
        public void Add(string name, double value)
        {
            Doubles.Add(name, value);
        }
        public void Add(string name, float value)
        {
            Floats.Add(name, value);
        }

        public void Add(string name, decimal value)
        {
            Decimals.Add(name, value);
        }

        #endregion
    }
}
