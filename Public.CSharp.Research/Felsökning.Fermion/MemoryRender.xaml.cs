//-----------------------------------------------------------------------
// <copyright file="MemoryRender.xaml.cs" company="None">
//     Copyright (c) 2019 felsokning. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Felsökning.Fermion
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;

    /// <summary>
    ///     Initializes a new instance of the <see cref="MemoryRender"/> class.
    /// </summary>
    public partial class MemoryRender : Window
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="MemoryRender"/> class.
        /// </summary>
        /// <param name="returnMemDict">Dictionary passed in to convert.</param>
        public MemoryRender(Dictionary<int, Dictionary<string, long>> returnMemDict)
        {
            InitializeComponent();
            blog.ItemsSource = LoadCollectionData(returnMemDict);
        }

        /// <summary>
        /// Initializes a new instance of <see cref="LoadCollectionData"/>.
        /// </summary>
        /// <param name="returnMemDict"></param>
        /// <returns>List of type <see cref="ID"/> to display in the DataGrid.</returns>
        public static List<Id> LoadCollectionData(Dictionary<int, Dictionary<string, long>> returnMemDict)
        {
            List<Id> list = new List<Id>();
            foreach (KeyValuePair<int, Dictionary<string, long>> bob in returnMemDict)
            {
                IEnumerable<string> tempIString = bob.Value.Keys.OfType<string>();
                string tempString = (string)tempIString.FirstOrDefault<string>();
                IEnumerable<long> tempILong = bob.Value.Values.OfType<long>();
                long tempLong = (long)tempILong.First<long>();
                list.Add(new Id() { Identity = bob.Key, Name = tempString, Value = tempLong });
            }

            return list;
        }
    }
}