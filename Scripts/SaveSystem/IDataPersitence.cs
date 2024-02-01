using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataPersitence 
{
    void LoadData(BackGroundData data );
    void SaveData(ref BackGroundData data);
}
